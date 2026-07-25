using System.Collections.Generic;
using UnityEngine;

public class Debuff : MonoBehaviour
{
    private CardContext ctx;
    private float timestamp;
    private float duration;
    private int count;
    private EDebuff debuff;
    public void Init(CardContext _ctx,EDebuff _debuff,float _duration,int _count)
    {
        timestamp = Time.time;
        ctx = _ctx;
        debuff = _debuff;
        duration = _duration;
        count = _count;

        if(debuff == EDebuff.Confused)
        {
            PlayerHand.OnDrawCard += ConfuseCard;
        }
    }
    private void OnDisable()
    {
        PlayerHand.OnDrawCard -= ConfuseCard;
    }

    private void Update()
    {
        if(timestamp + duration > Time.time)
        {
            switch (debuff)
            {
                case EDebuff.Slow:
                    float slowAmount = (float)count / 100;
                    if(ctx.playerMovement.MovementMult > slowAmount) ctx.playerMovement.MovementMult = slowAmount;
                    break;
                case EDebuff.Immobile:
                    ctx.playerMovement.CanMove = false;
                    break;
                case EDebuff.Stun:
                    ctx.playerUI.CanPlay = false;
                    break;
                case EDebuff.Confused:
                    if(count <= 0) End();
                    break;
            }
        }
        else
        {
            End();
        }
    }

    private void ConfuseCard(CardHolder card)
    {

    }

    private void End()
    {
        switch (debuff)
        {
            case EDebuff.Slow:
                ctx.playerMovement.MovementMult = 1;
                break;
            case EDebuff.Immobile:
                ctx.playerMovement.CanMove = true;
                break;
            case EDebuff.Stun:
                ctx.playerUI.CanPlay = true;
                break;
        }
        Destroy(gameObject);
    }

}

public enum EDebuff
{
    Slow,
    Immobile,
    Stun,
    Confused
}
