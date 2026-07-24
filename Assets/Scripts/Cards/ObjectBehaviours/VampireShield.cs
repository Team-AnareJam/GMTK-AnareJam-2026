using System;
using UnityEngine;

namespace Cards.ObjectBehaviours
{
    public class VampireShield : MonoBehaviour
    {
        public int Hits;
        public float TimeRecovered;
        public float Duration;
        private float timePassed;
        public void Init(int hits, float timeRecovered, float duration)
        {
            Hits = hits;
            TimeRecovered = timeRecovered;
            Duration = duration;
            DamageMediator.OnDealDamageStart += StopDamage;
        }

        private void Update()
        {
            if (timePassed > Duration)
            {
                Destroy(gameObject);
            }

            timePassed += Time.deltaTime;
        }

        private void StopDamage(DamageInstance instance)
        {
            if (instance.TType == ETargetType.Player)
            {
                if (instance.Damage > 0)
                {
                    instance.Damage = 0;
                    Hits--;
                    if (Hits <= 0)
                    {
                        Destroy(gameObject);
                    }
                }

                TimerManager.Instance.UpdateTimer(TimeRecovered);
            }
        }

        private void OnDisable()
        {
            DamageMediator.OnDealDamageStart -= StopDamage;
        }
    }
}