    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Audio;

    public class MenuOpciones : MonoBehaviour
    {
        // Este script controla el menú de opciones del juego
        // Permite al jugador ajustar configuraciones como pantalla completa, resolución y volumen
        // Los cambios se guardan en PlayerPrefs para persistencia entre sesiones
        [Header("Opciones de Configuración")]
        [SerializeField] private AudioMixer volumeMixer;
        [SerializeField] private AudioMixer efectosMixer;
        [SerializeField] private TMPro.TMP_Dropdown resolucionDropdown;



        //Opciones de pantalla
        public void PantallaCompleta(bool pantallaCompleta)
        {
            
            Screen.fullScreen = pantallaCompleta;
            PlayerPrefs.SetInt("pantallaCompleta", pantallaCompleta ? 1 : 0);// Guardar estado de pantalla completa
            PlayerPrefs.Save();
        }
        // Método para cambiar la resolución
        // Utiliza Screen.SetResolution para cambiar la resolución del juego
        // Guarda la resolución seleccionada en PlayerPrefs para persistencia
        // El método CambiarResolucionPorIndice se llama desde un dropdown en la UI
        private void CambiarResolucion(int width, int height, bool fullscreen)
        {
            Screen.SetResolution(width, height, fullscreen);
        }
        // Método para cambiar la resolución según el índice del dropdown
        // Utiliza PlayerPrefs para guardar el índice seleccionado
        public void CambiarResolucionPorIndice(int index)
        {
            Debug.Log("Índice recibido del dropdown: " + index);
            PlayerPrefs.SetInt("resolucion", index);
            PlayerPrefs.Save();
            switch (index)
            {
                case 0:
                    CambiarResolucion(1920, 1080, true);
                    Debug.Log("Resolución cambiada a 1920x1080");
                    break;
                case 1:
                    CambiarResolucion(1280, 720, false);
                    Debug.Log("Resolución cambiada a 1280x720");
                    break;
                case 2:
                    CambiarResolucion(800, 600, false);
                    Debug.Log("Resolución cambiada a 800x600");
                    break;
            }
        }
        // Método para cambiar el volumen de la música
        // Utiliza AudioMixer para ajustar el volumen de la música
        // Guarda el volumen en PlayerPrefs para persistencia
        // El método CambiarVolumen se llama desde un slider en la UI
        public void CambiarVolumen(float volumen)
        {
            volumeMixer.SetFloat("Musica", volumen);
            PlayerPrefs.SetFloat("volumenMusica", volumen);
            PlayerPrefs.Save();
        }
        // Método para cambiar el volumen de los efectos de sonido
        // Utiliza AudioMixer para ajustar el volumen de los efectos de sonido
        // Guarda el volumen en PlayerPrefs para persistencia
        public void CambiarEfectos(float volumen)
        {

            efectosMixer.SetFloat("Efectos", volumen);
            PlayerPrefs.SetFloat("volumenEfectos", volumen);
            PlayerPrefs.Save();
        }
    }
