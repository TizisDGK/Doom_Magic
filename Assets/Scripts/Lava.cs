using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    IEnumerator damageRoutine;
    [SerializeField] int damageAmount = 10;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {
            StartCoroutine(damageRoutine = ContiniousDamage(other.gameObject.GetComponent<DamagableComponent>()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {
            StopCoroutine(damageRoutine);
        }
    }

    IEnumerator ContiniousDamage(DamagableComponent damagableComponent)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            damagableComponent.Hp -= damageAmount;
            Debug.Log($"{damagableComponent.Hp} current HP");
        }
    }

    void OnCharacterStay(PlayerController controller)
    {
        print($"Lava Player Stay: {controller.name}" );
    }

    void OnCharacterExit()
    {
        print("Lava Player Exit");
    }

    void OnCharacterEnter()
    {
        print("Lava Player Enter");
    }
}
