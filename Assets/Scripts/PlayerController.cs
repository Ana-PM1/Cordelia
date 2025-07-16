using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed = 5f;
    [SerializeField] 
    private float jumpForce = 5f;


    private Rigidbody rb;
    private int jumpCount = 0;
    private int maxJumps = 2;
    private bool isGrounded = false;

    public bool isOnRope = false;

    [SerializeField]
    private float vidas = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoPersonaje();
        SaltarPersonaje();
    }

    private void MovimientoPersonaje()
    {

        if (isOnRope)
        {
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(0, verticalInput, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);  
        } 
    }

    private void SaltarPersonaje()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps && !isOnRope)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
            Debug.Log("En Suelo");
        }
    }

    public void TakeDamage(float damage)
    {
        vidas -= damage;
        Debug.Log($"{gameObject.name} recibió daño. Vidas restantes: {vidas}");
        if (vidas <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
        Destroy(gameObject);
    }

    public void GanarVida(float cantidad)
    {
        vidas += cantidad;
        Debug.Log("Vida aumentada. Vidas actuales: " + vidas);
    }


}
