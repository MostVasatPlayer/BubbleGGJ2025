using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndScaleManager : MonoBehaviour
{
    [Header("Dönüş ve Ölçek Ayarları")]
    public float radius = 2f; // Dönüş yarıçapı
    public float scaleSpeed = 2f; // Ölçeklendirme hızı
    public float scaleAmount = 0.5f; // Ölçek değişim miktarı

    [Header("Döndürülecek Nesneler")]
    public List<Transform> objectsToRotate = new List<Transform>(); // Döndürülüp ölçeklendirilecek nesneler
    public List<float> rotationSpeeds = new List<float>(); // Her nesne için dönüş hızı

    private List<float> angles = new List<float>(); // Her nesne için açı
    private List<Vector3> initialPositions = new List<Vector3>(); // Her nesnenin başlangıç pozisyonu
    private List<Vector3> initialScales = new List<Vector3>(); // Her nesnenin başlangıç ölçeği
    private List<int> directions = new List<int>(); // Her nesne için dönüş yönü

    void Start()
    {
        // Nesneler için başlangıç değerlerini kaydet
        foreach (Transform obj in objectsToRotate)
        {
            if (obj != null)
            {
                initialPositions.Add(obj.position);
                initialScales.Add(obj.localScale);
                angles.Add(0f); // Başlangıç açısı
                directions.Add(Random.value > 0.5f ? 1 : -1); // Rastgele dönüş yönü
            }
        }

        // rotationSpeeds listesine dönüş hızlarını eklemek için kontrol
        if (rotationSpeeds.Count < objectsToRotate.Count)
        {
            Debug.LogWarning("Dönüş hızları eksik, her nesneye dönüş hızı ekleyin.");
        }
    }

    void Update()
    {
        // Her nesne için dönüş ve ölçek işlemleri
        for (int i = 0; i < objectsToRotate.Count; i++)
        {
            Transform obj = objectsToRotate[i];
            if (obj == null) continue;

            // Eğer rotationSpeeds listesinde hız değeri yoksa varsayılan bir hız belirle
            float currentSpeed = (rotationSpeeds.Count > i) ? rotationSpeeds[i] : 30f;

            // Açı güncellemesi (dönüş yönüne göre)
            angles[i] += directions[i] * currentSpeed * Time.deltaTime;
            float radians = angles[i] * Mathf.Deg2Rad;

            // Yeni pozisyonu hesapla
            Vector3 offset = new Vector3(Mathf.Cos(radians) * radius, Mathf.Sin(radians) * radius, 0);
            obj.position = initialPositions[i] + offset;

            // Ölçeği büyütüp küçült
            float scaleOffset = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
            obj.localScale = initialScales[i] + new Vector3(scaleOffset, scaleOffset, scaleOffset);
        }
    }
}