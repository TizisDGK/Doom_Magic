using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine 
{
    Dictionary<string, AIState> States { get; }

    AIState activeState;

    public AIStateMachine() //конструктор
    {
        States = new Dictionary<string, AIState>();
    }

    public void AddState(string stateId, AIState state)
    {
        States.Add(stateId, state); //добавили в дикшионари по стейт айди стейт

    }

    public void SetActiveState(string stateId)
    {
        activeState?.Disable(); //есть ли у нас активный стейт
        activeState = States[stateId];
        activeState.Enable();
    }
}
