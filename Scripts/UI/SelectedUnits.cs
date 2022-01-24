using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnits : MonoBehaviour
{
    public event System.Action ColourBlue;
    public event System.Action ColourRed;

    [SerializeField] private FighterConfig statsR;
    [SerializeField] private FighterConfig statsB;
    private void Update()
    {
        //For debugging
        if (Input.GetKeyDown(KeyCode.T))
        {
            int blue = PlayerPrefs.GetInt("blue", 9);
            int red = PlayerPrefs.GetInt("red", 9);
            Debug.Log("Blue: "+blue+ " Red: " + red);
        }
    }

    public void Red(int r)
    {
        PlayerPrefs.SetInt("red", r);
        ColourRed?.Invoke();
    }
    public void Blue(int b)
    {
        PlayerPrefs.SetInt("blue", b);
        ColourBlue?.Invoke();
    }
    public void SetBlueHP(float b)
    {
        statsB.hitPoints = (int)b;
    }
    public void SetRedHP(float r)
    {
        statsR.hitPoints = (int)r;
    }
    public void SetRedBlock(float r)
    {
        statsR.parry = (int)r;
    }
    public void SetBlueBlock(float b)
    {
        statsR.parry = (int)b;
    }

}
