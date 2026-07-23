using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    public Vector2 MovementTarget;
    public float MovementSpeed;
    [SerializeField] private Rigidbody rb;

    private void FixedUpdate()
    {
        Vector2 dir = (Vector2)transform.position - MovementTarget;
        if (dir.magnitude > 0.1f)
        {
            float speedMod = Mathf.Clamp(dir.magnitude, 0.1f, 1f);
            rb.linearVelocity = (dir.normalized * MovementSpeed * speedMod);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

}
