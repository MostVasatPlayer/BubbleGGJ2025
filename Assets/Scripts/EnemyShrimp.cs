using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShrimp : MonoBehaviour
{
    private Vector2 direction;
    private Rigidbody2D rb;
    private Transform transform;
    public Transform player;
    private float force;
    private float waitTime;
    private float forceTime;
    private bool forceApplied;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        waitTime = 0f;
        forceTime = 1f;
        forceApplied = false;
        force = 2f;
    }
    void Update()
    {
        direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        rb.rotation = angle+90f;
        if (forceApplied == false)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0f)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.AddForce(direction*force, ForceMode2D.Impulse);
                forceApplied = true;
                waitTime = 0.5f;
            }
        } else if (forceApplied == true){
            forceTime -= Time.deltaTime;
            if (forceTime <= 0f)
            {
                rb.velocity = new Vector2(0f,0f);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                forceTime = 1f;
                forceApplied = false;
            }
        }
    }
}
