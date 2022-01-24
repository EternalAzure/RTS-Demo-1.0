using UnityEngine;
using System.Collections.Generic;
using System;

/*
    Responsibilities:
    -Keep track of fighters
    -Declare game over

    GetEnemy() is called by both player and CPU controlled units.
    MultiSpawner and WaveSpawner inherit this class. As spawners they
    have access to all fighters on their respective faction.
    Closest() only iterates over copy of fighters list to avoid concurrent
    modification error.
    We always use copy of list. Original is modified every time fighter dies.
*/
public class Adversary: MonoBehaviour
{
    [SerializeField] protected List<Transform> fighters = new List<Transform>();
    
    public virtual Transform GetEnemy(Vector3 location, float range)
    {
        // Important to make copy and not use original
        List<Transform> copy = new List<Transform>(fighters);
        return Closest(location, copy, range);
    }

    Transform Closest(Vector3 location, List<Transform> list, float meleeRange)
    {
        if (list == null || list.Count == 0) return null;
       
        Transform closest = list[0];
        foreach (Transform t in list)
        {
            float a = Vector3.Distance(location, t.position);
            float b = Vector3.Distance(location, closest.position);
            if( a < b) closest = t;
        }
        if(Vector3.Distance(location, closest.position) > meleeRange) return null;
        return closest;
        
    }

    protected void RemoveSelf(Transform fighter)
    {
        fighters.Remove(fighter);
        Destroy(fighter.gameObject);
    }

    public void SetFighters(List<Transform> fighters)
    {
        this.fighters = fighters;
    }

    public List<Transform> GetFighters()
    {
        return new List<Transform>(fighters);
    }
}