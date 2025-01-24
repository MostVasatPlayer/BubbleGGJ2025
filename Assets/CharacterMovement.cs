using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Karakterin hareket hızı
    public float jumpForce = 10f; // Zıplama kuvveti

    private Rigidbody2D rb;  // Rigidbody2D bileşeni
    private bool isGrounded; // Karakterin platforma temas edip etmediğini kontrol eder
    private float moveInput; // Klavyeden gelen giriş değeri

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Klavyeden giriş al ("A/D" veya "Sol/sağ ok tuşları")
        moveInput = Input.GetAxis("Horizontal");

        // Eğer karakter platformdaysa ve boşluk tuşuna basılmışsa, zıpla
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false; // Zıpladıktan sonra yere temas kontrolü pasif hale gelir
        }
    }

    void FixedUpdate()
    {
        // Karakteri hareket ettir
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Eğer temas edilen nesne "Platform" etiketi taşıyorsa, isGrounded'ı true yap
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Platformdan ayrıldığında isGrounded'ı false yap
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}