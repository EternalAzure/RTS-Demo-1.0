using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formations
{
    public Vector3[] Square(Vector3 origin, int unitSize)
    {
        Vector3[] positions = new Vector3[unitSize];
        Vector3 position = new Vector3(origin.x - 1.5f, origin.y, origin.z - 1.5f); // Add offset of 1.5f

        int rows = Mathf.FloorToInt(Mathf.Sqrt(unitSize));

        float z = 0;
        float x = 0;
        for (int i = 0; i < unitSize; i++)
        {
            if (x >= rows)
            {
                 x = 0; 
                 z += 1.2f;
            }
            positions[i] = new Vector3(position.x + x, position.y, position.z + z);
            x += 1.2f;
        }

        return positions;
    }
}
