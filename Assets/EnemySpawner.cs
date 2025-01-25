using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Spawnlamak istediğiniz prefab.
    public GameObject prefab;

    // Spawn noktalarını içeren bir dizi.
    public Transform[] spawnPoints;

    // Prefab'ın spawn olma sıklığı (saniye).
    public float spawnInterval = 2.0f;

    // Spawnlama işlemini otomatik başlatmak için bir kontrol.
    public bool autoSpawn = true;

    private float timer;

    void Update()
    {
        if (autoSpawn)
        {
            // Zamanlayıcıyı günceller.
            timer += Time.deltaTime;

            // SpawnInterval süresi geçtiğinde prefab spawnlanır.
            if (timer >= spawnInterval)
            {
                SpawnPrefabAtRandomPoint();
                timer = 0f;
            }
        }
    }

    void SpawnPrefabAtRandomPoint()
    {
        if (prefab != null && spawnPoints.Length > 0)
        {
            // Rastgele bir spawn noktası seçilir.
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform selectedPoint = spawnPoints[randomIndex];

            // Prefab spawnlanır.
            Instantiate(prefab, selectedPoint.position, selectedPoint.rotation);
            Debug.Log($"Prefab spawnlandı! Spawn noktası: {selectedPoint.name}");
        }
        else
        {
            Debug.LogWarning("Prefab veya spawn noktaları atanmadı!");
        }
    }

    // İsteğe bağlı: Spawn işlemini manuel tetiklemek için bir tuş atanabilir.
    public void ManualSpawn()
    {
        SpawnPrefabAtRandomPoint();
    }
}