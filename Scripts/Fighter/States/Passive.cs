using UnityEngine;
using UnityEngine.AI;

/*
    State Machine State
    Passive state follows movement orders of IController.
    Meanwhile it scans for enemies.
*/
public class Passive : MachineState
{
    [SerializeField] protected Adversary adversary; // in spawner
    [SerializeField] protected StateMachine ai; // From this gameobject
    [SerializeField] protected FighterConfig config;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Chase chase;
    [SerializeField] protected Vector3 target;

    void Awake()
    {
        ai = GetComponent<StateMachine>();
        chase = GetComponent<Chase>();

        // Configure NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        target = transform.position;
        agent.destination = target;
        agent.speed = config.speed;
        agent.stoppingDistance = 0;
        agent.angularSpeed = config.angularSpeed;
        
    }
    public override void Init()
    {   
        agent.stoppingDistance = 0;

        Look();

        Travel();
        
        FindEnemies();
    }

    protected void Look()
    {
        if (target == null) return;
        Vector3 direction = (target - transform.position).normalized;
        if (direction.x == 0 && direction.z == 0) return; // Unity does not like zero rotations
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); 
    }

    protected virtual void Travel()
    {

    }

    protected virtual void FindEnemies()
    {
        
    }

    public void SetAdversaries(Adversary adversary)
    {
        this.adversary = adversary;
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
}
