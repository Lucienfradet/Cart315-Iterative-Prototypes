using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject missile;
    private Rigidbody rb;
    void Start()
    {
        rb = missile.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        GameObject.Destroy(missile);
    }
}
