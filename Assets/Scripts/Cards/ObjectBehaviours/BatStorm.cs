using System;
using UnityEngine;

namespace Cards.ObjectBehaviours
{
    public class BatStorm : MonoBehaviour
    {
        private float Damage, ExpansionRate, Duration, RotationSpeed;
        private Transform PlayerPos;
        public void Init(float angle, float size, float damage, float expansionRate, float duration, float rotationSpeed, Transform playerPos, float StartOffset)
        {
            PlayerPos = playerPos;
            transform.localScale *= size;
            Damage = damage;
            ExpansionRate = expansionRate;
            Duration = duration;
            RotationSpeed = rotationSpeed;
            transform.position = PlayerPos.position + Vector3.up * StartOffset;
            transform.position = new Vector3(transform.position.x, transform.position.y, Constants.DEPTH);
            transform.RotateAround(PlayerPos.position, Vector3.forward, angle);
        }

        private void Update()
        {
            
            transform.position += (transform.position - PlayerPos.position).normalized * (ExpansionRate * Time.deltaTime);
            transform.RotateAround(PlayerPos.position, Vector3.forward, RotationSpeed * Time.deltaTime);
        }
    }
}