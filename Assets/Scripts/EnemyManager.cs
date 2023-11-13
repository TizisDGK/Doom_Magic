using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class EnemyManager
{
    //здесь хранятся все враги

    static HashSet<DamagableComponent> damagableComponents = new HashSet<DamagableComponent>();

    public static IReadOnlyCollection<DamagableComponent> Enemies => damagableComponents;

    public static void RegisterEnemy(DamagableComponent damagable)
    {
        damagableComponents.Add(damagable);
    }

    public static void UnregisterEnemy(DamagableComponent damagable)
    {
        damagableComponents.Remove(damagable);
    }


    public static DamagableComponent GetFirstVisibleTarget(
        Transform sourceTransform,
        float coneAngle,
        Affiliation affiliation,
        float maxDistance)
    {
        //Debug.DrawLine(transform.position, transform.position + transform.forward * 100, Color.green);

        foreach (DamagableComponent enemy in EnemyManager.Enemies.Where(damagable => (damagable.Affiliation & affiliation) > 0))
        {
            // enemy.Affiliation.HasFlag(affiliation);
            //transform.forward;
            Vector3 enemyDirection = (enemy.transform.position - sourceTransform.position);
            if (enemyDirection.sqrMagnitude > maxDistance * maxDistance)
                continue;

            Vector3 enemyDirection2D = enemyDirection; //трансформируем в 2д направление
            enemyDirection2D.y = 0;
            enemyDirection2D = enemyDirection2D.normalized;

            enemyDirection = enemyDirection.normalized;


            float angle = Mathf.Acos(Vector3.Dot(sourceTransform.forward, enemyDirection2D)) * Mathf.Rad2Deg; // смотрим угол


            if (angle < coneAngle)
            {
                CapsuleCollider enemyCollider = enemy.GetComponent<CapsuleCollider>();
                Vector3 unitFrac = new Vector3(0, enemyCollider.height / 2);
                RaycastHit hit;


                if (AimLineAttack(sourceTransform, enemy.transform.position)
                    || AimLineAttack(sourceTransform, enemy.transform.position + unitFrac)
                    || AimLineAttack(sourceTransform, enemy.transform.position - unitFrac))
                {
                    return enemy;
                }
            }
        }
        return null;
    }

    static bool AimLineAttack(Transform sourceTransform, Vector3 targetPos)
    {
        if (Physics.Linecast(sourceTransform.position, targetPos, out RaycastHit hit)
                    && hit.collider.GetComponent<DamagableComponent>()) //проверяем что во что-то попали и что это врага
        {
            Debug.DrawLine(sourceTransform.position, targetPos, Color.green);
            return true;
        }
        return false;
    }
}
