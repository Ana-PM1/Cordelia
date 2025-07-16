using UnityEngine;

public class GameManager : MonoBehaviour
{
    



    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private EnemyController[] enemigos;

    

    private void Update()
    {

        // Prueba: Q le hace daño al jugador
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.TakeDamage(1);
        }

        // Prueba: T le hace daño a todos los enemigos
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (var enemigo in enemigos)
            {
                enemigo.TakeDamage(1);
            }
        }
    }


    
}
