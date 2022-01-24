using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void Quit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Back()
    {
        int lastScene = PlayerPrefs.GetInt("lastScene", 0);
        SceneManager.LoadScene(0);
    }
    
    public void MainMenu()
    {
        PlayerPrefs.SetInt("lastScene", 0);
        SceneManager.LoadScene(0);
    }

    public void Settings()
    {
        PlayerPrefs.SetInt("lastScene", 1);
        SceneManager.LoadScene(1);
    }

    public void CampaignMap()
    {
        PlayerPrefs.SetInt("lastScene", 2);
        SceneManager.LoadScene(2);
    }

    public void PreBattle()
    {
        // Have to set this up in campaign map
        // you PlayerPref to store scene index
        SceneManager.LoadScene(3); // default 3
    }

    public void Battle()
    {
        Debug.Log("Battle() - Change Scene");
        // Have to set this up in campaign map
        // you PlayerPref to store scene index
        SceneManager.LoadScene(4); // default 4
    }

    public void Restart()
    {
        Debug.Log("Restart() - Change Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
