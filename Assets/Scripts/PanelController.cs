using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{

    public GameObject panelCredits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePanelCredits()
    {
        panelCredits.SetActive(!panelCredits.activeSelf);
    }

    public void HidePanelCredits()
    {
        panelCredits.SetActive(false);
    }
}
