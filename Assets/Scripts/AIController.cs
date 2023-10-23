using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : BaseCharacterController
{
    bool isMoveToCompleted = true;
    int pathPointIndex;

    NavMeshPath path;

    protected override void Awake()
    {
        base.Awake();

        path = new NavMeshPath();
    }

    protected bool MoveTo(Vector3 targetPos)
    {
        bool hasPath =  NavMesh.CalculatePath(transform.position, targetPos,  NavMesh.AllAreas, path);
        if (hasPath)
        {
            pathPointIndex = 1;
        }

        isMoveToCompleted = !hasPath;


        return hasPath;
    }

    protected virtual void Update()
    {
        if (path.status != NavMeshPathStatus.PathInvalid)
        {
            //нарисовать путь

            for (int i = 0; i < path.corners.Length - 1; i++) // -1 потому что путь от одной точки до следующей точки
            {
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            }
        }

        if (isMoveToCompleted)
            return;

        Vector3 targetPos = path.corners[pathPointIndex];    //вектор куда надо двигаться
        Vector3  sourcePos = transform.position;

        targetPos.y = 0;
        sourcePos.y = 0;

        if(Vector3.Distance(sourcePos, targetPos) < 1) //если растояние меньше, то
        {

            if(pathPointIndex + 1 >= path.corners.Length)
            {
                print("done");
                isMoveToCompleted = true;
                return;
            }

            // пойти к след точке
            pathPointIndex++;
            targetPos = path.corners[pathPointIndex];
            targetPos.y = 0;
        }

        Vector3 direction = (targetPos - sourcePos).normalized;

        MoveWorld(direction.x, direction.z);
    }

}
