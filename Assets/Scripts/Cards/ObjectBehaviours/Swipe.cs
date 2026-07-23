using System;
using Unity.Mathematics;
using UnityEngine;

namespace Cards.ObjectBehaviours
{
    public class Swipe : MonoBehaviour
    {
        private float TimeInSeconds;
        private float timeElapsed = 0;
        private float Damage;
        private Vector2 pos1;
        private Vector2 pos2;

        private void Init(Vector2 mousedir, float dist, float timeInSeconds, float damage, float angle)
        {
            TimeInSeconds = timeInSeconds;
            Damage = damage;
            var positions = MathAE.SwipePositions(mousedir, angle, dist);
            pos1 = positions.start;
            pos2 = positions.end;
        }

        private void FixedUpdate()
        {
            Vector3.Slerp(pos1, pos2, timeElapsed / TimeInSeconds);
            timeElapsed += Time.fixedDeltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            throw new NotImplementedException();
        }
    }
}