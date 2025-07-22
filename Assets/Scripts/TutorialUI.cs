using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public GameObject hudText; // Referencia al texto del HUD
    public KeyCode keyToPress = KeyCode.E;

    private bool playerInRange = false;
    private bool hasInteracted = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasInteracted)
        {
            hudText.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hudText.SetActive(false);
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && !hasInteracted && Input.GetKeyDown(keyToPress))
        {
            // Acción que quieres que ocurra
            Debug.Log("Interacción realizada");

            hasInteracted = true;
            hudText.SetActive(false);
        }
    }
}
