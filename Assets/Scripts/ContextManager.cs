using UnityEngine;

public class ContextManager : MonoBehaviour
{
    public static ContextManager Instance;
    public CardContext CardCtx;
    [SerializeField] private PlayerManager player;

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
        CardLogic.ctx.player = player;
    }
}
