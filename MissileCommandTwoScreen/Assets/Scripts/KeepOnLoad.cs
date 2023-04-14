using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnLoad : MonoBehaviour
{
    public static bool tutorialState;
    private bool getDataOnceSwitch = true;
    // Start is called before the first frame update
    void Start()
    {
        if (getDataOnceSwitch) {
            tutorialState = missileCommand.tutorialSwitch;
        }
    }

    void Awake() {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
