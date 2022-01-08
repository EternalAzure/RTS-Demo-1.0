using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    GameMaster holds info about all units in game.
    Class is used for finding enemies.
*/
public class GameMaster : MonoBehaviour
{
    [SerializeField] private IController[] playerUnits;
    [SerializeField] private IController[] aiUnits;

    public int playerUnitCount;
    public int aiUnitCount;

    void Start()
    {
        playerUnitCount = playerUnits.Length;
        aiUnitCount = aiUnits.Length;
    }

    public void SetPlayerUnits(IController[] array)
    {
        playerUnits = array;
    }
    public void SetAIUnits(IController[] array)
    {
        aiUnits = array;
    }

    public Transform GetEnemy(Vector3 location, float meleeRange, bool isAI)
    {
        // Melee range is chaseDistance
        // At that range StateMachine changes state from passive
        Transform[] array;
        if (isAI) array = ClosestUnitOnAverage(location, playerUnits);
        else array = ClosestUnitOnAverage(location, aiUnits);
        return Closest(location, array, meleeRange);
    }
    Transform[] ClosestUnitOnAverage(Vector3 location, IController[] array)
    {
        IController closest = array[0];
        foreach (IController c in array)
        {
            float a = Vector3.Distance(location, c.GetCenter());
            float b = Vector3.Distance(location, closest.GetCenter());
            if( a < b) closest = c;
        }
        
        return closest.GetList();
    }

    Transform Closest(Vector3 location, Transform[] array, float meleeRange)
    {
        if (array == null) return null;
        try
        {
            Transform closest = array[0];
            foreach (Transform t in array)
            {
                float a = Vector3.Distance(location, t.position);
                float b = Vector3.Distance(location, closest.position);
                if( a < b) closest = t;
            }
            if(Vector3.Distance(location, closest.position) > meleeRange) return null;
            return closest;
        }
        catch (IndexOutOfRangeException)
        {
            throw;
        }
        
    }
}
