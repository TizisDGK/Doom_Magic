using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingAIState : AIState
{
    public AIController AIController { get; }

    public ChasingAIState(AIController aiController, AIStateMachine stateMachine) : base(stateMachine)
    {
        AIController = aiController;
    }

    IEnumerator chasingCoroutine;

    public override void Enable()
    {
        Coroutines.StartCoroutine(chasingCoroutine = ChasingCoroutine());
    }

    public override void Disable()
    {
        Coroutines.StopCoroutine(chasingCoroutine);
    }

    IEnumerator ChasingCoroutine()
    {
        Vector3 targetPos = Vector3.zero;

        while (true)
        {
            if(AIController.Sense.Target != null) //если не видим цель (герой зашел за угол например), то враг будет идти до последней позиции где видел игрока
            {
                targetPos = AIController.Sense.Target.transform.position;
                AIController.MoveTo(targetPos);
            }

            yield return null;
        }
    }
    
}
