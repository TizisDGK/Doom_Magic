using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float sensitivity = 1;

    float verticalSpeed;
    Vector3 surfaceNormal;

    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
      

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float xMouseMovement = Input.GetAxis("Mouse X");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = Vector3.ProjectOnPlane(moveDirection, surfaceNormal);

        Debug.DrawLine(transform.position, transform.position + moveDirection * 2, Color.blue);

        transform.Rotate(new Vector3(0, xMouseMovement * sensitivity * Time.deltaTime, 0));

        if (characterController.isGrounded)
            verticalSpeed = 0;
        else
            verticalSpeed -= 9.8f * Time.deltaTime;


        characterController.Move((moveDirection * speed + Vector3.up * verticalSpeed) * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.DrawLine(hit.point, hit.point + hit.normal * 10, Color.red);

        surfaceNormal = hit.normal;

    }

}
