using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableComponent : MonoBehaviour
{
    [SerializeField] int hp = 100;

    int currentHp;

    bool isDead;

    public void Start()
    {
        currentHp = hp;
    }

    public bool IsDead => isDead;

    public int Hp
    {
        get => currentHp;
        set
        {
            if (isDead)
                return;

            currentHp = value;

            if(currentHp <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} is dead");
        isDead = true;
    }

    private void OnEnable()
    {
        EnemyManager.RegisterEnemy(this);
    }

    private void OnDisable()
    {
        EnemyManager.UnregisterEnemy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heal"))
        {
            currentHp += 20;
            Destroy(other.gameObject);
            if (currentHp > hp)
                currentHp = hp;
            Debug.Log($"{currentHp} current HP");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            StartCoroutine("ContiniousDamage");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            StopCoroutine("ContiniousDamage");
        }
    }

    IEnumerator ContiniousDamage()
    {
        while (true)
        {            
            yield return new WaitForSeconds(1); 
            currentHp -= 10; 
            if (currentHp <= 0)
            {
                StopCoroutine("ContiniousDamage");
                Die();
            }
            Debug.Log($"{currentHp} current HP");
        }
    }
}
