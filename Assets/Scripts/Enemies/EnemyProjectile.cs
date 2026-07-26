using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SpriteRenderer rend;
    private bool initiated;
    private IDamageable origin;
    private int damage;
    public void Init(IDamageable _origin, Sprite sprite, Vector2 dir, float scale, float speed,float lifetime, int _damage)
    {
        initiated = true;
        origin = _origin;
        damage = _damage;

        rend.sprite = sprite;
        transform.localScale = new Vector3(scale, scale, scale);
        transform.localRotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.up, dir));
        rb.linearVelocity = dir * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!initiated) return;
        if (other.CompareTag("Player"))
        {
            DamageInstance instance = new(TimerManager.Instance,origin,ETargetType.Player, damage);
            DamageMediator.DealDamage(instance);
            Destroy(gameObject);
        }
    }
}
