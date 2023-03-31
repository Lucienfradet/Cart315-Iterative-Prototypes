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

        //Debug Key
        if (Input.GetKeyDown(KeyCode.K)) {
            missileCommand.gameState = "secondMissile";
            GameObject.Destroy(missile);

        }
        if (Input.GetKeyDown(KeyCode.L)) {
            missileCommand.gameState = "thirdMissile";
            GameObject.Destroy(missile);

        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag != "arrow") {
            missileCommand.changeStateTutorial();
            GameObject.Destroy(missile);
        }
    }
}
