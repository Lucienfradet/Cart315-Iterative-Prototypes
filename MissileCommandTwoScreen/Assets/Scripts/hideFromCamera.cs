using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideFromCamera : MonoBehaviour {
    
    [SerializeField] GameObject[] objectsToHide;

    void OnPreCull() {
        foreach (GameObject obj in objectsToHide) {
            //disable the object before this camera renders
            obj.SetActive(false);
        }
    }

    private void OnPostRender() {
        foreach (GameObject obj in objectsToHide) {
            //Re-enable the object for other cameras to render
            obj.SetActive(true);
        }
    }
}
