using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 1;
    [SerializeField] float sideSpeed = 1;
    [SerializeField] float sensitivity = 1;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float xMouseMovement = Input.GetAxis("Mouse X");

        float rotation = xMouseMovement * sensitivity * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0);

        Vector3 force = Vector3.zero;
        force += transform.forward * vertical * Time.fixedDeltaTime * forwardSpeed;
        force += transform.right * horizontal * Time.fixedDeltaTime * sideSpeed;

        rb.AddForce(force);
    }
}
