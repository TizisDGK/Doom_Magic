using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardForce = 700;
    [SerializeField] float sideForce = 700;
    [SerializeField] float speedUp;
    [SerializeField] float sensitivity = 1;
    

    Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    


    // Update is called once per frame
    private void FixedUpdate()
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

        float xMouseMovement = Input.GetAxis("Mouse X");
        

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 force = Vector3.zero;
        force += transform.forward * vertical* Time.fixedDeltaTime * forwardForce;
        force += transform.right * horizontal * Time.fixedDeltaTime * sideForce;

        float rotation = xMouseMovement * sensitivity * Time.deltaTime;

        rb.AddForce(force);
        transform.Rotate(0, rotation, 0);
        //rb.AddForce(new Vector3(horizontal * Time.fixedDeltaTime * forwardForce, 0, vertical * Time.fixedDeltaTime * sideForce));

    }
}
