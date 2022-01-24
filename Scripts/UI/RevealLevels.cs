using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealLevels : MonoBehaviour
{
    public Transform campaignMapPanel;

    void Start()
    {
        campaignMapPanel = transform.GetChild(0);
    }

    // Save int level to PlayerPrefs and iterate when opening game after saving and quiting
    public void RevealLevel(int level)
    {
        campaignMapPanel.GetChild(level).gameObject.SetActive(true);
    }
}
