using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    [SerializeField] int heal = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<DamagableComponent>(out DamagableComponent damagableComponent))
        {
            damagableComponent = other.gameObject.GetComponent<DamagableComponent>();
            damagableComponent.Hp += heal;
            Debug.Log($"{damagableComponent.Hp} current HP");
            Destroy(gameObject);
        }
    }
}
