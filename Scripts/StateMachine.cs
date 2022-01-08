using UnityEngine;
public class StateMachine : MonoBehaviour
{
    public enum State 
    {
        Passive,
        Chase,
        Fight
    }
    public State state;
    public MachineState passive; // From this gameobject
    public MachineState chase; // From this gameobject
    public MachineState fight; // From this gameobject

    // Start is called before the first frame update
    void Start()
    {
        state = State.Passive;
        passive = GetComponent<Passive>();
        if(passive == null) passive = GetComponent<PassiveOld>();
        chase = GetComponent<Chase>();
        fight = GetComponent<Fight>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Passive:
            // Set parent units objective as destination for Nav Agent
            // Follow around
                passive.Init();
                break;
            case State.Chase:
            // Set target as destination for Nav Agent
            // Chase after
                chase.Init();
                break;
            case State.Fight:
            // Inflict damage and play an attack animation
            // Hit on ;)
                fight.Init();
                break;
        }
    }
}
