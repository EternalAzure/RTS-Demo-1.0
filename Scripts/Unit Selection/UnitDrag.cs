using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    Camera myCamera;
    public LayerMask selectable;
    [SerializeField] RectTransform boxVisual; // Graphical
    Rect selectionBox; // Logical
    Vector2 P1;
    Vector2 P2;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
        P1 = Vector2.zero;
        P2 = Vector2.zero;
        DrawVisual();
    }

    // Update is called once per frame
    void Update()
    {
        // When clicked
        if (Input.GetMouseButtonDown(0))
        {
            P1 = Input.mousePosition;
            selectionBox = new Rect();
        }
        // When dragged
        if (Input.GetMouseButton(0))
        {
            P2 = Input.mousePosition;
            DrawVisual();
            DrawSelection();
            SelectUnits(); // This is not misplaced
        }

        // When realeased
        if (Input.GetMouseButtonUp(0))
        {
            P1 = Vector2.zero;
            P2 = Vector2.zero;
            DrawVisual();
        }
    }

    void DrawVisual()
    {
        Vector2 boxStart = P1;
        Vector2 boxEnd = P2;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        boxVisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        // x calculation
        if (Input.mousePosition.x < P1.x)
        {
            // Dragging left
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = P1.x;
        }
        else
        {
            // Dragging right
            selectionBox.xMin = P1.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        // y calculation
        if (Input.mousePosition.y < P1.y)
        {
            // Dragging down
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = P1.y;
        }
        else
        {
            // Dragging up
            //SelectUnits(); what teh fy
            selectionBox.yMin = P1.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    void SelectUnits()
    {
        foreach (var unit in UnitSelections.Instance.unitList)
        {
            // if unit is within bounds of selection rect
            if (selectionBox.Contains(myCamera.WorldToScreenPoint(unit.transform.position)))
            {
                // if unit is on right LayerMask
                if (selectable.Contains(unit.layer)) //unit.layer == 6
                {
                    UnitSelections.Instance.DragSelect(unit);
                }
            }
        }
    }
}
