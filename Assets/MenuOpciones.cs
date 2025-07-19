    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Audio;

    public class MenuOpciones : MonoBehaviour
    {

        [SerializeField] private AudioMixer volumeMixer;
        [SerializeField] private AudioMixer efectosMixer;
        [SerializeField] private TMPro.TMP_Dropdown resolucionDropdown;


        void Start()
        {
            
            // Al iniciar, cargamos las configuraciones si existen
            float volumen = PlayerPrefs.GetFloat("volumenMusica", -20f);
            CambiarVolumen(volumen);
            volumeMixer.SetFloat("Musica", volumen);

            float fx = PlayerPrefs.GetFloat("volumenEfectos", -20f);
            CambiarEfectos(fx);
            efectosMixer.SetFloat("Efectos", fx);

            int resolucion = PlayerPrefs.GetInt("resolucion", 0);
            resolucionDropdown.value = resolucion;
            resolucionDropdown.RefreshShownValue();
            CambiarResolucionPorIndice(resolucion);
            

            bool pantallaCompleta = PlayerPrefs.GetInt("pantallaCompleta", 1) == 1;
            PantallaCompleta(pantallaCompleta);

        }


        public void PantallaCompleta(bool pantallaCompleta)
        {
            
            Screen.fullScreen = pantallaCompleta;
            PlayerPrefs.SetInt("pantallaCompleta", pantallaCompleta ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void CambiarResolucion(int width, int height, bool fullscreen)
        {
            Screen.SetResolution(width, height, fullscreen);
        }

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
/*
        public void CambiarAResolucionAlta()
        {
            CambiarResolucion(1920, 1080, true);
        }

        public void CambiarAResolucionMedia()
        {
            CambiarResolucion(1280, 720, false);
        }

        public void CambiarAResolucionBaja()
        {
            CambiarResolucion(800, 600, false);
        }

*/
        public void CambiarVolumen(float volumen)
        {
            volumeMixer.SetFloat("Musica", volumen);
            PlayerPrefs.SetFloat("volumenMusica", volumen);
            PlayerPrefs.Save();
        }
        
        public void CambiarEfectos(float volumen)
        {

            efectosMixer.SetFloat("Efectos", volumen);
            PlayerPrefs.SetFloat("volumenEfectos", volumen);
            PlayerPrefs.Save();
        }
    }
