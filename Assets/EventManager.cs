using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EventManager : MonoBehaviour
{
    [Header("Prefab List")]
    public GameObject[] prefabList; // Prefab'larınızı buraya ekleyin.

    [Header("Activation Durations")]
    public float activationDuration = 20f; // Her prefab'ın aktif kalma süresi.

    void Start()
    {
        activationDuration = 20f;
        StartCoroutine(RandomPrefabActivation());
    }

    private IEnumerator RandomPrefabActivation()
    {
        while (true)
        {
            // Rastgele bir prefab seç.
            int randomIndex = Random.Range(0, prefabList.Length);

            // Tüm prefab'ları devre dışı bırak.
            for (int i = 0; i < prefabList.Length; i++)
            {
                prefabList[i].SetActive(i == randomIndex);
                if (prefabList[i].name == "HandWc")
                {
                    prefabList[i].GetComponent<EnemySpawner>().timer = 0.3f;
                } else if (prefabList[i].name == "HandRail")
                {   
                    prefabList[i].GetComponent<EnemySpawner>().timer = 0.2f;
                }
                else if (prefabList[i].name == "HandGGJ2025")
                {   
                    prefabList[i].GetComponent<EnemySpawner>().timer = 0.1f;
                } 
                else if (prefabList[i].name == "HandHotDog")
                {   
                    prefabList[i].GetComponent<EnemySpawner>().timer = 0.1f;
                }
            }

            // Süre kadar bekle.
            yield return new WaitForSeconds(activationDuration);
        }
    }
}