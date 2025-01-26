using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BubbleGun : MonoBehaviour
{
    public GameObject singleBubblePrefab; // Tek balon prefab'ı
    public GameObject chargedBubblePrefab; // Şarjlı balon prefab'ı
    public Transform shootPos;
    public float waitTime;
    public float chargeTime = 2f; // Uzun basımda şarj süresi
    public float timeWait = 0f;
    private bool waiting = false;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        timeWait = 0f;
        waiting = false;
        waitTime = 2f;    
    }
    void Update()
    {
        waitTime -= Time.deltaTime;
        // Tek basımda bir balon atma
        if (Input.GetKeyDown(KeyCode.E))
        {
            waiting = true;
        }

        if (waiting == true) //Basılma süresi
        {
            timeWait += Time.deltaTime;
        }

        // Tuş bırakıldığında şarj sıfırlanır
        if (Input.GetKeyUp(KeyCode.E) && waiting == true && waitTime <= 0f)
        {
            if (timeWait >= chargeTime)
            {
                animator.SetTrigger("Fire");
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>().PlaySound("Create");
                Instantiate(chargedBubblePrefab, shootPos.position, transform.rotation);
            } else {
                animator.SetTrigger("Fire");
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>().PlaySound("Create");
                Instantiate(singleBubblePrefab, shootPos.position, transform.rotation); // Tek balon at
            }
            waitTime = 2f;
            waiting = false;
            timeWait = 0f;
        }
    }
}
