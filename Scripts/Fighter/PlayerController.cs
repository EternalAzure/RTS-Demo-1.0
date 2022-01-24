using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

/*
    Main task of controller is to calculate relative positions for fighters.
    This happens in Move()
*/
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    public Vector3 destination;
    public event Action<Transform> onDeathEvent;
    public Transform[] fighters;
    public bool selected = false;
    public float offset = 0f;
    Formations formations = new Formations();

    public void SetList(Transform[] t)
    {
        fighters = t;
    }
    public Transform[] GetList()
    {
        if (fighters.Length == 0) return null;
        return fighters;
    }

    void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Start()
    {
        foreach (Transform fighter in fighters)
        {
            fighter.GetComponent<ReactionsToHostilities>().onDeathEvent += OnDeath;
        }
    }

    private void Update()
    {
        if(!selected) return; 
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float newX = 5 * (Mathf.RoundToInt(hit.point.x / 5)) - offset;
                float newY = hit.point.y;
                float newZ = 5 * (Mathf.RoundToInt(hit.point.z / 5)) - offset;

                destination = new Vector3(newX, newY, newZ);
            }
            
            Move(fighters.Clone() as Transform[], destination);
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


    public void Move(Transform[] fighters, Vector3 destination)
    {
        if (fighters.Length <= 0) return;

        Vector3[] positions = formations.Square(destination, fighters.Length);

        for (int i = 0; i < fighters.Length; i++)
        {
            try
            {
                fighters[i].GetComponent<Passive>().SetTarget(positions[i]);
            }
            catch (MissingReferenceException)
            {
                
                throw;
            }
        }
    }

    void OnDeath(Transform dead)
    {
        fighters = fighters.Where(val => val != dead).ToArray();
        onDeathEvent?.Invoke(dead);
    }

    public bool RecogniseFighter(Transform fighter)
    {
        foreach (var f in fighters)
        {
            if (f == fighter) return true;
        }
        return false;
    }
}
