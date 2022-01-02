using System.Collections;
using UnityEngine;

public class DeployFirstFrameCommands: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FirstFrameCommands sc = gameObject.AddComponent<FirstFrameCommands>() as FirstFrameCommands;
    }
}
