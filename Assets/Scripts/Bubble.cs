using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private float initialX = 25f;
    private float initialY = 3f;
    public Rigidbody2D rb;
    private SpriteRenderer img;
    private float ymax;
    private Transform player; //Oyuncu
    public Transform transform; //Baloncuk
    public Sprite[] sprites;
    public GameObject enemyInside;
    private float timeWait;
    public bool waiting;
    private CircleCollider2D boxCollider;
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        img = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<CircleCollider2D>();
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        foreach (GameObject bubble in bubbles)
        {
            Collider2D collider = bubble.GetComponent<CircleCollider2D>();
            if (collider != null && boxCollider != null)
            {
                Physics2D.IgnoreCollision(boxCollider, collider);
            }
        }
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ground"), LayerMask.NameToLayer("Bubble"), true);
        waiting = false;
        timeWait = 0f;
        ymax = 5f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb.velocity = new Vector2(initialX*Mathf.Sign(player.localScale.x), initialY);
    }
    void Update()
    {
        if (waiting != true)
        {
            rb.velocity = new Vector2(rb.velocity.x*0.99f, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(rb.velocity.x*0.9f, rb.velocity.y*0.9f);
        }
        if (transform.position.y >= ymax)
        {
            Destroy(gameObject);
        }
        if (waiting == true)
        {
            timeWait -= Time.deltaTime;
            if (timeWait <= 0f)
            {
                if (enemyInside.name == "Enemy (3)(Clone)")
                {
                    enemyInside.GetComponent<EnemyCharRotate>().halfLeft = false;
                } else if (enemyInside.name == "Enemy(Clone)")
                {
                    float angle = UnityEngine.Random.Range(0 , Mathf.PI);
                    float distance = UnityEngine.Random.Range(0, 1f);
                    Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))*distance;
                    Vector2 pos = offset + (Vector2)transform.position;
                    GameObject newObj = Instantiate(enemyInside, pos, transform.rotation);
                    newObj.SetActive(true);
                    newObj.name = "Enemy(Clone)";
                }
                enemyInside.SetActive(true);
                enemyInside.GetComponent<Transform>().position = transform.position;
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (waiting == false && other.gameObject.activeSelf == true && other.gameObject.tag == "Enemy")
        {   
            bool goingToBeActive = false;
            enemyInside = other.gameObject;
            timeWait = 10f;
            rb.velocity = new Vector2(0f, 0f);
            if (other.gameObject.name == "Enemy(Clone)")
            {
                img.sprite = sprites[0];
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>().PlaySound("Absorb");
            } else if (other.gameObject.name == "Enemy (1)(Clone)")
            {
                img.sprite = sprites[1];
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>().PlaySound("Absorb");
            } else if  (other.gameObject.name == "Enemy (2)(Clone)")
            {
                img.sprite = sprites[2];
                timeWait = 5f;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>().PlaySound("Absorb");
            }else if (other.gameObject.name == "Enemy (3)(Clone)")
            {
                img.sprite = sprites[3];
                if (enemyInside.GetComponent<EnemyCharRotate>().halfLeft != true)
                {
                    goingToBeActive = true;
                    enemyInside.GetComponent<EnemyCharRotate>().halfLeft = true;
                    Destroy(gameObject);
                }
            }
            enemyInside.SetActive(goingToBeActive);
            waiting = true;
        }
    } 
}
