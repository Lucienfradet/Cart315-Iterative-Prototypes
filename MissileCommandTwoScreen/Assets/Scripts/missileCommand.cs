using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileCommand : MonoBehaviour {
    public GameObject[] nonStaticTargets;
    public static GameObject[] targets;
    public Camera canonCamera;
    public GameObject prefab;
    public static List<MissileContainer> missileContainers = new List<MissileContainer>();

    public static string gameState = "tutorial";

    public int farLeftSpawn = 0;
    public int farRightSpawn = 0;

    public int height = 0;

    public float missileSpeed = 15.0F;


    [Header("TutorialSound")]
    public AudioSource tuto1;
    public AudioSource tuto2;
    public AudioSource tuto3;

    public static string tutorialState = "tutorialState";
    public bool tutorialSwitchNonStatic = true;
    public static bool tutorialSwitch;

    public GameObject textToSkipTutorial;

    [Header("barrageMissile")]
    public int waveNumber = 1;
    public bool waveStarted = false;
    public bool waveTransition = false;
    public float spawnLikeliness = 0.007F;
    public float spawnLikelinessIncrementAmount = 0.0005F;
    public int amountInWave = 3;
    public int amountInWaveIncrementAmount = 2;

    // Start is called before the first frame update
    void Start() {
        tutorialSwitch = tutorialSwitchNonStatic;
        targets = nonStaticTargets;

        textToSkipTutorial.SetActive(false);

        if (tutorialSwitch) {
            gameState = "tutorial";
        }
        else {
            gameState = "barrageMissile";
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        switch (gameState) {
            case "tutorial":
                switch (tutorialState) {
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
                }
                break;
            case "barrageMissile":
                if (canonEnter.inside && !waveTransition) {
                    waveStarted = true;
                }
                else if (!canonEnter.inside && waveTransition) {
                    waveTransition = false;
                }
                if (waveStarted) {
                    randomelyControlledBarrage();
                }
                break;
            default:
                break;
        }

    }

    private void Update() {
        cleanMissileContainer();

        if (missileCommand.tutorialSwitch) {
            checkForTutorialSkipInput();
        }

        checkTargetStatus();

        //resetSceneDEBUG
        if (Input.GetKeyDown(KeyCode.J)) {
            Application.LoadLevel("mainGame");
        }
    }

    private static void checkTargetStatus() {
        if (targets.Length > 0) {
            bool TargetStillAlive = false; //assume false
            foreach (GameObject target in targets) {
                if (target.GetComponent<MeshRenderer>().material.color == targetController.aliveMaterial.color) {
                    TargetStillAlive = true;
                }
            }
            if (!TargetStillAlive) {
                killPlayer();
            }
        }
    }

    private static void killPlayer() {
        Application.LoadLevel("mainGame");
    }

    private void displayTutorialSkipMessage() {
        if (canonEnter.inside && missileCommand.tutorialSwitch) {
            textToSkipTutorial.SetActive(true);
        }
        else {
            textToSkipTutorial.SetActive(false);
        }
    }

    private void checkForTutorialSkipInput() {
        if (Input.GetKeyDown(KeyCode.P)) {
            missileCommand.changeTutorialSwitch(false);
            if (missileContainers.Count > 0) {
                foreach (MissileContainer missileCont in missileContainers) {
                    GameObject missile = missileCont.getMissileParent();
                    GameObject.Destroy(missile);
                }
            }
            tuto1.Stop();
            tuto2.Stop();
            tuto3.Stop();
            gameState = "barrageMissile";
        }
        displayTutorialSkipMessage();
    }

    public static void changeTutorialSwitch(bool state) {
        missileCommand.tutorialSwitch = state;
        KeepOnLoad.tutorialState = state;
    }

    private void cleanMissileContainer() {
        //clean the missileContainer List
        if (missileContainers.Count > 0) {
            foreach (MissileContainer mC in missileContainers) {
                if (mC.getMissileParent().transform.childCount == 0) {
                    // we have no children!
                    if (mC.getIndex() < missileContainers.Count) {
                        missileContainers.RemoveAt((int)mC.getIndex());
                        break;
                    }
                }
            }
        }
    }

    public static void changeStateTutorial() {
        if (missileCommand.tutorialState == "firstMissile") {
            missileCommand.tutorialState = "secondMissile";
        }
        else if (missileCommand.tutorialState == "secondMissile") {
            missileCommand.tutorialState = "thirdMissile";
        }
        else if (missileCommand.tutorialState == "thirdMissile") {
            missileCommand.tutorialSwitch = false;
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
        if (missileContainers.Count < amountInWave - 1) {
            if (Random.Range(0.0F, 1.0F) < spawnLikeliness) {
                int xPos = Random.Range(farLeftSpawn, farRightSpawn);
                int zPos = Random.Range(farLeftSpawn, farRightSpawn);

                GameObject missile = Instantiate(prefab) as GameObject;
                Vector3 vectorPos = new Vector3((float)xPos, (float)height, zPos);
                Debug.Log(vectorPos);
                missile.transform.position = vectorPos;


                missile.transform.LookAt(targets[Random.Range(0, 6)].transform);

                Rigidbody rb = missile.GetComponentInChildren<Rigidbody>();
                rb.AddForce(missile.transform.forward * missileSpeed);

                missileContainers.Add(new MissileContainer(missileContainers.Count, missile));
            }
        }
        else {
            waveStarted = false;
            waveTransition = true;
            amountInWave += amountInWaveIncrementAmount;
            waveNumber++;
            spawnLikeliness += spawnLikelinessIncrementAmount;
        }
        
    }


}
