using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Spawnlamak istediğiniz prefab.
    public GameObject[] prefabList;
    // Spawn noktalarını içeren bir dizi.
    public Transform[] spawnPoints;
    // Spawnlama işlemini otomatik başlatmak için bir kontrol.
    public bool autoSpawn = true;
    public float timer;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ground"), LayerMask.NameToLayer("Enemy"), true);
        timer = 5f;
    }

    void Update()
    {
        if (autoSpawn)
        {
            // Zamanlayıcıyı günceller.
            timer -= Time.deltaTime;

            // SpawnInterval süresi geçtiğinde prefab spawnlanır.
            if (timer <= 0f)
            {
                SpawnPrefabAtRandomPoint();
                timer = 5f;
            }
        }
    }

    void SpawnPrefabAtRandomPoint()
    {
        // Rastgele bir spawn noktası seçilir.
        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Transform selectedPoint = spawnPoints[randomIndex];
        randomIndex = UnityEngine.Random.Range(0, prefabList.Length);
        GameObject prefab = prefabList[randomIndex];

        // Prefab spawnlanır.
        GameObject character = Instantiate(prefab, selectedPoint.position, selectedPoint.rotation);
        if (character.name == "Enemy(Clone)")
        {
            character.GetComponent<EnemyShrimp>().player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        } else {
            character.GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }
}