using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Swordmen", menuName = "Swordmen")]
public class Swordmen : ScriptableObject
{
    public int damage;
    public float speed;
    public float cd; //seconds
    public float range;
    public float parry;
    public int hp;
}
