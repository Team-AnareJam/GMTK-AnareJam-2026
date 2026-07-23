using System;
using Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cards.ObjectBehaviours
{
    public class Heartseeker : MonoBehaviour
    {
        private float dmg;
        private Vector2 dir;
        private float CritChance;
        private float Speed;
        public void Init(float Damage, float TimeToDie, float critChance, float speed, Vector2 direction)
        {
            dmg = Damage;
            Destroy(gameObject, TimeToDie);
            dir = direction;
            CritChance = critChance;
            Speed = speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy")) return;
            var stats = other.GetComponent<EnemyStats>();
            stats.TakeDamage(Random.value < CritChance/100 ? dmg * 2 : dmg);
        }

        public void FixedUpdate()
        {
            transform.position += (Vector3)dir * (Speed * Time.fixedDeltaTime);
        }
    }
}