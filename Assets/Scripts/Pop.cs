using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Pop : MonoBehaviour
{
    private Transform transform;
    public int hundredPercent;
    private Rigidbody2D rb;
    public bool isPushed;
    private float waitTime;
    void Start()
    {
        
        waitTime = 0.5f;
        isPushed = false;
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        hundredPercent = 0;
    }
    void Update() {
    
        if (isPushed = true)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0f)
            {
                isPushed = false;
                waitTime = 0.5f;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bubble" && other.gameObject.GetComponent<Bubble>().waiting == true)
        {
            isPushed = true;
            Vector2 direction = (other.gameObject.transform.position - transform.position).normalized;
            rb.AddForce(direction*-2f);
            other.gameObject.GetComponent<Animator>().enabled = true;
            other.gameObject.GetComponent<Animator>().SetTrigger("Pop");
            Destroy(other.gameObject.GetComponent<Bubble>().enemyInside, 2f);
            hundredPercent += 1;
        }
    }
    private float GetAnimationDuration(Animator animator, string animationName)
    {
        if (animator == null || animator.runtimeAnimatorController == null) {return 0f;}
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 0f;
    } 
}
