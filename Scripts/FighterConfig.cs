using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FighterConfig", menuName = "FighterConfig")]
public class FighterConfig : ScriptableObject
{
    // Damage that is inflicted on others per attack
    public float damage;
    // Movement speed on battle field
    public float speed;
    // Distance between it and its parent unit
    public float chaseDistance;
    // Distance where to stop. Main reason being animations
    public float stoppingDistance;
    // How fast fighter turns. slow:120 medium:240 fast:480
    public float angularSpeed;
    // Distance within it will fight
    public float engagementDistance;
    // Distance within it can strike
    public float strikeDistance;
    // 
    public float parry;
    // Health points
    public float hp;
}
