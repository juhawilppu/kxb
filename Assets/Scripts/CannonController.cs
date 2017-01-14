using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CannonController : MonoBehaviour {

    Player player;
    Text textK;
    Text textB;

    LineRenderer lineRenderer;

    Vector3 oldStart;
    Vector3 iterationStart;
    Vector3 currentStart;
    Vector3 oldEnd;
    Vector3 iterationEnd;
    Vector3 currentEnd;

    Color okColor = new Color(0, 0, 255);
    Color failColor = new Color(255, 0, 0);

    private float startTime = -2;
    private float transitionTime = 0.5f;

    int k = 0;
    int b = 0;
    private float journeyLength;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<Player>();
        textK = GameObject.Find("k").GetComponent<Text>();
        textB = GameObject.Find("b").GetComponent<Text>();

        DrawNumbers();
	}

    void DrawNumbers()
    {
        textK.text = k + "";
        textB.text = b + "";

        var center = new Vector3(0, b, 1);
        var offset = new Vector3(20, 20*k, 0);

        var start = center - offset;
        var end = center + offset;
        DrawLine(start, end);
    }

    public void ReceiveMessage(string message)
    {
        string[] splitted = message.Split(' ');
        int number;

        if (splitted[0] == "k")
            number = k;
        else
            number = b;

        if (splitted[1] == "+")
            number++;
        else
            number--;

        if (splitted[0] == "k")
            k = number;
        else
            b = number;

        k = Mathf.Clamp(k, -5, 5);
        b = Mathf.Clamp(b, -5, 5);

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

        if (fracJourney == 1)
            startTime = -1;
    }

    public void Shoot()
    {
        player.Shoot(currentStart, currentEnd);
    }
}
