using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagableComponent : MonoBehaviour
{

    [SerializeField] int hp = 100;

    int currentHp;
    bool isDead;

    [SerializeField] Text countHp;

    private void Start()
    {
        currentHp = hp;
    }

    public bool IsDead => isDead; //авто геттер, еще одно проперти(свойство)
    public bool isAlive => !isDead;

    public int Hp //property свойство
    {
        get => currentHp;
        set
        {
            if (isDead)
                return;
            currentHp = value;

            if(currentHp <= 0)
                Die();

        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} is dead");
        isDead = true;
    }

    private void Update()
    {
        countHp.text = currentHp.ToString() + " / " + hp.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("lava"))
        {
            if(currentHp != 0)
            {
                currentHp -= 10;
                Debug.Log("damage");
            }
            else
            {
                Die();
            }
            
        }
        else if (other.gameObject.CompareTag("hill"))
        {
            if (currentHp != 100)
            {
                currentHp += 5;
                Destroy(other.gameObject);
                Debug.Log("hill");
            }
        }
    }

}