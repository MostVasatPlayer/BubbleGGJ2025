using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGun : MonoBehaviour
{
    private float waitTime;
    public GameObject bubble;
    public Transform shootPos;

    void Start()
    {
        waitTime = 2f;
    }
    void Update()
    {
        waitTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && waitTime <= 0f)
        {
            Instantiate(bubble, shootPos.position, transform.rotation);
            waitTime = 2f;
        }
    }
}
