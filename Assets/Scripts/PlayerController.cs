using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardForce = 1;
    [SerializeField] float sideForce = 1;

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

        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(horizontal * Time.fixedDeltaTime * forwardForce, 0, vertical * Time.fixedDeltaTime * sideForce));

        

    }

    
}
