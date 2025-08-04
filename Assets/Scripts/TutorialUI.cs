using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [Header("Texto asociado a este trigger")]
    public GameObject tutorialText; // Texto que se activará al entrar

    void Start()
    {
        if (tutorialText != null)
            tutorialText.SetActive(false); // Asegurarse que está desactivado al inicio
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tutorialText != null)
        {
            tutorialText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && tutorialText != null)
        {
            tutorialText.SetActive(false);
        }
    }
}
