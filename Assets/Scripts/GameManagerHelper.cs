using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerHelper : MonoBehaviour
{
    public string currentLevel; // Nombre del nivel actual

    public void CargarNivel(string nombreNivel)
    {
        GameManager.Instance.CargarNivel(nombreNivel);
       
    }

    public void DesCargarNivel()
    {
        GameManager.Instance.DesCargarNivel(currentLevel);
    }

}
