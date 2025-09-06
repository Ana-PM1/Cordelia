using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EjecutarCinematica : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Console.WriteLine("Se detecta JUGADOR");
            
            gameObject.SetActive(false);
            playableDirector.Play();
            
            
        }
    }
}
