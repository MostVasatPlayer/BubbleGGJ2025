using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndScale : MonoBehaviour
{
    public float rotationSpeed = 30f; // Dönüş hızı
    public float radius = 2f; // Dönüş yarıçapı
    public float scaleSpeed = 2f; // Ölçeklendirme hızı
    public float scaleAmount = 0.5f; // Ölçek değişim miktarı

    private float angle = 0f; // Şu anki açı
    private Vector3 centerPoint; // Başlangıçtaki merkez noktası
    private Vector3 initialScale; // Başlangıçtaki ölçek
    private int direction; // Dönüş yönü: 1 (saat yönü), -1 (saat yönü tersi)

    void Start()
    {
        // Başlangıç merkez noktasını kaydet
        centerPoint = transform.position;

        // Başlangıç ölçeğini kaydet
        initialScale = transform.localScale;

        // Dönüş yönünü rastgele belirle
        direction = Random.value > 0.5f ? 1 : -1; // %50 ihtimalle saat yönü ya da tersine döner
    }

    void Update()
    {
        // Açı güncellemesi (dönüş yönüne göre)
        angle += direction * rotationSpeed * Time.deltaTime;
        float radians = angle * Mathf.Deg2Rad;

        // Yeni pozisyonu hesapla (X ve Y düzlemi üzerinde sınırla)
        Vector3 offset = new Vector3(Mathf.Cos(radians) * radius, Mathf.Sin(radians) * radius, 0);
        transform.position = centerPoint + offset;

        // Ölçeği büyütüp küçült
        float scaleOffset = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = initialScale + new Vector3(scaleOffset, scaleOffset, scaleOffset);
    }
}