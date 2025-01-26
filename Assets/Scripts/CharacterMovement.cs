using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Karakterin hareket hızı
    public float jumpForce = 10f; // Zıplama kuvveti
    public Transform groundCheck; //Karakterin ayağı
    public LayerMask groundLayer; //Platformları tanımlamak için
    public LayerMask bubbleLayer;
    public Transform transform; //Karakterin konum bilgisi
    private Rigidbody2D rb;  // Rigidbody2D bileşeni
    private float moveInput; // Klavyeden gelen giriş değeri
    private float radius; //Karakterin yer kontrolü için yarattığı dairenin yarıçapı
    private bool pushed;
    private float pushWaitTime;
    public int hundredPercent; 
    public UnityEngine.UI.Image img;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        pushed = false;
        pushWaitTime = 0.5f;
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        img.fillAmount = hundredPercent/100;
    }

    void Update()
    {
        if (pushed == false)
        {
             // Klavyeden giriş al ("A/D" veya "Sol/sağ ok tuşları")
            moveInput = Input.GetAxis("Horizontal");

            // Eğer karakter platformdaysa ve boşluk tuşuna basılmışsa, zıpla
            if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>().PlaySound("Jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            if (rb.velocity.y > 0f && Input.GetKeyUp(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
            }
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); //Karakterin hızını belirleme
            Flip();
        } else if (pushed == true)
        {
            pushWaitTime -= Time.deltaTime;
            if (pushWaitTime <= 0f)
            {
                pushed = false;
                pushWaitTime = 0.5f;
            }
        }
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
        if (hundredPercent >= 100)
        {
            SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
        } else {
            img.fillAmount = (float)hundredPercent/100f;
        }
    }

    private bool isGrounded()//Yerde olup olmadığını kontrol etme
    {
        bool returnValue = false;
        if (Physics2D.OverlapCircle(groundCheck.position, 0.23f, groundLayer) == true)
        {
            returnValue = true;
        } 
        if (Physics2D.OverlapCircle(groundCheck.position, 0.23f, bubbleLayer) == true)
        {
            returnValue = true;
        }
        return returnValue;
    }

    private void Flip()//Hareketen yönüne dönük
    {
        if (rb.velocity.x < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        } else if (rb.velocity.x > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    void OnDrawGizmos() //Debug için çizim
    {
        Gizmos.color = Color.green; 
        int segments = 100; 
        float angleStep = 360f / segments;
        Vector3 prevPoint = groundCheck.position + new Vector3(radius, 0, 0);
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 newPoint = groundCheck.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            Gizmos.DrawLine(prevPoint, newPoint); 
            prevPoint = newPoint; 
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bubble" && other.gameObject.GetComponent<Bubble>().waiting == true)
        {
            pushed = true;
            Vector2 direction = (other.gameObject.transform.position - transform.position).normalized;
            rb.AddForce(direction*-200f);
            Destroy(other.gameObject.GetComponent<Bubble>().enemyInside);
            other.gameObject.GetComponent<Animator>().enabled = true;
            other.gameObject.GetComponent<Animator>().SetTrigger("Pop");
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>().PlaySound("Popping");
            Destroy(other.gameObject, 0.2f);
            hundredPercent += 5;
        }
    }
}