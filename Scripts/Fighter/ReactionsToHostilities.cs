using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.Events;

/*
    Responsibilities:
    -Turn received stun, damage, knock back etc into real action.
    -Report about death


*/
public class ReactionsToHostilities : MonoBehaviour
{
    public event Action<Transform> onDeathEvent;
    public FighterConfig stats; // in Unity editor
    public float healthPoints;
    
    void Start()
    {
        healthPoints = stats.hitPoints;
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        if (healthPoints > 0) return;
        
        // TODO death animation
        onDeathEvent?.Invoke(this.transform);
    }

    public void TakeStun()
    {
        // TODO
        Debug.Log("Stunned");
    }

    public void TakeKnockBack()
    {
        // TODO
        Debug.Log("Knocked back");
    }

    
}
