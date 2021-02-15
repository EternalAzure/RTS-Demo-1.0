using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask unitMask;

    private UnitController selected = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200, unitMask))
            {
                selected = hit.transform.gameObject.GetComponent<Soldier>().GetUC();
                selected.SetEnabled(true);
            }
            else if (selected != null)
            {
                selected.SetEnabled(false);
            }
        }
    }
}
