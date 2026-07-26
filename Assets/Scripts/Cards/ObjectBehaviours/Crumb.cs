using System;
using UnityEngine;

namespace Cards.ObjectBehaviours
{
    public class Crumb : MonoBehaviour
    {
        private float TimePassed;
        public float CrumbSize;
        public float CrumbDamageFrequency;
        public float Damage;

        public void Init(float crumbDuration, float crumbSize, float crumbDamageFrequency, float damage)
        {
            CrumbSize = crumbSize;
            CrumbDamageFrequency = crumbDamageFrequency;
            Damage = damage;
            Destroy(gameObject, crumbDuration);
        }

        public void Update()
        {
            TimePassed += Time.deltaTime;
            if (TimePassed <= CrumbDamageFrequency)
            {
                return;
            }

            TimePassed = 0;


            foreach (var hit in Physics.OverlapSphere(transform.position, CrumbSize))
            {
                if (hit.CompareTag("Enemy"))
                {
                    var instance = new DamageInstance(hit.GetComponent<IDamageable>(),TimerManager.Instance, ETargetType.Enemy, Damage);
                    DamageMediator.DealDamage(instance);
                }
            }
        }
    }
}