using Cards;
using UnityEngine;

public class ContextManager : MonoBehaviour
{
    public static ContextManager Instance;
    public CardContext CardCtx;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        CardCtx = new();
        CardLogic.ctx = CardCtx;
    }
}
