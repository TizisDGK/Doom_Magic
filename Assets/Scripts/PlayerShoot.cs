using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //Код проверяет есть ли враги в обзоре игрока 

    [SerializeField] UIAim aim;

    private void Update()
    {
        DamagableComponent damagable = EnemyManager.GetFirstVisibleTarget(transform, 3, Affiliation.Demon | Affiliation.Netral, 30);

        aim.canShoot = damagable != null;
    }

}
