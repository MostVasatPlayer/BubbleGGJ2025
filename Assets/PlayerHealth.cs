using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli

public class PlayerHealth : MonoBehaviour
{
    public Animator healthBarAnimator; // Can barı animatörü
    private int currentHealth = 4;     // Başlangıç sağlığı
    public Animator damagedAnimator;   // "Damaged" animasyonunu oynatan animatör

    public GameObject ink;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Düşmanla çarpışmayı kontrol et
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= 1;
            healthBarAnimator.SetInteger("health", currentHealth);

            // Düşman ismi kontrolü ve ink objesini aktif etme
            if (collision.gameObject.name == "Enemy (2)(Clone)")
            {
                ink.SetActive(true);
            }

            // "Damaged" animasyonunu tetikle
            damagedAnimator.SetTrigger("Damaged");

            Destroy(collision.gameObject);

            // Sağlık sıfır veya daha düşükse "WinScene" sahnesine geçiş yap
            if (currentHealth <= 0)
            {
                SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
            }
        }
    }
}
