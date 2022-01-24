using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScreen : MonoBehaviour
{
    Vector2 startPosition;
    Vector2 newPosition;
    public Transform panel; // in Editor
    public float movementTime = 1f;

    // Move UI panel horizontally by dragging mouse (inversed)

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            newPosition = new Vector2(startPosition.x - Input.mousePosition.x, 0);
        }

        panel.transform.localPosition = Vector2.Lerp(panel.transform.localPosition, newPosition, Time.deltaTime * movementTime);
    }
}
