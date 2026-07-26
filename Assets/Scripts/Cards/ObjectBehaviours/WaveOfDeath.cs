using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.ObjectBehaviours
{
    public class WaveOfDeath : MonoBehaviour
    {
        private float TimePassed;
        private float Duration;
        private float Damage;
        private float Speed;
        private float AddedTime;
        private Vector3 dir;

        private List<IDamageable> hits = new();

        public void Init(float duration, float size, float damage, float speed, float addedTime, Vector2 mouseDir)
        {
            TimePassed = 0;
            Duration = duration;
            transform.localScale *= size;
            Damage = damage;
            Speed = speed;
            AddedTime = addedTime;
            hits = new();
            dir = mouseDir;
            transform.localPosition = (Vector3)ContextManager.Instance.CardCtx.PlayerPosition + Constants.GetDepth();
            transform.localRotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.down, dir));
            DamageMediator.OnDealDamageEnd += OnKill;
        }

        private void Update()
        {
            transform.localPosition += dir * (Speed * Time.deltaTime);
            TimePassed += Time.deltaTime;
            if (TimePassed > Duration)
            {
                Destroy(gameObject);
            }
        }

        private void OnDisable()
        {
            DamageMediator.OnDealDamageEnd -= OnKill;
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("Enemy"))
            {
                var id = other.GetComponent<IDamageable>();
                hits.Add(id);
                var instance = new DamageInstance(id, TimerManager.Instance, ETargetType.Enemy, Damage);
                DamageMediator.DealDamage(instance);
            }
        }

        private void OnKill(DamageInstance instance)
        {
            if (instance.IsDead)
            {
                if (hits.Contains(instance.Target))
                {
                    TimerManager.Instance.UpdateTimer(AddedTime);
                }
            }
        }
    }
}