using System.Collections;
using UnityEngine;

public class WinScreenSpawner : MonoBehaviour
{
    [Header("Prefab Ayarları")]
    public GameObject winScreenPrefab; // Spawnlanacak prefab

    [Header("Spawn Noktaları")]
    public Transform[] spawnPoints; // Spawn noktaları için Empty GameObject'ler

    [Header("Spawn Sıklığı")]
    public float minSpawnInterval = 1f; // Minimum spawn süresi (saniye)
    public float maxSpawnInterval = 3f; // Maksimum spawn süresi (saniye)

    private bool spawningActive = true; // Spawn işlemi aktif mi?

    private void Start()
    {
        // Spawn sürecini başlat
        StartCoroutine(SpawnPrefabsPeriodically());
    }

    /// <summary>
    /// Belirtilen tüm spawn noktalarından rastgele prefab spawnlar.
    /// </summary>
    private IEnumerator SpawnPrefabsPeriodically()
    {
        while (spawningActive)
        {
            // Rastgele bir süre bekle
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            // Rastgele bir spawn noktası seç
            if (spawnPoints.Length > 0)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[randomIndex];

                if (spawnPoint != null && winScreenPrefab != null)
                {
                    // Prefab spawnla
                    Instantiate(winScreenPrefab, spawnPoint.position, Quaternion.identity);
                    Debug.Log($"Prefab {spawnPoint.position} konumunda spawnlandı (Bekleme süresi: {waitTime:F2} saniye).");
                }
            }
            else
            {
                Debug.LogWarning("Spawn noktası bulunamadı!");
            }
        }
    }

    /// <summary>
    /// Spawn işlemini durdurur.
    /// </summary>
    public void StopSpawning()
    {
        spawningActive = false;
        Debug.Log("Prefab spawn işlemi durduruldu.");
    }

    /// <summary>
    /// Spawn işlemini yeniden başlatır.
    /// </summary>
    public void StartSpawning()
    {
        if (!spawningActive)
        {
            spawningActive = true;
            StartCoroutine(SpawnPrefabsPeriodically());
            Debug.Log("Prefab spawn işlemi yeniden başlatıldı.");
        }
    }
}
