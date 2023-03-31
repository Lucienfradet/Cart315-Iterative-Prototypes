using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonControls : MonoBehaviour
{
    public Camera cannonCam;

    [Header("Projectile")]
    public GameObject prefabBullet;
    public GameObject prefabExplosion;
    public GameObject spawner;
    private GameObject projectile;
    public bool projectileTrigger = false;
    public int projectileSpeed = 10;
    public float offset = -125.0F;

    [Header("Movements")]
    public float speed = 15.0F;

    [Header("Shake")]
    public GameObject shaker;
    public float shakeSpeed = 1.0F;
    public float shakeAmount = 1.0F;
    private Vector3 rotationVector;
    private Vector3 previouseVector;
    private bool shakeFlag = false;

    [Header("Arrow")]
    private FunctionTimer arrowTimer = null;
    public float timeBeforeArrowReapears = 5F;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    private void FixedUpdate() {
        if (shakeFlag) {
            if (rotationVector != previouseVector) {
                ShakeCanon();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canonEnter.inside) {
            rotationVector = new Vector3(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0.0F);
            transform.Rotate(rotationVector * speed * Time.deltaTime);

            shakeFlag = true;
        }

        //shooting
        if (Input.GetKeyDown(KeyCode.Space) && !projectileTrigger && canonEnter.inside) {
            Debug.Log("SpaceBar");
            canonEnter.SetArrowState(false);

            if (missileCommand.gameState != "firstMissile" && missileCommand.gameState != "secondMissile" && missileCommand.gameState != "thirdMissile") {
                arrowTimer = new FunctionTimer(reactivateArrow, timeBeforeArrowReapears);
            }
            projectileTrigger = true;

            projectile = Instantiate(prefabBullet) as GameObject;
            projectile.transform.position = spawner.transform.position;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = Quaternion.AngleAxis(offset, Vector3.right)* cannonCam.transform.forward * projectileSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.F) && projectileTrigger && canonEnter.inside) {
            GameObject explosion = Instantiate(prefabExplosion) as GameObject;
            explosion.transform.position = projectile.transform.position;
            canonEnter.SetArrowState(true);

            Object.Destroy(projectile);
            projectileTrigger = false;
        }

        if (arrowTimer != null) {
            arrowTimer.Update();
        }
    }

    private void ShakeCanon() {
        Vector3 pos = shaker.transform.position;
        shaker.transform.position = new Vector3(pos.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount, pos.y, pos.z);
    }

    private void reactivateArrow() {
        Debug.Log("hello");
        arrowTimer = null;
        canonEnter.SetArrowState(true);
    }

}
