using System;
using System.Collections.Generic;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cards
{
    public class CardStore : MonoBehaviour
    {
        [SerializeField]private GameObject CardSeller;
        [SerializeField] private List<CardHolderHolderHolder> Cards;
        [SerializeField] private GameObject CardPrefab;
        [SerializeField] private GameObject Daddy;
        [SerializeField] private float bonusCardLuck;
        [SerializeField] private float CommonChance;
        [SerializeField] private float RareChance;
        [SerializeField] private float SRareChance;
        [SerializeField] private float LegendaryChance;

        private float NormalDeltaTime;

        public void Awake()
        {
            WaveManager.OnWaveEnd += PrepareStore;
        }

        [Button]
        public void PrepareStore()
        {
            Time.timeScale = 0;
            NormalDeltaTime = Time.fixedDeltaTime;
            Time.fixedDeltaTime = 0;
            bool BonusCard = Random.value < bonusCardLuck / 100;
            for (int i = 0; i < ( BonusCard ? 3 : 4); i++)
            {
                CardRarity rarity = CardRarity.Common;
                var rnd = Random.value;
                if (rnd < LegendaryChance / 100)
                {
                    rarity = CardRarity.Legendary;
                }
                else if (rnd < (LegendaryChance + SRareChance) / 100)
                {
                    rarity = CardRarity.SuperRare;
                }
                else if (rnd < (LegendaryChance + SRareChance + RareChance) / 100)
                {
                    rarity = CardRarity.Rare;
                }
                
                Cards[i].CardHolder.ChangeCard(LibraryManager.Instance.CardLibrary.GetCardByRarity(rarity));
            }

            InputManager.Instance.ToggleActionMap(InputManager.Actions.UI);
            ShowStore(BonusCard);
        }

        private void ShowStore(bool BonusCard)
        {
            Cards[3].CardHolderHolder.SetActive(BonusCard);
            Debug.Log("ooioioii");
            CardSeller.SetActive(true);
        }

        private void CloseStore()
        {
            Time.timeScale = 1;
            CardSeller.SetActive(false);
        }
    }

    [Serializable]
    public class CardHolderHolderHolder
    {
        public GameObject CardHolderHolder;
        public CardHolder CardHolder;
    }
}