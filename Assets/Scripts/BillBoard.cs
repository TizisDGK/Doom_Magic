using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Transform cashedCamera;
    // Start is called before the first frame update
    void Start()
    {
        cashedCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(cashedCamera.position.x, transform.position.y, cashedCamera.position.z));
    }
}
