using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISense : MonoBehaviour
{
    [SerializeField] float viewDistance = 20;
    [SerializeField] float viewCone = 60;
    [SerializeField] Affilation searchTargets;

    public event Action<DamagableComponent> TargetChanged;

    DamagableComponent target;

    public DamagableComponent Target
    {
        get => target;
        private set
        {
            if (target == value)
                return;

            target = value;
            TargetChanged?.Invoke(target);
            Debug.Log($"Target changed: {(target == null ? "null" : target.gameObject.name)}");
        }
    }

    private void Update()
    {
        Target = EnemyManager.GetFirstVisibleTarget(transform, viewCone, searchTargets, viewDistance);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = new Color(1, 0, 0, 0.2f);
        UnityEditor.Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            Quaternion.AngleAxis(-viewCone * 0.5f, Vector3.up) * transform.forward,
            viewCone,
            viewDistance);
    }
#endif

}
