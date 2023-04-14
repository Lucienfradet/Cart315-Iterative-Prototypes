using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetController : MonoBehaviour
{
    public Material aliveMaterialNonStatic;
    public Material deadMaterialNonStatic;

    public static Material aliveMaterial;
    public static Material deadMaterial;

    // Start is called before the first frame update
    void Start()
    {
        aliveMaterial = aliveMaterialNonStatic;
        deadMaterial = deadMaterialNonStatic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void killTarget(GameObject target) {
        target.GetComponent<MeshRenderer>().material = deadMaterial;
    }

    public static void reactivateTarget(GameObject target) {
        target.GetComponent<MeshRenderer>().material = aliveMaterial;
    }
}
