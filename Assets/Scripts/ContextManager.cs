using UnityEngine;
using UnityEngine.Serialization;

public class ContextManager : MonoBehaviour
{
    public static ContextManager Instance;
    public CardContext CardCtx;
    [SerializeField] private PlayerUIManager playerUI;

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
        CardLogic.ctx.playerUI = playerUI;
    }

    public static GameObject InstantiateObject(GameObject instance)
    {
        return Instantiate(instance);
    }
}
