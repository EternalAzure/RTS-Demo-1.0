using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    // Fighters will add themselves via Unit script
    //public List<GameObject> unitList = new List<GameObject>(); 
    public List<PlayerController> unitsSelected = new List<PlayerController>();
    private static UnitSelections _instance;
    public static UnitSelections Instance {get {return _instance;}}
    public PlayerController[] controllers;

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

    void Start()
    {
        GameObject multiSpawnerObject = GameObject.FindGameObjectWithTag("MultiSpawner");
        controllers = multiSpawnerObject.GetComponents<PlayerController>();
    }

    public void ClickSelect(Transform fighter)
    {
        DeselectAll();
        PlayerController controller = FindController(fighter, controllers);
        if (controller == null) return;

        unitsSelected.Add(controller);
        AllowMovement(controller, true);
    }

    public void ShiftClickSelect(Transform fighter)
    {
        PlayerController controller = FindController(fighter, controllers);
        if (!unitsSelected.Contains(controller))
        {
            unitsSelected.Add(controller);
            AllowMovement(controller, true);
        }
        else
        {
            unitsSelected.Remove(controller);
            AllowMovement(controller, false);
        }
    }

    public void DragSelect(Transform fighter)
    {
        PlayerController controller = FindController(fighter, controllers);
        if (controller == null) return;

        if (!unitsSelected.Contains(controller))
        {
            unitsSelected.Add(controller);
            AllowMovement(controller, true);
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

    public void Deselect(Transform fighter)
    {
        PlayerController controller = FindController(fighter, controllers);
        unitsSelected.Remove(controller);
        AllowMovement(controller, false);
    }

    private void AllowMovement(PlayerController unit, bool isAllowed)
    {
        unit.selected = isAllowed;
    }

    PlayerController FindController(Transform selectedFighter, PlayerController[] controllers)
    {
        foreach (var controller in controllers)
        {
            if (controller.RecogniseFighter(selectedFighter)) return controller;
        }
        return null;
    }
}
