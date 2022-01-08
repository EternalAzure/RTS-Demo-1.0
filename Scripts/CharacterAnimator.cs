using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;
    const float locomotionAnimationSmoothTime = 0.1f;

    NavMeshAgent agent;
    protected Animator animator;
    protected AnimatorOverrideController overrideController;
    protected Fight fight;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        fight = GetComponent<Fight>();

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;

        fight.OnAttack += OnAttack;
    }

    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIDX = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIDX];
        //Override enables multiple attack animations
    }

    public virtual  void OnDeath()
    {
        //Plays death animation
        //animator.SetTrigger("death");
    }
}
