using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileCommand : MonoBehaviour
{
    public GameObject[] targets;
    public Camera canonCamera;
    public GameObject prefab;
    public static List<GameObject> missiles = new List<GameObject>();

    public int farLeftSpawn = 0;
    public int farRightSpawn = 0;

    public int height = 0;

    public float missileSpeed = 15.0F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0.0F, 1.0F) < 0.0005) {
            int xPos = Random.Range(farLeftSpawn, farRightSpawn);

            GameObject missile = Instantiate(prefab) as GameObject;
            Vector3 vectorPos = new Vector3((float)xPos, (float)height, canonCamera.transform.position.z);
            Debug.Log(vectorPos);
            missile.transform.position = vectorPos;


            missile.transform.LookAt(targets[Random.Range(0, 3)].transform);

            Rigidbody rb = missile.GetComponentInChildren<Rigidbody>();
            rb.AddForce(missile.transform.forward * missileSpeed);

            missiles.Add(missile);
        }
    }
}
