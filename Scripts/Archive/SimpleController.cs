using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

/*
    This is archived file and server no purpose in final game
*/
public class SimpleController : MonoBehaviour, IController
{
    [SerializeField] private LayerMask movementMask;
    [SerializeField] private Camera _camera;
    public Vector3 destination = new Vector3(-3, 0.5f, -4);
    public Transform[] fighters;
    public List<Transform> corpses;
    bool isIterating = false;

    public void SetList(Transform[] t)
    {
        fighters = t;
    }
    public Transform[] GetList()
    {
        return fighters;
    }

    // Start is called before the first frame update
    void Start()
    {
        movementMask = LayerMask.GetMask("Ground");
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        StartCoroutine(RemoveDeadSoldiers());
    }

    // Update is called once per frame
    private void Update()
    {

       if (Input.GetMouseButtonDown(1))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                destination = hit.point;
            }
        }
        Move();
    }

    public Vector3 GetCenter()
    {
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
        // Should calculate position in formation
        foreach (Transform item in fighters)
        {
            item.GetComponent<MovementModule>().SetDestination(destination);
        }
    }

    public bool OnDeath(Transform f)
    {
        corpses.Add(f);
        return true; 
        // Return value is not used
        // It is to satisfy interface
    }
    IEnumerator RemoveDeadSoldiers()
    {
        while (true)
        {
            while (isIterating)
            {
                yield return null;
            }
            if (fighters.Length > 0 && corpses.Count > 0)
            {
                fighters = fighters.Where(val => val != corpses[0]).ToArray();
                corpses.RemoveAt(0);
            }
            yield return null;
        }
    }    
}
