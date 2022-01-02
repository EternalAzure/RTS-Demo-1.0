using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFrameCommands : MonoBehaviour
{
    [SerializeField] private AIController controller = null;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        controller = GameObject.Find("Red Unit").GetComponent<AIController>();
    }

    // LateUpdate is called as the last thing per frame
    void LateUpdate()
    {
        controller.SetDestination( Vector3.zero);
        controller.Move();
        Destroy(this);
    }
}