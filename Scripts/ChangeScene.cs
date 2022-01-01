using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene(1);
    }

    public void BatteConfig()
    {
        SceneManager.LoadScene(2);
    }

    public void Restart()
    {
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
