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
    private Vector3 previouseVector;
    private bool shakeFlag = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canonEnter.inside) {
            Vector3 rotationVector = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0.0F);
            transform.Rotate(rotationVector * speed * Time.deltaTime);

            /*Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);*/
            if (shakeFlag) {
                if (rotationVector != previouseVector) {
                    ShakeCanon();
                }
            }
            shakeFlag = true;
        }

        //shooting
        if (Input.GetKeyDown(KeyCode.Space) && !projectileTrigger && canonEnter.inside) {
            Debug.Log("SpaceBar");
            projectileTrigger = true;

            projectile = Instantiate(prefabBullet) as GameObject;
            projectile.transform.position = spawner.transform.position;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = Quaternion.AngleAxis(offset, Vector3.right)* cannonCam.transform.forward * projectileSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && projectileTrigger && canonEnter.inside) {
            GameObject explosion = Instantiate(prefabExplosion) as GameObject;
            explosion.transform.position = projectile.transform.position;

            Object.Destroy(projectile);
            projectileTrigger = false;
        }
    }

    private void ShakeCanon() {
        Debug.Log("shaking");
        Vector3 pos = shaker.transform.position;
        shaker.transform.position = new Vector3(pos.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount, pos.y, pos.z);
    }

}
