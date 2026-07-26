using UnityEngine;

namespace Cards.ObjectBehaviours
{
    public class BatStorm : MonoBehaviour
    {
        public void Init(float angle, float size, float damage, float expansionRate, float duration, float rotationSpeed, Vector3 PlayerPos, float StartOffset)
        {
            transform.position = PlayerPos + Vector3.up * StartOffset;
            transform.position.Set(z:10);
            transform.RotateAround(PlayerPos, Vector3.forward, angle);
        }
    }
}