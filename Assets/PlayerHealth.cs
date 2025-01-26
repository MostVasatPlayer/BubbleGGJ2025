using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli

public class PlayerHealth : MonoBehaviour
{
    public Animator healthBarAnimator; // Can barı animatörü
    private int currentHealth = 5;     // Başlangıç sağlığı
    public GameObject ink;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Düşmanla çarpışmayı kontrol et
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= 1;
            healthBarAnimator.SetInteger("health", currentHealth);
            if (collision.gameObject.name == "Enemy (2)(Clone)")
            {
                ink.SetActive(true);
            }
            Destroy(collision.gameObject);
            if (currentHealth <= 0)
            {
                SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
            }
        }
    }
}
