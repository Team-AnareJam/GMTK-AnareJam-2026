using Cards;
using UnityEngine;

public class StoreOpener : MonoBehaviour
{
    public GameObject StorePrefab;
    [SerializeField] private GameObject Hand;
    [SerializeField] private PlayerHand PlayerHand;

    public void Awake()
    {
        WaveManager.OnWaveEnd += PrepareStore;
    }

    public void PrepareStore()
    {
        GameObject go = Instantiate(StorePrefab);
        go.transform.SetParent(gameObject.transform, false);
        
        CardStore store = go.GetComponent<CardStore>();
        store.Hand = Hand;
        store._hand = PlayerHand;
        store.PrepareStore();
    }
}
