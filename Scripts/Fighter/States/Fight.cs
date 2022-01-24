using UnityEngine;
using UnityEngine.AI;

/*
    State Machine State
    Every action will be IEnumerator that waits for action animation to finish.
*/
public class Fight : MachineState
{
    [SerializeField] protected StateMachine ai; // From this gameobject
    [SerializeField] protected Animator animator; // From this gameobject
    [SerializeField] protected NavMeshAgent agent; // From this gameobject
    [SerializeField] protected CharacterAnimator characterAnimator;
    [SerializeField] protected FighterConfig config; // in Unity Editor
    [SerializeField] protected Transform target;
    protected string _animation;
    protected bool notBusy = true;

    void Start()
    {
        ai = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        characterAnimator = GetComponent<CharacterAnimator>();
    }
    public override void Init()
    {
        // Right stopping distance matches attack animation
        agent.stoppingDistance = config.fightDistance;
        if (notBusy) ChooseAction();
    }

    protected virtual void ChooseAction()
    {
        
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
