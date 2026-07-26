using System;
using UnityEngine;

namespace Cards.ObjectBehaviours
{
    public class BatStorm : MonoBehaviour
    {
        private float Damage, ExpansionRate, Duration, RotationSpeed, PassedTime;
        private Transform PlayerPos;
        private AnimationCurve Curve;
        [SerializeField] private SpriteRenderer sr;
        public void Init(float angle, float size, float damage, float expansionRate, float duration, float rotationSpeed, Transform playerPos, float StartOffset, AnimationCurve curve)
        {
            PlayerPos = playerPos;
            transform.localScale *= size;
            Damage = damage;
            ExpansionRate = expansionRate;
            Duration = duration;
            RotationSpeed = rotationSpeed;
            Curve = curve;
            transform.position = PlayerPos.position + Vector3.up * StartOffset;
            transform.position = new Vector3(transform.position.x, transform.position.y, Constants.DEPTH);
            transform.RotateAround(PlayerPos.position, Vector3.forward, angle);
        }

        private void Update()
        {
            PassedTime += Time.deltaTime;
            if (PassedTime > Duration)
            {
                Destroy(gameObject);
            }

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Curve.Evaluate(PassedTime + 0.0001f / Duration));
            transform.position += (transform.position - PlayerPos.position).normalized * (ExpansionRate * Time.deltaTime);
            transform.RotateAround(PlayerPos.position, Vector3.forward, RotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                var instance = new DamageInstance(other.GetComponent<IDamageable>(),TimerManager.Instance, ETargetType.Enemy, Damage);
                DamageMediator.DealDamage(instance);
            }
        }
    }
}