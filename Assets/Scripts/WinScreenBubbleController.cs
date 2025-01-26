using UnityEngine;

public class WinScreenBubbleController : MonoBehaviour
{
    // Hareket Ayarları
    public float horizontalAmplitude = 0.5f; // X ekseninde salınım genliği
    public float horizontalFrequency = 1f; // X ekseninde salınım frekansı
    public float verticalSpeed = 1f; // Y ekseninde yükselme hızı

    // Animasyon ve Ses Ayarları
    public Animator animator; // Baloncuk animatörü
    public AudioClip popSound; // Tıklanınca çalacak ses
    private AudioSource audioSource; // Ses kaynağı

    private Vector3 startPosition;
    private bool isPopped = false; // Baloncuk patladı mı kontrolü
    private float timeElapsed = 0f; // Hareket için zaman sayacı

    void Start()
    {
        // Başlangıç pozisyonunu kaydet
        startPosition = transform.position;

        // Ses kaynağını oluştur ve objeye ekle
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Eğer baloncuk patladıysa hareket etmesin
        if (isPopped) return;

        // Zamanı güncelle
        timeElapsed += Time.deltaTime;

        // X ekseninde salınım
        float xOffset = Mathf.Sin(timeElapsed * horizontalFrequency) * horizontalAmplitude;

        // Y ekseninde yükselme
        float yOffset = timeElapsed * verticalSpeed;

        // Pozisyonu güncelle
        transform.position = startPosition + new Vector3(xOffset, yOffset, 0);
    }

    void OnMouseDown()
    {
        // Eğer zaten patladıysa bir daha işlem yapma
        if (isPopped) return;

        isPopped = true;

        // Ses çal
        if (popSound != null)
        {
            audioSource.clip = popSound;
            audioSource.Play();
        }

        // Animasyonu tetikle
        if (animator != null)
        {
            // Animator'deki 'Pop' animasyonunu tetikle
            animator.SetTrigger("Pop");
        }

        // Animasyon süresi kadar bekle ve baloncuğu yok et
        float animationDuration = GetAnimationDuration("Pop");
        Destroy(gameObject, animationDuration);
    }

    // Belirli bir animasyonun süresini hesaplar
    private float GetAnimationDuration(string animationName)
    {
        if (animator == null || animator.runtimeAnimatorController == null) return 0f;

        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 0f; // Varsayılan süresi 0
    }
}
