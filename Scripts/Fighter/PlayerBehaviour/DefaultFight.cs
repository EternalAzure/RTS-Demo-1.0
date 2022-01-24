using System.Collections;
using UnityEngine;
using System;

/*
    State Machine State
    Travels and seeks adversaries.
*/
public class DefaultFight : Fight
{
    protected override void ChooseAction()
    {
        notBusy = false;
        StartCoroutine(BaseAttack());
    }

    IEnumerator BaseAttack()
    {
        //Debug.Log("BaseAttack()");
        characterAnimator.OnAttack();
        _animation = "Attack";
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(_animation));
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(_animation)) yield return null;

        Damage();
        
        ai.state =StateMachine.State.Passive;
        notBusy = true;
        yield break;
    }

    void Damage()
    {
        //Debug.Log("Damage() - player");
        try
        {
            if (target == null) return;
            target.GetComponent<ReactionsToHostilities>().TakeDamage(config.damage);
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