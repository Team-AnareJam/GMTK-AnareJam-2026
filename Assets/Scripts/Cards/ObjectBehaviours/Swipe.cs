using System;
using Unity.Mathematics;
using UnityEngine;

namespace Cards.ObjectBehaviours
{
    public class Swipe : MonoBehaviour
    {
        private float Duration;
        private float timeElapsed = 0;
        private float Damage;
        
        
        private Quaternion rot1;
        private Quaternion rot2;

        public void Init(Vector2 mousedir, float dist, float timeInSeconds, float damage, float angle, float Size)
        {
            Duration = timeInSeconds;
            Damage = damage;
            mousedir.Normalize();
            var pos1 = Quaternion.Euler(0, 0, -angle/2) * mousedir;
            var pos2 = Quaternion.Euler(0, 0, angle/2) * mousedir;
            rot1 = Quaternion.FromToRotation(Vector2.up, pos1);
            rot2 = Quaternion.FromToRotation(Vector2.up, pos2);
            
            transform.localScale *= Size;
            transform.localRotation = rot1;
        }

        private void FixedUpdate()
        {
            
            //TODO: do smth with this pos
            transform.localRotation = Quaternion.Slerp(rot1, rot2, timeElapsed / Duration);
            timeElapsed += Time.fixedDeltaTime;
            if (timeElapsed > Duration)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;
            
            var damg = new DamageInstance(other.GetComponent<EnemyController>(),TimerManager.Instance, ETargetType.Enemy, Damage);
            DamageMediator.DealDamage(damg);
        }
    }
}