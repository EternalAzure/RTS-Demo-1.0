using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
    State Machine State
    Fight handles combat. 
    Hits enemy only once per animation.
    Signals animator to run attack anim.
*/
public class Fight : MachineState
{
    public event System.Action OnAttack;
    bool strike = true;
    [SerializeField] private StateMachine ai; // From this gameobject
    [SerializeField] private MovementModule movement; // From this gameobject
    [SerializeField] Animator animator; // From this gameobject

    void Start()
    {
        ai = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
        movement = GetComponent<MovementModule>();
    }
    public override void Init()
    {
        StartCoroutine(WaitAnimation());
    }

    IEnumerator WaitAnimation()
    {
        // Decide between attack and parry
        // We decided on attack as we have no parry animation yet

        // This is for CharacterAnimator
        OnAttack?.Invoke();
        
        // Wait for animation to run and then change state
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            //Hit only once
            if (strike)
            {
                // Inflict damage
                Damage();
                strike = false;
            }
            
            yield return null;
        }

        // Give new permission to hit
        strike = true;

        // Reset target and switch AI state
        movement.target = null;
        ai.state =StateMachine.State.Passive;
    }

    void Damage()
    {
        try
        {
            if (movement.target == null) return;
            movement.target.GetComponent<MovementModule>().TakeDamage(movement.stats.damage);
        }
        catch (MissingReferenceException)
        {
            // ignore
            throw;
        }
        catch (NullReferenceException)
        {
            throw;
        }
    }
}
