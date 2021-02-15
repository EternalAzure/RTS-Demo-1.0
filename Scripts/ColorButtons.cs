using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorButtons : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private Material red = null;
    [SerializeField] private Material blue = null;
    [SerializeField] private GameObject scriptParent;
    private void Start()
    {
        scriptParent.GetComponent<SelectedUnits>().ColourBlue += ColourBlue;
        scriptParent.GetComponent<SelectedUnits>().ColourRed += ColourRed;
        grid = GetComponent<GridLayoutGroup>();

        ColourBlue();
        ColourRed();
    }
    private void ColourBlue()
    {
        if (transform.tag != "Blue") return;
        GameObject go = grid.gameObject;
        Button[] b = go.GetComponentsInChildren<Button>();
        for (int i = 0; i < PlayerPrefs.GetInt("blue", 9); i++)
        {
            ColorBlock colors = b[i].colors;
            colors.normalColor = blue.color;
            b[i].colors = colors;
        }
        for (int i = PlayerPrefs.GetInt("blue", 9); i < b.Length; i++)
        {
            ColorBlock colors = b[i].colors;
            colors.normalColor = Color.white;
            b[i].colors = colors;
        }
    }

    private void ColourRed()
    {
        if (transform.tag != "Red") return;
        GameObject go = grid.gameObject;
        Button[] b = go.GetComponentsInChildren<Button>();
        for (int i = 0; i < PlayerPrefs.GetInt("red", 9); i++)
        {
            ColorBlock colors = b[i].colors;
            colors.normalColor = red.color;
            b[i].colors = colors;
        }
        for (int i = PlayerPrefs.GetInt("red", 9); i < b.Length; i++)
        {
            ColorBlock colors = b[i].colors;
            colors.normalColor = Color.white;
            b[i].colors = colors;
        }
    }
}
