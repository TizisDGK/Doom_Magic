using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] UIAim aim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(transform.position, transform.position + transform.forward * 100, Color.green);

        foreach(DamagableComponent enemy in EnemyManager.Enemies)
        {
            //transform.forward;
            Vector3 enemyDirection = (enemy.transform.position - transform.position);
            Vector3 enemyDirection2D = enemyDirection;
            enemyDirection2D.y = 0;
            enemyDirection2D = enemyDirection2D.normalized;

            enemyDirection = enemyDirection.normalized;
            

            float angle =  Mathf.Acos(Vector3.Dot(transform.forward, enemyDirection2D)) * Mathf.Rad2Deg;
           

            if (angle < 3)
            {
                CapsuleCollider enemyCollider =  enemy.GetComponent<CapsuleCollider>();
                Vector3 unitFrac = new Vector3(0, enemyCollider.height / 2);
                RaycastHit hit;


                if (AimLineAttack(enemy.transform.position)
                    || AimLineAttack(enemy.transform.position + unitFrac)
                    || AimLineAttack(enemy.transform.position - unitFrac))
                {
                    aim.canShoot = true;
                    return;
                }
          
            }
        }
        aim.canShoot = false;

    }

    bool AimLineAttack(Vector3 targetPos)
    {
        if (Physics.Linecast(transform.position, targetPos, out RaycastHit hit)
                    && hit.collider.GetComponent<DamagableComponent>()) //проверяем что во что-то попали и что это врага
        {
            Debug.DrawLine(transform.position, targetPos, Color.green);
            aim.canShoot = true;
            return true;
        }
        return false;
    }
}
