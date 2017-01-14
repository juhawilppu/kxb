using UnityEngine;
using System.Collections;
using System;

class DestroyThis : MonoBehaviour {

    public float lifeTime = 100f;

    void Start() {
        Destroy(gameObject, lifeTime);
    }

}