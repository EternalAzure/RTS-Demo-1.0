using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
   public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Menu()
    {
        SceneManager.LoadScene(1);
    }

}
