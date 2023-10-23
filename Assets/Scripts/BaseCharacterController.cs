using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class BaseCharacterController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float speedUp;

    Vector3 surfaceNormal;
    CharacterController characterController;
    float verticalSpeed;
    GameObject floor;

    GameObject Floor
    {
        get => floor;
        set //вызывается только когда присваивается объект
        {
            if (floor != value) // проверяем равно ли старое значение новому (проверка изменился ли пол
            {
                if (floor != null) //вызываем у старого пола экзит -- то есть мы с него ушли
                    floor.SendMessage("OnCharacterExit", this, SendMessageOptions.DontRequireReceiver);

                if (value != null) // вызываем у нового пола  энтер -- то есть мы в него вошли
                    value.SendMessage("OnCharacterEnter", this, SendMessageOptions.DontRequireReceiver);
            }
            floor = value;
        }
    }

    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();

    }

    protected void Rotate(float angle)
    {
        transform.Rotate(new Vector3(0, angle));
    }

    protected void SetRotation(float angle) //какой будет новый ротейшен
    {
        transform.rotation = Quaternion.EulerAngles((new Vector3(0, angle)));
    }

    protected void MoveLocal(float right, float forward)
    {
        if (characterController.isGrounded) verticalSpeed = -0.1f;
        else verticalSpeed += Physics.gravity.y * Time.deltaTime;

        Vector3 input = new Vector3(right, 0, forward);
        input = Vector3.ClampMagnitude(input, 1);

        Vector3 velocity = transform.TransformDirection(input) * speed;

        Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, surfaceNormal);
        Vector3 adjustedVelocity = slopeRotation * velocity;

        velocity = adjustedVelocity.y < 0 ? adjustedVelocity : velocity;


        velocity.y += verticalSpeed;

        characterController.Move(velocity * Time.deltaTime);


        GroundCheck(); //проверяем на чем стоим
    }

    protected void MoveWorld(float x, float z)
    {
        Vector3 direction = transform.InverseTransformDirection(new Vector3(x, 0, z));

        MoveLocal(direction.x, direction.z);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        Debug.DrawLine(hit.point, hit.point + hit.normal * 10, Color.red);

        surfaceNormal = hit.normal;
        print(hit.collider.name);
    }


    void GroundCheck() //берем физику из позиции персонада, выпускаем луч и проверяем что под персонажем
    {
        if (Physics.Linecast(transform.position, transform.position + Vector3.down * (characterController.height / 2 + 0.1f), out RaycastHit hit))
        {
            //если попали во что-то, то:

            Floor = hit.collider.gameObject; //мы выбираем пол на который попал луч
            Floor.SendMessage("OnCharacterStay", this, SendMessageOptions.DontRequireReceiver); // вызываем каждый кадр пока стоим на полу, что мы все еще стоим на полу 
            //сенд мессндж можно вызвать только у геймобджекта
        }
        else
        {
            Floor = null;
        }

    }
}
