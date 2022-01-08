using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
    Only single instance allowed!!!
*/
/*
    This is archived file and server no purpose in final game
*/
public class AliveOrDead : MonoBehaviour
{
    public static AliveOrDead Instance {get; private set;}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private AliveOrDead()
    {

    }

    // Data
    [SerializeField] private Transform[] ai;
    [SerializeField] private Transform[] player;

    public void SetAI(Transform[] array)
    {
        this.ai = array;
    }

    public void SetPlayer(Transform[] array)
    {
        this.player = array;
    }

    public void OnDeath(Transform fighter)
    {
        Debug.Log("AliveOrDead - OnDeath()");
        
    }

    public Transform[] GetEnemies(IController controller)
    {
       if(controller == null) return null;
        try
        {
            // SimpleController is exclusively used by human player
            SimpleController s = (SimpleController) controller;
            return ai;
        }
        catch (System.Exception)
        {
            return player;
            throw;
        }
    }

}