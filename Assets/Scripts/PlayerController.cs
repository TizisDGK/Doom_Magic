using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : BaseCharacterController
{
    
    [SerializeField] float sensitivity = 1;

    protected override void Awake() //если он был протектед то и здесь он останется протектед, переопределяем его
    {
        base.Awake(); //лучше первой строчкой ставить

        Cursor.lockState = CursorLockMode.Locked; //заблокировали курсор
        Cursor.visible = false; //сделали курсор невиидимым
    }


    private void Update()
    {
        Rotate(Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime);

        MoveLocal(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
    }

}
