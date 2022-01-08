using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    // Fighters will add themselves via Unit script
    public List<GameObject> unitList = new List<GameObject>(); 
    public List<GameObject> unitsSelected = new List<GameObject>();
    private static UnitSelections _instance;
    public static UnitSelections Instance {get {return _instance;}}

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitsSelected.Add(unitToAdd);
        AllowMovement(unitToAdd, true);
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            AllowMovement(unitToAdd, true);
        }
        else
        {
            unitsSelected.Remove(unitToAdd);
            AllowMovement(unitToAdd, false);
        }
    }

    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            AllowMovement(unitToAdd, true);
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
            AllowMovement(unit, false);
        }
        unitsSelected.Clear();
    }

    public void Deselect(GameObject unitToDeselect)
    {
        unitsSelected.Remove(unitToDeselect);
        AllowMovement(unitToDeselect, false);
    }

    private void AllowMovement(GameObject unit, bool isAllowed)
    {
        try
        {
            PlayerController p = (PlayerController) unit.GetComponent<MovementModule>().controller;
            p.selected = isAllowed;
        }
        catch (MissingReferenceException)
        {
            // Unit has removed itself from unitList in Unit.OnDestroy
            // No need for further actions
            throw;
        }
    }
}
