using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    
    [Header("Movimiento")]
    [SerializeField] 
    private float moveSpeed = 5f;
    [SerializeField] 
    private float jumpForce = 5f;

    
    private Rigidbody rb;
    private Animator animator;
    private int jumpCount = 0;
    private int maxJumps = 2;
    private bool isGrounded = false;

    public bool isOnRope = false;

    public float vidas = 3f;

    private float ultimaVelocidadY;
    [SerializeField] private float umbralDañoCaída = -20f;
    [SerializeField] private float multiplicadorDaño = 1f;

    

    [SerializeField]
    private Transform modeloVisual;
    private bool mirandoIzquierda = false;

    public GameObject[] vidasUi;
     

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();


        PlayerData datos = SaveManager.LoadPlayer(); // Carga los datos del jugador guardados
        // Si hay datos guardados y la escena es la misma que la guardada, cargar los datos
        // Si no, buscar el respawn por defecto
        if (datos != null)
        {
            vidas = datos.vidas;
            Vector3 pos = new Vector3(datos.posicion[0], datos.posicion[1], datos.posicion[2]);
            transform.position = pos;
            Debug.Log("Datos del jugador cargados");
        }
        else
        {
            // Buscar el respawn en la escena
            GameObject respawn = GameObject.FindGameObjectWithTag("Respawn");
            if (respawn != null)
            {
                transform.position = respawn.transform.position;
                Debug.Log("Respawn por defecto establecido");
            }
        }

        ActualizarUIVida(); // Actualiza la UI de vida al iniciar
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoPersonaje();
        SaltarPersonaje();
        ultimaVelocidadY = rb.velocity.y;
    }
    
    // Movimiento del personaje
    // Si está en cuerda, se mueve verticalmente, si no, horizontalmente
    private void MovimientoPersonaje()
    {

        if (isOnRope)
        {
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(0, verticalInput, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
        else if(!isOnRope && isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
            animator.SetFloat("velocidadMov", Mathf.Abs(horizontalInput));
            if (horizontalInput != 0)
            {
                MirarDireccion(horizontalInput);
            }
        } 
    }
    // Salto del personaje
    // Si está en cuerda, no puede saltar
    // Si no, puede saltar hasta x veces
    private void SaltarPersonaje()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps && !isOnRope)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            animator.SetBool("saltando", isGrounded);
        }
    }

    private void MirarDireccion(float horizontalInput)
    {
        if ((horizontalInput < 0 && !mirandoIzquierda) || (horizontalInput > 0 && mirandoIzquierda))
        {
            mirandoIzquierda = !mirandoIzquierda;
    
            modeloVisual.eulerAngles = new Vector3(0, mirandoIzquierda ? 270 : 90, 0);
        }
    }
    
    // Si colisiona con el suelo, puede saltar de nuevo
    // Si la velocidad en Y es menor que el umbral de daño por caída, recibe daño
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
            animator.SetBool("saltando", !isGrounded);

            if (ultimaVelocidadY < umbralDañoCaída)
            {
                float daño = Mathf.Abs(ultimaVelocidadY) * multiplicadorDaño;
                TakeDamage(daño);
                Debug.Log("Daño por caída: " + daño);
            }
        }
    }
    //Recibe daño al jugador
    // Si las vidas llegan a 0, muere
    public void TakeDamage(float damage)
    {
        vidas -= damage;
        Debug.Log($"{gameObject.name} recibió daño. Vidas restantes: {vidas}");
        ActualizarUIVida();
        if (vidas <= 0)
        {
            Die();
        }
    }
    //muere el jugador
    private void Die()
    {
        Debug.Log("Player died");
        moveSpeed = 0f; // Detiene el movimiento
        jumpForce = 0f; // Detiene el salto
        rb.velocity = Vector3.zero; // Detiene la física
        MirarDireccion(0); // Asegura que el personaje no se mueva al morir
        animator.SetTrigger("morir");
        StartCoroutine(EsperarYMorir());
        
        

    }
    private IEnumerator EsperarYMorir()
    {
        yield return new WaitForSeconds(2.8f); // duración del clip
        Destroy(gameObject);
    }
    // Gana vida al jugador
    public void GanarVida(float cantidad)
    {
        vidas += cantidad;
        Debug.Log("Vida aumentada. Vidas actuales: " + vidas);
        ActualizarUIVida();     
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SiguienteNivel"))
        {
            GameManager.Instance.SiguienteNivel();
        }
    }
    private void ActualizarUIVida()
    {
        for (int i = 0; i < vidasUi.Length; i++)
        {
            if (i < vidas)
            {
                vidasUi[i].SetActive(true);
            }
            else
            {
                vidasUi[i].SetActive(false);
            }
        }
    }


    
    



}
