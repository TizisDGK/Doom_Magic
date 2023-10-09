using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
     

public class UIAim : MonoBehaviour
{

    [SerializeField] Image aimImage;

    public bool canShoot { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aimImage.color = canShoot ? Color.red : Color.white;
        
    }
}
