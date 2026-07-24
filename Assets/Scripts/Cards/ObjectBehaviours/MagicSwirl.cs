using Enemies;
using NaughtyAttributes;
using UnityEngine;

namespace Cards.ObjectBehaviours
{
    public class MagicSwirl : MonoBehaviour
    {
        public float Cooldown;
        private float TimeSinceLastDamage = 0;
        public int Damage;
        public float Radius;
        private float spawnTime;
        private float lifetime;
        [SerializeField] private SpriteRenderer visual;
        [SerializeField] private float rotationSpeed;
        private bool isInit = false;
        [SerializeField] private AnimationCurve scaleCurve;
        [SerializeField] private AnimationCurve rotationCurve;
        private float angle;

        public void Update()
        {
            if (!isInit) return;
            TimeSinceLastDamage += Time.deltaTime;
            UpdateVisual();
            CheckDamage();
        }

        private void CheckDamage()
        {
            if (!(TimeSinceLastDamage > Cooldown)) return;
            TimeSinceLastDamage = 0;
            var x = Physics.OverlapSphere(transform.position, Radius);
            if (x.Length > 0)
            {
                foreach (var hit in x)
                {
                    if (!hit.CompareTag("Enemy"))
                    {
                        continue;
                    }
                    if (hit.TryGetComponent<EnemyController>(out var controller))
                    {
                        var properly = new DamageInstance(controller, ETargetType.Enemy, Damage);
                        DamageMediator.DealDamage(properly);
                    }
                }
            }
        }

        private void UpdateVisual()
        {
            float t = MathAE.RemapFloat(Time.time, spawnTime, spawnTime + lifetime, 0, 1);
            float scale = scaleCurve.Evaluate(t) * Radius * 2;
            visual.transform.localScale = new Vector3(scale,scale);
            angle += rotationCurve.Evaluate(t) * rotationSpeed;
            visual.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void Init(Vector2 mousepos, float duration, float cooldown, int damage, float radius)
        {
            transform.position = (Vector3)mousepos + Constants.GetDepth();
            Cooldown = cooldown;
            Damage = damage;
            Radius = radius;
            spawnTime = Time.time;
            lifetime = duration;
            Destroy(gameObject, duration);
            isInit = true;
        }

        [Button(enabledMode:EButtonEnableMode.Playmode)]
        public void Test()
        {
            Init(transform.position,5, 1, 1, 1);
        }
    }
}