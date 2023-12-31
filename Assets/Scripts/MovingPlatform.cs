using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Transform[] points;
    int i = 0;
    bool reverse = false;
    bool canMove;

    private void Start()
    {
        transform.position = points[0].transform.position;
        i = 0;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (Vector3.Distance(transform.position, points[i].transform.position) < 0.01f)
            {
                if (i == points.Length - 1)
                    reverse = true;
                else if (i == 0)
                    reverse = false;

                i = reverse ? i - 1 : i + 1;
            }            
        }
        else
        {
            if (Vector3.Distance(transform.position, points[i].transform.position) < 0.01f && i > 0)
                i--;
        }

        transform.position = Vector3.MoveTowards(transform.position, points[i].position, moveSpeed * Time.fixedDeltaTime);
    }


    void OnCharacterExit(PlayerController controller)
    {
        canMove = false;
        controller.transform.parent = null;
    }

    void OnCharacterEnter(PlayerController controller)
    {
        canMove = true;
        controller.transform.parent = gameObject.transform;
    }
}