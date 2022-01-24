using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conquest : MonoBehaviour
{
    // Friendly RGBA color
    // 100, 180, 250, 255

    // Hostile RGBA color
    // 250, 140, 100, 255

    public float r = 0.4f;
    public float g = 0.7f;
    public float b = 0.98f;
    public float conquestSpeed = 0.006f; // 0.01f worked well
    public Transform objectivesTracker;

    void Start()
    {
        objectivesTracker = transform.parent;
        this.gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b, 1);
    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerStay()
    {
        if (r >= 0.98)
        {
            // This objective has fallen to enemies
            objectivesTracker.GetComponent<Environment>().IncrementFallenObjectives();
            // There is bug that allowes to increment fallenObjectives twice 23.1.2022
            Destroy(this);
        }

        r += 0.232f * Time.deltaTime;
        g -= 0.232f / (58 / 15) * Time.deltaTime;
        b -= 0.232f * Time.deltaTime;

        this.gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b, 1);
    }
}
