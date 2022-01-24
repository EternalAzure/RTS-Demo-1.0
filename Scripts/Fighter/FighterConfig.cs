using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FighterConfig", menuName = "FighterConfig")]
public class FighterConfig : ScriptableObject
{
    public float damage; // Damage that is inflicted on others per attack
    public float speed; // Movement speed on battle field
    public float chaseDistance; // Distance in which chase state is triggered
    public float angularSpeed; // How fast fighter turns. slow:120 medium:240 fast:480
    public float fightDistance; // Distance in which fight state is triggered
    public float parry; // Chance to block
    public float hitPoints; // vitality
}
