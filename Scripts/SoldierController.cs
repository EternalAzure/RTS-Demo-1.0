using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierController : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] private Vector3 destination;
    [SerializeField] private Transform focus;
    [SerializeField] private float stoppingDistance;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        destination = transform.position;
        focus = null;
        stoppingDistance = GetComponent<Soldier>().stats.range;
        agent.stoppingDistance = stoppingDistance;
    }
    public void Move(Vector3 dest)
    {
        if (agent == null) return;
        if (dest == null) return;
        destination = dest;
        agent.SetDestination(dest);
    }
    public void Look()
    {
        if (focus == null) return;
        Vector3 direction = (focus.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); 
    }
    void Update()
    {
        Move(destination);
        Look();
    }
}
