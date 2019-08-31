using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    public List<GameObject> panels;

    public void Select(int panelIndex)
    {
        if (panelIndex < 0 || panelIndex >= panels.Count || !panels[panelIndex])
            return;
        foreach (GameObject panel in panels)
            panel.SetActive(false);
        panels[panelIndex].SetActive(true);
    }
}
