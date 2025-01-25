using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EventManager : MonoBehaviour
{
    [Header("Prefab List")]
    public GameObject[] prefabList; // Prefab'larınızı buraya ekleyin.

    [Header("Activation Durations")]
    public float activationDuration = 10f; // Her prefab'ın aktif kalma süresi.

    void Start()
    {
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
            }

            // Süre kadar bekle.
            yield return new WaitForSeconds(activationDuration);
        }
    }
}