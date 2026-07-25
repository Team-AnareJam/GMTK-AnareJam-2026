using System;
using Unity.Mathematics;
using UnityEngine;

namespace Cards.ObjectBehaviours
{
    public class BreadCrumbling : MonoBehaviour
    {
        public GameObject Crumb;
        private float TimePassed;
        private float Duration;
        private float CrumbDuration;
        private float CrumbSize;
        private float CrumbFrequency;
        private float Damage;
        private float FrequencyPerSecond;

        public void Init(float duration, float crumbDuration, float crumbSize, float damage, float frequencyPerSecond,
            float crumbFrequency)
        {
            Destroy(gameObject, duration);
            CrumbDuration = crumbDuration;
            CrumbSize = crumbSize;
            Damage = damage;
            FrequencyPerSecond = frequencyPerSecond;
            transform.parent = ContextManager.Instance.CardCtx.playerMovement.transform;
            transform.localPosition = Vector3.zero;
            CrumbFrequency = crumbFrequency;
            
        }

        private void Update()
        {
            TimePassed += Time.deltaTime;
            if (TimePassed <= FrequencyPerSecond)
            {
                return;
            }
            
            TimePassed = 0;

            var go = Instantiate(Crumb, transform.position, quaternion.identity);
            var c = go.GetComponent<Crumb>();   
            c.Init(CrumbDuration, CrumbSize, CrumbFrequency, Damage);
        }
    }
}