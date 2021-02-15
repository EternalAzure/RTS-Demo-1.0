using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour, IUnitController
{
    private new bool enabled = false;
    public bool isIterating = false;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask movementMask;

    private List<Transform> soldiers;
    private List<Transform> corpses;
    [SerializeField] private Vector3 destination;

    private void Start()
    {
        corpses = new List<Transform>();
        destination = transform.position;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        movementMask = LayerMask.GetMask("Ground");

        StartCoroutine(RemoveDeadSoldiers());
    }

    private void Update()
    {
       if (!enabled) return;

       if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                destination = hit.point;
                Move();
            }
        }
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
                }catch(Exception){}
                
                index++;
            }
        }
        isIterating = false;
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
            if (soldiers.Count > 0 && corpses.Count > 0)
            {
                soldiers.Remove(corpses[0]);
                corpses.RemoveAt(0);
            }
            yield return null;
        }
    }

    public void SetEnabled(bool b)
    {
        enabled = b;
    }
    public void SetSoldiers(List<Transform> soldiers)
    {
        this.soldiers = soldiers;
    }
}
