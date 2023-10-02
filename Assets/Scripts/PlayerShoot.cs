using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 100, Color.green); 
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit)
            && hit.collider.TryGetComponent(out DamagableComponent damagable))
        {
            Debug.Log("You can damage me");
        }
    }
}
