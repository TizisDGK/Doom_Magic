using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //[SerializeField] float forwardForce = 700;
    //[SerializeField] float sideForce = 700;
    [SerializeField] float speed = 10;
    [SerializeField] float sensitivity = 1;


    [SerializeField] float speedUp;

    Vector3 surfaceNormal;

    CharacterController characterController;

    float verticalSpeed;

    // Start is called before the first frame update
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
        
        /*
        float xMouseMovement = Input.GetAxis("Mouse X");
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical); //получили позицию в мировых координатах

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1); //метод позволяющий не выйти за пределы. Мы указали предел 1
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = Vector3.ProjectOnPlane(moveDirection, surfaceNormal);
        //moveDirection = transform.TransformDirection(moveDirection) * speed; //приведение к локальным координатам с помощью Transform Direction

        Debug.DrawLine(transform.position, transform.position + moveDirection * 2, Color.blue);

        transform.Rotate(new Vector3(0, xMouseMovement * sensitivity * Time.deltaTime, 0));

        

        if (characterController.isGrounded)
        {
            verticalSpeed = 0;
            //Vector3 velocity = characterController.velocity;
           // verticalMovement = velocity.y - 9.8f * Time.deltaTime;
        }
        else
        {
            verticalSpeed -= 9.8f * Time.deltaTime;
        }

        characterController.Move((moveDirection * speed + Vector3.up * verticalSpeed) * Time.deltaTime);
        
        /*
        if (characterController.SimpleMove(moveDirection))
        {
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2 ))
            {
                transform.position = hit.point + transform.up * (characterController.height / 2);
            }

        }*/

        //characterController.SimpleMove(moveDirection);
        
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        Debug.DrawLine(hit.point, hit.point + hit.normal * 10, Color.red);
        //Debug.DrawLine(transform.position, transform.position + hit.normal, Color.green);

        surfaceNormal = hit.normal;
        print(hit.collider.name);
    }

    // Update is called once per frame
    /*private void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            forwardForce = forwardForce + speedUp;
            sideForce = sideForce + speedUp;
        }
        else
        {
            forwardForce = 700;
            sideForce = 700;
        }
        

    // float xMouseMovement = Input.GetAxis("Mouse X");
    // float horizontal = Input.GetAxis("Horizontal");
    // float vertical = Input.GetAxis("Vertical");

    Vector3 force = Vector3.zero;
        force += transform.forward * vertical* Time.fixedDeltaTime * forwardForce;
        force += transform.right * horizontal * Time.fixedDeltaTime * sideForce;

        float rotation = xMouseMovement * sensitivity * Time.deltaTime;

        rb.AddForce(force);
        transform.Rotate(0, rotation, 0);
        //rb.AddForce(new Vector3(horizontal * Time.fixedDeltaTime * forwardForce, 0, vertical * Time.fixedDeltaTime * sideForce));

    }*/
}
