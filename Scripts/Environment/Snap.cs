using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public Vector3 gridSize = new Vector3(1, 1, 1);
  

    void OnDrawGizmos()
    {
        SnaptoGrid();
    }

    void SnaptoGrid()
    {
        Vector3 newPosition = new Vector3(
            Mathf.RoundToInt(this.transform.position.x / gridSize.x) * gridSize.x,
            Mathf.RoundToInt(this.transform.position.y / gridSize.y) * gridSize.y,
            Mathf.RoundToInt(this.transform.position.z / gridSize.z) * gridSize.z
        );
        this.transform.position = newPosition;
    }
}
