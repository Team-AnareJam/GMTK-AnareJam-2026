using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public Card Card;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float nextPos;
    [SerializeField] private float moveSpeed;
    public void Init(Card card)
    {
        Card = card;
        transform.position = new Vector3(transform.position.x, 0, 1);
        nextPos = transform.position.x;
    }

    public void MoveToPosition(float pos)
    {
        nextPos = pos;
    }

    private void FixedUpdate()
    {
        // if(-0.1f < nextPos - transform.position.x || nextPos - transform.position.x > 0.1f)
        // {
        //     rb.MovePosition(new Vector3((transform.position.x - nextPos) , 0, 1));
        // }
    }
}
