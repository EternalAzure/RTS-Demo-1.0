using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Main task of controller is to calculate relative positions for fighters.
    This happens in Move()
*/
public class PlayerController : MonoBehaviour, IController
{
    [SerializeField] private LayerMask movementMask;
    [SerializeField] private Camera _camera;
    public Vector3 destination;
    public Transform[] fighters;
    bool isIterating = false;
    public bool selected = false;

    public void SetList(Transform[] t)
    {
        fighters = t;
    }
    public Transform[] GetList()
    {
        if (fighters.Length == 0) return null;
        return fighters;
    }

    void Start()
    {
        // This makes fighters stay still were they spawned
        destination = GetCenter();
        Move();

        movementMask = LayerMask.GetMask("Ground");
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(!selected) return; 
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, movementMask))
            {
                destination = hit.point;
            }
            Move(); // new
        }
        //Move(); Why should this be called all the time? destination is saved in movement module any way. right?
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

    /* THIS COULD BE VERY DESIRABLE FORMATION
        int rows = Mathf.FloorToInt(Mathf.Sqrt(fighters.Length));
        int z = 0;
        for (int x = 0; x < fighters.Length; x++)
        {
            if (z >= rows) z = 0;
            Vector3 newPos = new Vector3(destination.x + x, destination.y, destination.z + z);
            fighters[x].GetComponent<MovementModule>().SetDestination(newPos);
            z++;
        }
    */

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
}
