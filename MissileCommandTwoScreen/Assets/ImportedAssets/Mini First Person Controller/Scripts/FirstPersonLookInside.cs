using UnityEngine;

public class FirstPersonLookInside : MonoBehaviour
{
    [SerializeField]
    Transform character;
    static Transform characterStatic;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;


    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovementInside>().transform;
        characterStatic = character;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
        characterStatic = character;
    }

    void Update()
    {
        if (FirstPersonMovementInside.movementIsActive) {
            // Get smooth velocity.
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);

            // Rotate camera up-down and controller left-right from velocity.
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
        }
        else {
            character.localRotation = Quaternion.AngleAxis(0, Vector3.down);
            character.localRotation = Quaternion.AngleAxis(0, Vector3.right);
        }
    }

    public static void ResetView() {
        Debug.Log("hellooooo");
        characterStatic.localRotation = Quaternion.AngleAxis(0, Vector3.up);
        characterStatic.localRotation = Quaternion.AngleAxis(0, Vector3.right);
    }

}
