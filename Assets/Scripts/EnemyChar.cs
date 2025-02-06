using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChar : MonoBehaviour
{
    public AIPath aiPath;
    private Transform transform;
    void Start() {
        transform = GetComponent<Transform>();
        aiPath = GetComponent<AIPath>();
    }
    void Update(){
        if(aiPath.desiredVelocity.x >= 0.01){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (aiPath.desiredVelocity.x <= -0.01f){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
