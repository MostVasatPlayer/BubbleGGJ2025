using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("Prefab List")]
    public List<GameObject> prefabList; // Prefab'larınızı buraya ekleyin.

    [Header("Activation Durations")]
    public float activationDuration = 2f; // Her prefab'ın aktif kalma süresi.

    void Start()
    {
        if (prefabList.Count > 0)
        {
            // Rastgele aktivasyon döngüsünü başlat.
            StartCoroutine(RandomPrefabActivation());
        }
        else
        {
            Debug.LogWarning("Prefab listesi boş!");
        }
    }

    private IEnumerator RandomPrefabActivation()
    {
        while (true)
        {
            // Rastgele bir prefab seç.
            int randomIndex = Random.Range(0, prefabList.Count);

            // Tüm prefab'ları devre dışı bırak.
            for (int i = 0; i < prefabList.Count; i++)
            {
                if (prefabList[i] != null)
                {
                    prefabList[i].SetActive(i == randomIndex);
                }
            }

            // Süre kadar bekle.
            yield return new WaitForSeconds(activationDuration);
        }
    }
}