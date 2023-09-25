using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardForce = 700;
    [SerializeField] float sideForce = 700;
    [SerializeField] float speedUp;
    

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
            Debug.Log("split");
        }
        else
        {
            forwardForce = 700;
            sideForce = 700;
            Debug.Log("dont split");
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(horizontal * Time.fixedDeltaTime * forwardForce, 0, vertical * Time.fixedDeltaTime * sideForce));

        

    }

    
}
