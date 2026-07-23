using UnityEngine;

namespace Enemies
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] private float health;

        public float TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                DoDie();
            }

            return damage;
        }

        private void DoDie()
        {
            Destroy(gameObject);
        }
    }
}