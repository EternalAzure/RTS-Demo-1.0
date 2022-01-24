using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool active = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (active) 
            {
                transform.GetChild(0).gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else 
            {
                transform.GetChild(0).gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            active = !active;
        }
    }

    public void Resume()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1;
        active = !active;
    }
}
