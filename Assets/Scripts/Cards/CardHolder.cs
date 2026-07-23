using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public Card Card;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 nextPos;

    public void Init(Card card)
    {
        Card = card;
        nextPos = transform.position;
    }

    public void MoveToPosition(Vector3 pos)
    {
        nextPos = pos;
    }

    private void FixedUpdate()
    {
        if(nextPos.x - transform.position.x > 0.1f)
        {
            rb.MovePosition(nextPos);
        }
    }
}
