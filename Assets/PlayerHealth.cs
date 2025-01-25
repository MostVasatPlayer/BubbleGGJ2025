using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Animator healthBarAnimator; // Can barı animatörü
    private int currentHealth = 5;     // Başlangıç sağlığı

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Düşmanla çarpışmayı kontrol et
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--; // Can azalt
            PlayHealthAnimation(); // İlgili animasyonu oynat
        }

        if (currentHealth <= 0)
        {
            // Oyuncu öldü
            Debug.Log("Player is Dead!");
            HandleDeath();
        }
    }

    private void PlayHealthAnimation()
    {
        // Can seviyesine uygun animasyon adını oluştur
        int animationIndex = currentHealth - 1; // Hp4 5 can, Hp3 4 can, vb.
        string animationName = "Hp" + animationIndex;
        healthBarAnimator.Play(animationName);
        Debug.Log($"Playing animation: {animationName}");
    }

    private void HandleDeath()
    {
        // Ölüm durumunda yapılacak işlemler
        Debug.Log("Game Over! Restart or show death screen.");
    }
}
