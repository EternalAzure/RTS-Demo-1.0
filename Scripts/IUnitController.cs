using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitController
{
    void Move();
    void SetSoldiers(List<Transform> list);
    void OnDeath(Soldier soldier);
    
}
