using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private float initialX = 25f;
    private float initialY = 3f;
    public Rigidbody2D rb;
    public GameObject inside;
    private SpriteRenderer img;
    private float ymax;
    private Transform player; //Oyuncu
    public Transform transform; //Baloncuk
    void Start()
    {
        ymax = 5f;
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        img = inside.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb.velocity = new Vector2(initialX*Mathf.Sign(player.localScale.x), initialY);
    }
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x*0.99f, rb.velocity.y);
        if (transform.position.y >= ymax)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            img.sprite = other.gameObject.GetComponent<SpriteRenderer>().sprite;
            Color fadedColor = img.color;
            fadedColor.a = 0.5f;
            img.color = fadedColor;
            inside.GetComponent<Transform>().localScale *= 0.5f;
        }
    }
}
