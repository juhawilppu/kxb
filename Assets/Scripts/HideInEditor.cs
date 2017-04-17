using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HideInEditor : MonoBehaviour
{
    void OnEnable()
    {
        transform.gameObject.SetActive(true);
    }
}
