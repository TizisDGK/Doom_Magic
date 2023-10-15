using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float speedMove = 3f;
    bool movingUp = true;
    bool canMove;

    void Update()
    {
        if (canMove) //если стоит на платформе она двигается вверх-вниз
        {
            if (transform.position.y > 7.5f)
            {
                movingUp = false;
            }
            else if (transform.position.y <= -7f)
            {
                movingUp = true;
            }

            if (movingUp)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speedMove * Time.deltaTime, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speedMove * Time.deltaTime, transform.position.z);
            }
        }
        else //если не стоим на платформе, она опускается обратно вниз
        {
            if(transform.position.y > -7f && transform.position.y != -7f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speedMove * Time.deltaTime, transform.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canMove = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canMove = false;
    }
}
