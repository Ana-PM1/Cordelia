using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaDaño : MonoBehaviour
{
    public GameObject boss;
    public float daño = 1f;
    public float vida = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Llama al método TomarDaño del Boss
            BossMovimiento bossMovimiento = boss.GetComponent<BossMovimiento>();
            if (bossMovimiento != null)
            {
                bossMovimiento.TomarDaño(daño);
            }
        }

        // Puedes poner aquí chequeo opcional de vida si quieres
        if (vida <= 0)
        {
            Muerte();
        }
    }

    void Muerte()
    {
        Debug.Log("El jugador ha muerto.");
        Destroy(gameObject); // Destruye el jugador
    }

    public void TomarDaño(float cantidad)
    {
        vida -= cantidad;
        Debug.Log("Vida restante del jugador: " + vida);
        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            TomarDaño(daño);
        }
    }
}
