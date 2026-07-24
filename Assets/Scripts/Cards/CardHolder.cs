using System;
using System.Globalization;
using System.Text;
using TMPro;
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
    [SerializeField]private SpriteRenderer Background;
    [SerializeField]private SpriteRenderer CardArt;
    [SerializeField]private TMP_Text Cost;
    [SerializeField]private TMP_Text Name;
    [SerializeField]private TMP_Text Description;
    [SerializeField]private TMP_Text Credits;
    
    
    public void Init(Card card, int index)
    {
        Card = card;
        scale = transform.localScale;
        nextPos = transform.localPosition.x;
        StandardZOffset = transform.localPosition.z;
        Index = index;
        transform.localPosition = new Vector3(3000, transform.localPosition.y, (int)transform.localPosition.z - Index);
        ShowCard();
    }

    private void ShowCard()
    {
        Background.sprite = Card.Background;
        CardArt.sprite = Card.Art;
        Cost.text = $"{Card.Cost}";
        Name.text = Card.Name;
        Description.text = Card.Description;
        Credits.text = Card.Credits;
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
            if (transform.localScale.magnitude < (scale * TargetScale).magnitude)
            {
                transform.localScale *= 1.1f;
            }
            else
            {
                transform.localScale = scale * TargetScale;
            }
        }
        else
        {
            if (transform.localScale.magnitude > scale.magnitude)
            {
                transform.localScale *= 0.9f;
            }else
            {
                transform.localScale = scale;
            }
        }
    }

    
    public void ToggleHover(bool toggle)
    {
        IsPreviewing = toggle;
        //transform.localScale = IsPreviewing ? scale * 1.5f : scale;
    }
}
