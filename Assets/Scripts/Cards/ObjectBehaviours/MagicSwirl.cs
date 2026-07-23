using System;
using System.Collections;
using System.Linq.Expressions;
using Enemies;
using UnityEngine;

namespace Cards.ObjectBehaviours
{
    public class MagicSwirl : MonoBehaviour
    {
        public float Cooldown;
        private float TimeSinceLastDamage = 0;
        public float Damage;
        public float Radius;

        public void Update()
        {
            if (!(TimeSinceLastDamage > Cooldown)) return;
            
            TimeSinceLastDamage = 0;
            var x = Physics.OverlapSphere(transform.position, Radius);
            if (x.Length > 0)
            {
                foreach (var hit in x)
                {
                    if (!hit.CompareTag("Enemy"))
                    {
                        continue;
                    }
                    if (hit.TryGetComponent<EnemyStats>(out var stats))
                    {
                        stats.TakeDamage(Damage);
                    }
                }
            }
        }

        public void Init(Vector2 mousepos, float TimeToDie, float cooldown, float damage, float radius)
        {
            transform.position = mousepos;
            Cooldown = cooldown;
            Damage = damage;
            Radius = radius;
            Destroy(gameObject, TimeToDie);
        }
    }
}