using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileCommand : MonoBehaviour
{
    public GameObject[] targets;
    public Camera canonCamera;
    public GameObject prefab;
    public static List<MissileContainer> missileContainers = new List<MissileContainer>();

    public int farLeftSpawn = 0;
    public int farRightSpawn = 0;

    public int height = 0;

    public float missileSpeed = 15.0F;

    public static string gameState = "firstMissile";

    [Header("TutorialSound")]
    public AudioSource tuto1;
    public AudioSource tuto2;
    public AudioSource tuto3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (gameState) {
            case "firstMissile":
                if (missileContainers.Count == 0 && canonEnter.inside) {
                    firstMissile();
                    tuto1.Play();

                }
                break;
            case "secondMissile":
                if (missileContainers.Count == 0 && canonEnter.inside) {
                    secondMissile();
                    tuto1.Stop();
                    tuto2.Play();
                }
                break;
            case "thirdMissile":
                if (missileContainers.Count == 0 && canonEnter.inside) {
                    thirdMissile();
                    tuto2.Stop();
                    tuto3.Play();
                }
                break;
            case "barrageMissile":
                if (canonEnter.inside) {
                    randomelyControlledBarrage();
                }
                break;
            default:
                break;
        }
    }

    private void Update() {
        cleanMissileContainer();
    }

    private void cleanMissileContainer() {
        //clean the missileContainer List
        foreach (MissileContainer mC in missileContainers) {
            if (mC.getMissileParent().transform.childCount == 0) {
                // we have no children!
                missileContainers.RemoveAt((int)mC.getIndex());
                break;
            }
        }
    }

    public static void changeStateTutorial() {
        if (missileCommand.gameState == "firstMissile") {
            missileCommand.gameState = "secondMissile";
        }
        else if (missileCommand.gameState == "secondMissile") {
            missileCommand.gameState = "thirdMissile";
        }
        else if (missileCommand.gameState == "thirdMissile") {
            missileCommand.gameState = "barrageMissile";
        }
    }

    private void firstMissile() {
            GameObject missile = Instantiate(prefab) as GameObject;
            Vector3 vectorPos = new Vector3(canonCamera.transform.position.x, (float)height, canonCamera.transform.position.z);
            missile.transform.position = vectorPos;

            missile.transform.LookAt(canonCamera.transform);

            Rigidbody rb = missile.GetComponentInChildren<Rigidbody>();
            rb.AddForce(missile.transform.forward * missileSpeed);

        missileContainers.Add(new MissileContainer(missileContainers.Count, missile));
    }

    private void secondMissile() {
        GameObject missile = Instantiate(prefab) as GameObject;
        Vector3 vectorPos = new Vector3(farRightSpawn, (float)height, canonCamera.transform.position.z);
        missile.transform.position = vectorPos;

        missile.transform.LookAt(canonCamera.transform);

        Rigidbody rb = missile.GetComponentInChildren<Rigidbody>();
        rb.AddForce(missile.transform.forward * missileSpeed);

        missileContainers.Add(new MissileContainer(missileContainers.Count, missile));
    }

    private void thirdMissile() {
        Debug.Log("third");
        GameObject missile = Instantiate(prefab) as GameObject;
        Vector3 vectorPos = new Vector3(farRightSpawn, (float)height, farRightSpawn);
        missile.transform.position = vectorPos;

        missile.transform.LookAt(canonCamera.transform);

        Rigidbody rb = missile.GetComponentInChildren<Rigidbody>();
        rb.AddForce(missile.transform.forward * missileSpeed);

        missileContainers.Add(new MissileContainer(missileContainers.Count, missile));
    }

    private void randomelyControlledBarrage() {
        if (Random.Range(0.0F, 1.0F) < 0.007) {
            int xPos = Random.Range(farLeftSpawn, farRightSpawn);
            int zPos = Random.Range(farLeftSpawn, farRightSpawn);

            GameObject missile = Instantiate(prefab) as GameObject;
            Vector3 vectorPos = new Vector3((float)xPos, (float)height, zPos);
            Debug.Log(vectorPos);
            missile.transform.position = vectorPos;


            missile.transform.LookAt(targets[Random.Range(0, 3)].transform);

            Rigidbody rb = missile.GetComponentInChildren<Rigidbody>();
            rb.AddForce(missile.transform.forward * missileSpeed);

            missileContainers.Add(new MissileContainer(missileContainers.Count, missile));
        }
    }


}
