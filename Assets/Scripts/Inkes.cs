using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inkes : MonoBehaviour
{
    private float timer;
    void Start()
    {
        timer = 5f;
    }
    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
