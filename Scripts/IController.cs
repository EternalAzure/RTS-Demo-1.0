using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    void Move();
    Vector3 GetCenter();
    bool OnDeath(Transform t);
    void SetList(Transform[] t);
    Transform[] GetList();
}