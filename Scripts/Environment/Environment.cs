using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Environment : MonoBehaviour
{
    public int objectivesCount = 0;
    public int fallenObjectives = 0;
    public List<Transform> objectives = new List<Transform>(); // in Unity Editor

    void Awake()
    {
        if (objectives != null) objectivesCount = objectives.Count;
    }

    public void IncrementFallenObjectives()
    {
        fallenObjectives++;
        if (fallenObjectives >= objectivesCount) EndGame();
    }

    private void EndGame()
    {
        Time.timeScale = 0; // Stop game
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        GameObject endScreen = canvas.transform.GetChild(1).gameObject;
        while (!endScreen.activeSelf) endScreen.SetActive(true);

        Transform textObject = endScreen.transform.GetChild(0);
        textObject.GetComponent<Text>().text = "Game Over";
    }

    public List<Transform> GetObjectives()
    {
        return objectives;
    }
}
