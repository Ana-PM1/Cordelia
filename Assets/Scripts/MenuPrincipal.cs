using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    
    [SerializeField] private Button botonContinuar;
    public PlayerData verPlayerData; // Instancia de PlayerData para almacenar los datos del jugador

    private void Start()
    {
        // Si hay datos guardados, activar botón continuar
        if (GameFlowManager.Instance.HayPartidaGuardada())
        {
            botonContinuar.gameObject.SetActive(true);
        }
        
    }
    // Método para cargar la escena del juego
    // Si hay datos guardados, carga la escena guardada
    public void Continuar()
    {
        PlayerData data = SaveManager.LoadPlayer();
        verPlayerData = SaveManager.LoadPlayer(); // Asigna los datos cargados a la instancia de PlayerData
        if (data != null)
        {
            TransitionManager.Instance.CargarEscenaConTransicion(data.escenaNombre); // nombre de la escena guardada
        }
    }
    // Método para iniciar un nuevo juego
    // Limpia los datos guardados y carga la primera escena del juego
    public void IniciarNuevoJuego()
    {
        GameFlowManager.Instance.IniciarNuevoJuego();
    }
    // Método para salir del juego
    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Juego cerrado.");
    }
    
}
