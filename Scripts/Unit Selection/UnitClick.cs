using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera myCamera;
    public LayerMask selectable;
    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
        selectable = LayerMask.GetMask("Selectable");
        ground = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectable))
            {
                Transform clickedFighter = hit.collider.transform;

                // if we hit selectable object
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    // shift click
                    UnitSelections.Instance.ShiftClickSelect(clickedFighter);
                }
                else
                {
                    // normal click
                    UnitSelections.Instance.ClickSelect(clickedFighter);
                }
            }
            else
            {
                // if we didn't && we are not shift clicking
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }
    }
}
