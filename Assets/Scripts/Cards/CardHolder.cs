using System;
using Unity.Mathematics;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public Card Card;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float nextPos;
    [SerializeField] private float moveSpeed;
    private float StandardZOffset;
    public int Index;
    private Vector3 scale;
    
    public void Init(Card card, int index)
    {
        Card = card;
        scale = transform.localScale;
        nextPos = transform.localPosition.x;
        StandardZOffset = transform.localPosition.z;
        Index = index;
        transform.localPosition = new Vector3(3000, transform.localPosition.y, (int)transform.localPosition.z - Index);
    }

    public void MoveToPosition(float pos, int index)
    {
        nextPos = pos;
        Index = index;
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(nextPos, transform.localPosition.y, StandardZOffset - Index);
        
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition, 
            targetPos, 
            moveSpeed * Time.deltaTime
        );
    }

    private bool IsPreviewing;
    public void ToggleHover()
    {
        if (!IsPreviewing)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                StandardZOffset + 50);
            transform.localScale *= 1.5f;
            IsPreviewing = true;
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                StandardZOffset - Index);
            transform.localScale = scale;
            IsPreviewing = false;
        }
    }
}
