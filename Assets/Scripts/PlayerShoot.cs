using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] UIAim aim;

    // Update is called once per frame

    void Update()
    {
        DamagableComponent damagable 
            = EnemyManager.GetFirstVisibleTarget(transform, 3, Affilation.Demon | Affilation.Neutral, 30);

        aim.canShoot = damagable != null;
    }

}
