using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonEnter : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject enteringTextObj;
    public GameObject UIOverlay;
    public GameObject UIFire;
    public GameObject UICam;

    [Header("Arrow")]
    public GameObject[] arrow;
    public static GameObject[] arrowStatic;

    [Header("Public")]
    public GameObject player;

    public Camera playerCam;
    public Camera cannonCam;
    public Camera playerCanonCam;

    public static bool inside = false;


    void Start()
    {
        playerCam.enabled = true;
        FirstPersonMovementOutside.movementIsActive = true;
        FirstPersonMovementInside.movementIsActive = false;
        playerCam.GetComponent<AudioListener>().enabled = true;
        playerCanonCam.GetComponent<AudioListener>().enabled = false;
        enteringTextObj.SetActive(false);

        UIOverlay.SetActive(false);
        UIFire.SetActive(false);
        UICam.SetActive(false);

        //set ArrowStatic
        arrowStatic = new GameObject[arrow.Length];
        int i = 0;
        foreach (GameObject gO in arrow) {
            arrowStatic[i] = gO;
            i++;
        }
        SetArrowState(false);
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.E) && enteringTextObj.activeSelf && !inside) {
            inside = true;
            SetArrowState(true);
            //get inside
            FirstPersonMovementOutside.movementIsActive = false;
            FirstPersonMovementInside.movementIsActive = true;
            playerCam.GetComponent<AudioListener>().enabled = false;
            playerCanonCam.GetComponent<AudioListener>().enabled = true;
            FirstPersonLookInside.ResetView();
            enteringTextObj.SetActive(false);

            /*UIOverlay.SetActive(true);
            UIFire.SetActive(true);
            UICam.SetActive(true);*/

            playerCam.enabled = false;
            playerCanonCam.enabled = true;
            Debug.Log("getting inside");
        }
        else if (Input.GetKeyUp(KeyCode.E) && inside) {
            inside = false;
            SetArrowState(false);
            //get outisde
            FirstPersonMovementOutside.movementIsActive = true;
            FirstPersonMovementInside.movementIsActive = false;
            playerCam.GetComponent<AudioListener>().enabled = true;
            playerCanonCam.GetComponent<AudioListener>().enabled = false;

            /*UIOverlay.SetActive(false);
            UIFire.SetActive(false);
            UICam.SetActive(false);*/

            playerCam.enabled = true;
            playerCanonCam.enabled = false;
            enteringTextObj.SetActive(true);
            Debug.Log("getting out!");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            enteringTextObj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            enteringTextObj.SetActive(false);
        }
    }

    public static void SetArrowState(bool state) {
        foreach (GameObject gO in arrowStatic) {
            gO.SetActive(state);
        }
    }
}
