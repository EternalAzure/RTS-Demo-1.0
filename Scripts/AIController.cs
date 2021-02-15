using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour, IUnitController
{
    List<Transform> soldiers;
    List<Transform> corpses;
    public Vector3 destination;
    public bool isIterating = false;
    void Start()
    {
        corpses = new List<Transform>();
        destination = transform.position;
        StartCoroutine(RemoveDeadSoldiers());
    }

    public void Move()
    {
        if (soldiers.Count <= 0) return;
        isIterating = true;
        int rows = Mathf.CeilToInt(Mathf.Sqrt(soldiers.Count));
        int columns = Mathf.FloorToInt(Mathf.Sqrt(soldiers.Count));
        int index = 0;
        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < columns; z++)
            {
                Vector3 newPos = new Vector3(destination.x + x, destination.y, destination.z + z);
                try
                {
                    soldiers[index].GetComponent<SoldierController>().Move(newPos);
                }
                catch (Exception) { }

                index++;
            }
        }
        isIterating = false;
    }

    public void SetSoldiers(List<Transform> soldiers)
    {
        this.soldiers = soldiers;
    }
    public void OnDeath(Soldier s)
    {
        corpses.Add(s.transform);
    }
    IEnumerator RemoveDeadSoldiers()
    {
        while (true)
        {
            while (isIterating)
            {
                yield return null;
            }
            if (corpses.Count > 0 && soldiers.Count > 0)
            {
                soldiers.Remove(corpses[0]);
                corpses.RemoveAt(0);
            }
            yield return null;
        }
    }
}
