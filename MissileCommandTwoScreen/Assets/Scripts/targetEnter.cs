using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetEnter : MonoBehaviour
{
    public GameObject repairKeyText;
    public GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        repairKeyText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (repairKeyText.activeSelf == true && Input.GetKeyDown(KeyCode.E)) {
            targetController.reactivateTarget(target);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && target.GetComponent<MeshRenderer>().material.color == targetController.deadMaterial.color) {
            repairKeyText.SetActive(true);
        }
        else {
            repairKeyText.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            repairKeyText.SetActive(false);
        }
    }
}
