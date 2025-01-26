using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BubbleShield : MonoBehaviour
{
    public GameObject player;
    private Transform transform;
    private float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = 4f;
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.GetComponent<Transform>().position;
        waitTime -= Time.deltaTime;
        if (waitTime <= 0f)
        {
            player.GetComponent<PlayerShield>().isOpen = false;
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.name == "Enemy (3)(Clone)" && other.gameObject.GetComponent<EnemyCharRotate>().halfLeft == false)
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce((other.gameObject.transform.position - transform.position)*5f, ForceMode2D.Impulse);
                other.gameObject.GetComponent<EnemyCharRotate>().halfLeft = true;
            } else {
                Destroy(other.gameObject);
            }
        }
    }
}
