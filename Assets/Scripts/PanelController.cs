using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{

    public GameObject panelCredits;
    public GameObject panelConfiguration;

    public void TogglePanelCredits()
    {
        panelCredits.SetActive(!panelCredits.activeSelf);
    }

    public void HidePanelCredits()
    {
        panelCredits.SetActive(false);
    }

    public void TogglePanelConfiguration()
    {
        panelConfiguration.SetActive(!panelConfiguration.activeSelf);
    }

    public void HidePanelConfiguration()
    {
        panelConfiguration.SetActive(false);
    }
}
