using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    Movement and combat
    
    Nav mesh agent is Unity provided AI that moves gameobjects on Nav mesh.
    This class supports movement. Because health points are on same stat card,
    this class signals when fighter is dead.

    Important:
    LateUpdate() runs only after Update().
    1. Use lists to calculate combat using Update()
    2. Update/edit those lists using LateUpdate()
    3. Profit. No concurrent modification error, no missing reference nor null references.
*/
public class MovementModule : MonoBehaviour
{
    public Transform target = null; // by StateMachine
    public FighterConfig stats; // in Unity editor
    bool condition3 = false; // IController via OnDeath() return value
    private Vector3 destination; // by IController via SetDestination()
    public IController controller; // MultiSpawner
    public float healthPoints;
    public NavMeshAgent navMeshAgent; //From this gameobject
    public Vector3 navMeshAgentDestination; // For debugging only;

    private float damageTakenInThisFrame = 0;
    
    void Start()
    {
        healthPoints = stats.hp;
        if(controller == null) controller = GetComponent<IController>();

        // Nav Mesh configuration by stats
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = stats.speed;
        navMeshAgent.stoppingDistance = 0;
        navMeshAgent.angularSpeed = stats.angularSpeed;
        destination = transform.position;
        navMeshAgent.destination = destination;
    }

    // MOVEMENT
    // ======================================================= //
    void Update()
    {
        Look();
        navMeshAgentDestination = navMeshAgent.destination;
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }

    public void Move(Vector3 destination)
    {
        navMeshAgent.destination = destination;
    }

    public void Move()
    {
        // Calling this method implicates passive state
        navMeshAgent.stoppingDistance = 0;
        navMeshAgent.destination = destination;
    }

    private void Look()
    {
        if (target == null) return;
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); 
    }

    // COMBAT & DEATH
    // ======================================================= //
    void LateUpdate()
    {
        ReduceHealth();
    }
    public void TakeDamage(float damage)
    {
        damageTakenInThisFrame += damage;
    }

    private void ReduceHealth()
    {
        healthPoints -= damageTakenInThisFrame;
        if (healthPoints > 0) return;
        
        // SafeDestroy blocks execution till safe
        SafeDestroy();
        Destroy(this.gameObject);
    }


    void SafeDestroy()
    {
        // We expect these to always return true
        UnitSelections.Instance.unitList.Remove(this.gameObject); // condition1
        UnitSelections.Instance.unitsSelected.Remove(this.gameObject); // condition2

        // OnDeath will only return true. And only when controller is ready
        condition3 = controller.OnDeath(this.transform); // condition3

        // Could use coroutine to save computing time
        while(true)
        {
            if (condition3)
            {
                return;
            }
            Debug.LogWarning(gameObject.name + " was not destroyed as expected. \nMethod continued execution without waiting controller.OnDeath() to return true.");
        }
    }
}
