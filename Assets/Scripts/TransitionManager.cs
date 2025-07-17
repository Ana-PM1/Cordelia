using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    [Header("Elementos de UI de carga")]
    public GameObject canvasCarga;       
    public Text loadingText;
    public RawImage rawLoadingImage; 
    public GameObject canvasViejo;
    

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (canvasCarga != null)
            canvasCarga.SetActive(false); // Se inicia oculto
    }

    public void CargarEscenaConTransicion(string nombreEscena)
    {
        StartCoroutine(CargarEscena(nombreEscena));
    }

    private IEnumerator CargarEscena(string nombreEscena)
    {
        if (canvasCarga != null)
            canvasCarga.SetActive(true);
            canvasViejo.SetActive(false); 

        if (loadingText != null)
            loadingText.text = "Cargando...";

        if (rawLoadingImage != null)
            rawLoadingImage.gameObject.SetActive(true); // Mostrar imagen

        yield return new WaitForSeconds(1f);

        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);
        operacion.allowSceneActivation = false;

        while (!operacion.isDone)
        {
            float progreso = Mathf.Clamp01(operacion.progress / 0.9f);

            // Si quieres simular progreso con escala:
            if (rawLoadingImage != null)
                rawLoadingImage.rectTransform.localScale = new Vector3(progreso, 1f, 1f);

            if (operacion.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                operacion.allowSceneActivation = true;
            }

            yield return null;
        }
    }

}
