using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    IEnumerator damageRoutine;
    [SerializeField] int damageAmount = 10;
    // Start is called before the first frame update

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
        StopCoroutine(damageRoutine);
    }

    void OnCharacterEnter(PlayerController controller)
    {
        StartCoroutine(damageRoutine = ContiniousDamage(controller.gameObject.GetComponent<DamagableComponent>()));
    }
}
