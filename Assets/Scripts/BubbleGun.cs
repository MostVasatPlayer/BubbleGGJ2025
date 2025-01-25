using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGun : MonoBehaviour
{
    public GameObject singleBubblePrefab; // Tek balon prefab'ı
    public GameObject chargedBubblePrefab; // Şarjlı balon prefab'ı
    public Transform shootPos;

    public float chargeTime = 2f; // Uzun basımda şarj süresi
    public float shotDelay = 0.5f; // Tek basımda atışlar arasındaki bekleme süresi (delay)
    public float chargedShotDelay = 0.2f; // Çoklu atıştaki balonlar arasındaki bekleme süresi (delay)
    public float chargeCooldown = 2f; // Şarjlı atış yapabilmek için bekleme süresi

    private bool canShoot = true; // Tek atış kontrolü
    private bool isCharging = false; // Şarj kontrolü
    private bool canChargeShoot = true; // Şarjlı atış yapabilme kontrolü
    private float chargeTimer = 0f; // Şarj süresi takipçisi

    void Update()
    {
        // Tek basımda bir balon atma
        if (Input.GetKeyDown(KeyCode.E) && canShoot)
        {
            Instantiate(singleBubblePrefab, shootPos.position, transform.rotation); // Tek balon at
            StartCoroutine(ShotDelay(shotDelay)); // Gecikme başlat
        }

        // Uzun basımda balon atma
        if (Input.GetKey(KeyCode.E))
        {
            if (!isCharging)
            {
                isCharging = true;
                chargeTimer = 0f; // Şarj süresini sıfırla
            }

            chargeTimer += Time.deltaTime; // Şarj süresi artıyor

            // Şarj süresi tamamlandığında çoklu balon atmaya başla
            if (chargeTimer >= chargeTime && canChargeShoot)
            {
                StartCoroutine(ShootChargedBubbles()); // Şarjlı balonları ateşle
                isCharging = false; // Şarj bitti
                StartCoroutine(ChargeCooldown()); // Şarjlı atış sonrası bekleme süresi
            }
        }

        // Tuş bırakıldığında şarj sıfırlanır
        if (Input.GetKeyUp(KeyCode.E))
        {
            isCharging = false;
            chargeTimer = 0f;
        }
    }

    // Şarjlı balonları arka arkaya atma fonksiyonu
    private IEnumerator ShootChargedBubbles()
    {
        while (chargeTimer >= chargeTime) // Şarj süresi yeterli ise
        {
            Instantiate(chargedBubblePrefab, shootPos.position, transform.rotation);
            yield return new WaitForSeconds(chargedShotDelay); // Arada bekleme süresi (çoklu atış için)
        }
    }

    // Tek basımda atışlar arasındaki gecikme (delay)
    private IEnumerator ShotDelay(float delay)
    {
        canShoot = false; // Tek atış yapılmasını engelle
        yield return new WaitForSeconds(delay); // Bekleme süresi
        canShoot = true; // Tek atış yapılabilir hale gelir
    }

    // Şarjlı atış sonrası bekleme süresi
    private IEnumerator ChargeCooldown()
    {
        canChargeShoot = false; // Şarjlı atış yapılamaz
        yield return new WaitForSeconds(chargeCooldown); // Bekleme süresi
        canChargeShoot = true; // Şarjlı atış yapılabilir hale gelir
    }
}
