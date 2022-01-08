using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/*
    Controllers handle moving in formations. 
    They also manage list of fighters alive.
    Controllers get movement orders else where.
    Player or AI.
*/
public class AIController : MonoBehaviour, IController
{
    public Transform[] fighters; // Set in MultiSpawner
    public Vector3 destination;
    bool isIterating = false;

    public void SetList(Transform[] t)
    {
        fighters = t;
    }

    public Transform[] GetList()
    {
        if (fighters.Length == 0) return null;
        return fighters;
    }

    // Start is called before the first frame update
    void Start()
    {
        // This makes fighters stay still were they spawned
        destination = GetCenter();
        Move();

        foreach (Transform item in fighters)
        {
            // Not to mix with player selectable objects on layer Selectable
            item.gameObject.layer = 0;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        try
        {
            Move();
        }
        catch (MissingReferenceException)
        {
            //ignore
            throw;
        }
    }

    public Vector3 GetCenter()
    {
        if (fighters.Length == 0)  return Vector3.positiveInfinity;

        float x = 0;
        float y = 0;
        float z = 0;
        int count = fighters.Length;
        foreach (Transform item in fighters)
        {
            x += item.position.x;
            y += item.position.y;
            z += item.position.z;
        }
        return new Vector3(x/count, y/count, z/count);
    }

    public void Move()
    {
        if (fighters.Length <= 0) return;
        isIterating = true;
        
        int rows = Mathf.FloorToInt(Mathf.Sqrt(fighters.Length)); // x
        int columns = Mathf.CeilToInt(fighters.Length / rows); // z

        float z = 0;
        float x = 0;
        for (int i = 0; i < fighters.Length; i++)
        {
            if (x >= rows)
            {
                 x = 0; 
                 z += 1.2f;
            }
            Vector3 newPos = new Vector3(destination.x + x, destination.y, destination.z + z);
            fighters[i].GetComponent<MovementModule>().SetDestination(newPos);
            x += 1.2f;
        }
        isIterating = false;
    }
    

    public bool OnDeath(Transform dead)
    {
        StartCoroutine(SafeRemove(dead));
        return true;
    }

    IEnumerator SafeRemove(Transform dead)
    {
        while (isIterating)
        {
            yield return null;
        }
        if (fighters.Length > 0)
        {
            fighters = fighters.Where(val => val != dead).ToArray();
        }
        yield return null;
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }

    void OnDestroy()
    {
        isIterating = false;
    }
}

