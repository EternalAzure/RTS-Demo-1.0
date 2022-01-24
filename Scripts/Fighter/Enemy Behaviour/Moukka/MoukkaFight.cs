using System.Collections;
using UnityEngine;
using Random=UnityEngine.Random;

public class MoukkaFight : Fight
{
    protected override void ChooseAction()
    {
        notBusy = false;

        int f = Random.Range(0, 10);
        if (f > 3)
        {
            StartCoroutine(Attack());
        }
        else
        {
            StartCoroutine(Hesitate());
        }

    }

    IEnumerator Attack()
    {
        characterAnimator.OnAttack();
        _animation = "Attack";
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(_animation));
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(_animation)) yield return null;

        Damage();
        
        ai.state =StateMachine.State.Passive;
        notBusy = true;
        yield break;
    }

    IEnumerator Hesitate()
    {
        // Play some animation or something
        yield return new WaitForSeconds(2.5f);
        notBusy = true;
        yield break;
    }

    void Damage()
    {
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
    }
}
