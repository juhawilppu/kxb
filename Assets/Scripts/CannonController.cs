using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CannonController : MonoBehaviour {

    Player player;

    Text textK_Single;
    Text textK_Multi_Up;
    Text textK_Multi_Divider;
    Text textK_Multi_Down;

    Text textB;

    LineRenderer lineRenderer;

    Vector3 oldStart;
    Vector3 iterationStart;
    Vector3 currentStart;
    Vector3 oldEnd;
    Vector3 iterationEnd;
    Vector3 currentEnd;

    Color shootColor = new Color(0, 255, 0);
    Color okColor = new Color(0, 0, 255);
    Color failColor = new Color(255, 0, 0);

    private float startTime = -2;
    private float transitionTime = 0.5f;

    int kUp = 0;
    int kDown = 1;

    int b = 0;
    private float journeyLength;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<Player>();

        textK_Single = GameObject.Find("k Single").GetComponent<Text>();
        textK_Multi_Up = GameObject.Find("k Multi/k up").GetComponent<Text>();
        textK_Multi_Divider = GameObject.Find("k Multi/____").GetComponent<Text>();
        textK_Multi_Down = GameObject.Find("k Multi/k down").GetComponent<Text>();

        textB = GameObject.Find("b").GetComponent<Text>();

        DrawNumbers();
	}

    void DrawNumbers()
    {
        setKSingleVisible(getK() >= 1 || getK() == 0f || getK() <= -1);

        if (getK() >= 1 || getK() == 0f || getK() <= -1)
        {
            textK_Single.text = format(kUp);            
        } else
        {
            textK_Multi_Up.text = format(kUp);
            textK_Multi_Down.text = format(kDown);
        }

        textB.text = format(b);

        var center = new Vector3(0, b, 1);
        var offset = new Vector3(20, 20*getK(), 0);

        var start = center - offset;
        var end = center + offset;
        DrawLine(start, end);
    }
    
    string format(int number)
    {
        return  ("" + number).Replace('-', '–'); // en dash
    }

    float getK()
    {
        return (float)kUp / (float)kDown;
    }

    void setKSingleVisible(bool isSingleVisible)
    {
        textK_Single.enabled = isSingleVisible;
        textK_Multi_Up.enabled = !isSingleVisible;
        textK_Multi_Divider.enabled = !isSingleVisible;
        textK_Multi_Down.enabled = !isSingleVisible;
    }

    public void changeB(string message)
    {
        if (message == "+")
            b++;
        else
            b--;

        b = Mathf.Clamp(b, -5, 5);

        DrawNumbers();
    }

    public void changeK(string message)
    {
        float k = getK();

        if (message == "+")
        {
            // Cases are sort from largest to smallest
            if (k >= 1)
            {
                kUp++;
            } else if (k < 1 && k > 0)
            {
                kDown--;
            }
            else if (k == 0f)
            {
                kUp = 1;
                kDown = 5;
            } else if (kUp == -1 && kDown == 5)
            {
                kUp = 0;
                kDown = 1;
            }
            else if (k < 0 && k > -1)
            {
                kDown++;
            }
            else if (k == -1)
            {
                kUp = -1;
                kDown = 2;
            } else if (k < -1)
            {
                kUp++;
            }
        } else
        {
            // Cases are sort from largest to smallest
            if (k > 1)
            {
                kUp--;
            }
            else if (k == 1f)
            {
                kUp = 1;
                kDown = 2;
            }
            else if (kUp == 1 && kDown == 5)
            {
                kUp = 0;
                kDown = 1;
            }
            else if (k < 1 && k > 0)
            {
                kDown++;
            }
            else if (k == 0f)
            {
                kUp = -1;
                kDown = 5;
            }
            else if (k <= 0 && k > -1)
            {
                kDown--;
            }
            else if (k <= -1)
            {
                kUp--;
            }
        }

        kUp = Mathf.Clamp(kUp, -5, 5);
        kDown = Mathf.Clamp(kDown, -5, 5);

        DrawNumbers();
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        if (lineRenderer == null)
        {
            GameObject myLine = new GameObject();
            myLine.transform.position = start;
            myLine.AddComponent<LineRenderer>();

            lineRenderer = myLine.GetComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.SetWidth(0.1f, 0.1f);

            lineRenderer.sortingOrder = 12;
            lineRenderer.sortingLayerName = "Players";
        }

        if (player.gameObject.transform.position.y == b)
        {
            lineRenderer.SetColors(okColor, okColor);
        } else
        {
            lineRenderer.SetColors(failColor, failColor);
        }

        if (startTime == -2)
        {
            // New line
            oldStart = start;
            oldEnd = end;
        } else
        {
            // Move existing line
            oldStart = iterationStart;
            oldEnd = iterationEnd;
        }
        currentStart = start;
        currentEnd = end;

        journeyLength = (oldStart - currentStart).magnitude;
        startTime = Time.time;
    }

    void Update()
    {
        if (startTime < 0)
            return;

        float timeSpent = Time.time - startTime;
        float fracJourney = timeSpent / transitionTime;
        fracJourney = Mathf.Clamp(fracJourney, 0, 1);

        iterationStart = oldStart * (1 - fracJourney) +  currentStart * fracJourney;
        iterationEnd = oldEnd * (1 - fracJourney) + currentEnd * fracJourney;

        lineRenderer.SetPosition(0, iterationStart);
        lineRenderer.SetPosition(1, iterationEnd);
        player.SetRotation(iterationEnd);

        if (fracJourney == 1)
            startTime = -1;
    }

    public void Shoot()
    {
        bool hitEnemy = player.Shoot(currentStart, currentEnd);

        if (hitEnemy)
        {
            lineRenderer.SetColors(shootColor, shootColor);
        } else
        {
            lineRenderer.SetColors(failColor, failColor);
        }
    }
}
