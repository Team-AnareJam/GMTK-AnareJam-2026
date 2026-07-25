using System;
using System.Collections;
using NaughtyAttributes;
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
    private float StandardYOffset;
    private float StandardZOffset;
    private Vector3 scale;
    [SerializeField] private float previewZValue;
    [SerializeField] private float PreviewYOffset;

    [SerializeField] private float TargetScale;
    [SerializeField] private SpriteRenderer CardBorder;
    [SerializeField] private SpriteRenderer CardArt;
    [SerializeField] private SpriteRenderer CardArtBG;
    [SerializeField] private TMP_Text Cost;
    [SerializeField] private TMP_Text Name;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private TMP_Text Credits;
    [SerializeField] private bool Shop;
    [SerializeField] private bool Flipped;

    [SerializeField] private Sprite AttackBorder;
    [SerializeField] private Sprite SkillBorder;
    [SerializeField] private Sprite StatusBorder;
    
    [SerializeField] private Sprite Attack;
    [SerializeField] private Sprite Skill;
    [SerializeField] private Sprite Status;

    public void Init(Card card, int index)
    {
        Card = card;
        scale = transform.localScale;
        nextPos = transform.localPosition.x;
        StandardYOffset = transform.localPosition.y;
        StandardZOffset = transform.localPosition.z;
        Index = index;
        transform.localPosition = new Vector3(3000, transform.localPosition.y, (int)transform.localPosition.z - Index);
        ShowCard();
    }

    private void ShowCard()
    {
        CardBorder.sprite = Card.cardType switch
        {
            CardType.Attack => AttackBorder,
            CardType.Skill => SkillBorder,
            CardType.Status => StatusBorder,
            _ => throw new ArgumentOutOfRangeException()
        };
        CardArt.sprite = Card.Art;
        Cost.text = $"{Card.Cost}";
        Name.text = Card.Name;
        CardArtBG.sprite = Card.cardType switch
        {
            CardType.Attack => Attack,
            CardType.Skill => Skill,
            CardType.Status => Status,
            _ => throw new ArgumentOutOfRangeException()
        };
        Description.text = Card.Description;
        Credits.text = Card.Credits;
    }


    private float progress;
    [SerializeField] private float FlipTime;
    
    public void rotate(bool flip, float RotationSpeed, float WaitTime)
    {
        StartCoroutine(RotateCard(flip, RotationSpeed,WaitTime));
    }

    private IEnumerator RotateCard(bool flip, float RotationTime, float WaitTime)
    {
        yield return new WaitForSecondsRealtime(WaitTime);
        Flipped = flip;
        progress = 0;
        Debug.Assert(FlipTime != 0);
        var goal = Flipped ? 180 : 0; 
        var start = transform.localEulerAngles.y;
        while (Mathf.Abs(transform.localEulerAngles.y - goal) > 1)
        {
            transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, Mathf.LerpAngle(start, goal, progress / RotationTime), transform.localEulerAngles.z);
            progress += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, goal, transform.localEulerAngles.z);
    }

    public void ChangeCard(Card newCard)
    {
        Card = newCard;
        ShowCard();
    }

    public void MoveToPosition(float pos, int index)
    {
        nextPos = pos;
        Index = index;
    }

    private void FixedUpdate()
    {
        if (Shop) return;
        Vector3 targetPos = new Vector3(nextPos, IsPreviewing ? StandardYOffset + PreviewYOffset : StandardYOffset,
            IsPreviewing ? previewZValue : StandardZOffset - Index);

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
            }
            else
            {
                transform.localScale = scale;
            }
        }
    }


    public void ToggleHover(bool toggle)
    {
        IsPreviewing = toggle;
    }
}