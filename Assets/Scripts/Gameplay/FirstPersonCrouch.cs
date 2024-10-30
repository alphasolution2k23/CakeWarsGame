using System.Collections;
using UnityEngine;

public class FirstPersonCrouch : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera's transform
    public float crouchHeight = 0.5f; // Height to move the camera down when crouching
    public float crouchSpeed = 5f; // Speed at which the camera moves when crouching

    private bool isCrouching = false;
    private Vector3 standingCameraPosition; // The original position of the camera when standing
    private Vector3 crouchingCameraPosition; // The position of the camera when crouching

    void Start()
    {
        // Store the original position of the camera when standing
        standingCameraPosition = cameraTransform.localPosition;

        // Calculate the position of the camera when crouching
        crouchingCameraPosition = standingCameraPosition - Vector3.up * crouchHeight;
    }

    void Update()
    {
        //if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.C))
        //{
        //    ToggleCrouch();
        //}
        float verticalInput = ControlFreak2.CF2Input.GetAxis("Vertical");
        if (verticalInput != 0f && verticalInput<=-0.5f && !isCrouching)
        {
            StartCoroutine(MoveCamera(crouchingCameraPosition));
        }
        if (verticalInput != 0f && verticalInput >= 0.5f && isCrouching)
        {
            StartCoroutine(MoveCamera(standingCameraPosition));
        }
    }

    //public void ToggleCrouch()
    //{
    //    isCrouching = !isCrouching; // Toggle the crouch state

    //    // If crouching, move the camera down
    //    if (isCrouching)
    //    {
    //        StartCoroutine(MoveCamera(crouchingCameraPosition));
    //    }
    //    else // If uncrouching, move the camera back up
    //    {
    //        StartCoroutine(MoveCamera(standingCameraPosition));
    //    }
    //}

    IEnumerator MoveCamera(Vector3 targetPosition)
    {
        PlayerControllerS.PlayerControllerSInstance.isCrouched = !PlayerControllerS.PlayerControllerSInstance.isCrouched;
        isCrouching = !isCrouching;
        float elapsedTime = 0f;
        Vector3 startingPosition = cameraTransform.localPosition;

        // Move the camera smoothly to the target position
        while (elapsedTime < crouchSpeed)
        {
            cameraTransform.localPosition = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / crouchSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the camera reaches the target position exactly
        cameraTransform.localPosition = targetPosition;
    }
}
