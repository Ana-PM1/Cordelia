using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{

    [SerializeField] private AudioMixer volumeMixer;
    [SerializeField] private AudioMixer efectosMixer;

    public void PantallaCompleta(bool pantallaCompleta)
    {
        
        Screen.fullScreen = pantallaCompleta;
    }

    public void ModoVentana(bool modoVentana)
    {
        
        Screen.SetResolution(1280, 720, false);
    
    }

    public void CambiarVolumen(float volumen)
    {
        volumeMixer.SetFloat("Musica", volumen);
    }
    
    public void CambiarEfectos(float volumen)
    {

        efectosMixer.SetFloat("Efectos", volumen);
    }
}
