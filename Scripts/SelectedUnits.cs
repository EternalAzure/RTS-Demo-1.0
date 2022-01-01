using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnits : MonoBehaviour
{
    public event System.Action ColourBlue;
    public event System.Action ColourRed;

    [SerializeField] private Swordmen statsR;
    [SerializeField] private Swordmen statsB;
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
        statsB.hp = (int)b;
    }
    public void SetRedHP(float r)
    {
        statsR.hp = (int)r;
    }
    public void SetRedBlock(float r)
    {
        statsR.parry = (int)r;
    }
    public void SetBlueBlock(float b)
    {
        statsR.parry = (int)b;
    }
    public void SetRedCD(float r)
    {
        statsR.cd = r/10;
    }
    public void SetBlueCD(float b)
    {
        statsR.cd = b/10;
    }

}
