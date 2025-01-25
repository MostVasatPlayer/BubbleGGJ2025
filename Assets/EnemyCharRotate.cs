using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharRotate : MonoBehaviour
{
    private Transform transform;
    public bool halfLeft;
    private Animator animator;
    void Start()
    {
        halfLeft = false;
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90f);
    }
    void Update()
    {
        animator.SetBool("isHalf", halfLeft);
    }
}
