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
            Debug.Log(randomIndex);

            // Tüm prefab'ları devre dışı bırak.
            for (int i = 0; i < prefabList.Length; i++)
            {
                if (i == randomIndex)
                {
                    prefabList[i].SetActive(i == randomIndex);
                    if (prefabList[i].name == "HandWc")
                    {
                        prefabList[i].GetComponent<EnemySpawner>().timer = 0.3f;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>().PlaySound("Toilet");
                    } else if (prefabList[i].name == "HandRail")
                    {   
                        prefabList[i].GetComponent<EnemySpawner>().timer = 0.2f;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>().PlaySound("Train");
                    }
                    else if (prefabList[i].name == "HandGGJ2025")
                    {   
                        prefabList[i].GetComponent<EnemySpawner>().timer = 0.1f;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>().PlaySound("Keyboard");
                    } 
                    else if (prefabList[i].name == "HandHotDog")
                    {   
                        prefabList[i].GetComponent<EnemySpawner>().timer = 0.1f;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>().PlaySound("Eat");
                    }
                } else {
                    prefabList[i].SetActive(false);
                }
            }

            // Süre kadar bekle.
            yield return new WaitForSeconds(activationDuration);
        }
    }
}