using System;
using Unity.Mathematics;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public Card Card;
    public bool IsPreviewing;
    public int Index;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float nextPos;
    [SerializeField] private float moveSpeed;
    private float StandardZOffset;
    private Vector3 scale;
    [SerializeField] private float previewZValue;
    [SerializeField] private float TargetScale;
    
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
        Vector3 targetPos = new Vector3(nextPos, transform.localPosition.y, IsPreviewing ? previewZValue : StandardZOffset - Index);
        
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition, 
            targetPos, 
            moveSpeed * Time.deltaTime
        );
        
        if (IsPreviewing)
        {
            if (transform.localScale.magnitude <= (scale * TargetScale).magnitude)
            {
                transform.localScale *= 1.1f;
            }
        }
        else
        {
            if (transform.localScale.magnitude >= scale.magnitude)
            {
                transform.localScale *= 0.9f;
            }
        }
    }

    
    public void ToggleHover(bool toggle)
    {
        IsPreviewing = toggle;
        //transform.localScale = IsPreviewing ? scale * 1.5f : scale;
    }
}
