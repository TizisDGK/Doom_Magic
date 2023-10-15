using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float sensitivity = 1;


    [SerializeField] float speedUp;

    Vector3 surfaceNormal;

    CharacterController characterController;

    float verticalSpeed;

    private void Start()
    {
        
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }


    private void Update()
    {
        Vector3 rotation = new Vector3(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime);

        transform.Rotate(rotation);

        if (characterController.isGrounded) verticalSpeed = -0.1f;
        else verticalSpeed += Physics.gravity.y * Time.deltaTime;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1);

        Vector3 velocity = transform.TransformDirection(input) * speed;

        Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, surfaceNormal);
        Vector3 adjustedVelocity = slopeRotation * velocity;

        velocity = adjustedVelocity.y < 0 ? adjustedVelocity : velocity;


        velocity.y += verticalSpeed;

        characterController.Move(velocity * Time.deltaTime);
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        Debug.DrawLine(hit.point, hit.point + hit.normal * 10, Color.red);
       
        surfaceNormal = hit.normal;
        print(hit.collider.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MovingPlatform>())
        {
            this.transform.parent = collision.transform; //двигаемся вместе с платформой
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<MovingPlatform>())
        {
            this.transform.parent = null;
        }
    }
}
