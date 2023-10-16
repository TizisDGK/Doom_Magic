using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    IEnumerator damageRoutine;
    [SerializeField] int damageAmount = 10;
    [SerializeField] DamagableComponent damagableComponent;
    // Start is called before the first frame update
    void Start()
    {
        damageRoutine = ContiniousDamage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {
            damagableComponent = other.gameObject.GetComponent<DamagableComponent>();
            StartCoroutine(damageRoutine = ContiniousDamage());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {
            StopCoroutine(damageRoutine);
            damagableComponent = null;
        }
    }

    IEnumerator ContiniousDamage()
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
        print($"Lava Player Stay: {controller.name}");
    }

    void OnCharacterEnter()
    {
        print("Lava Player Enter");
    }

    void OnCharacterExit()
    {
        print("Lava Player Exit");
    }
}

