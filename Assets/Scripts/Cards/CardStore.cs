using System;
using System.Collections.Generic;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Cards
{
    public class CardStore : MonoBehaviour
    {
        [SerializeField]private GameObject CardSeller;
        [SerializeField] private List<CardHolderHolderHolder> Cards;
        [SerializeField] private GameObject CardPrefab;
        [SerializeField] private GameObject Hand;
        [SerializeField] private GameObject Button;
        [SerializeField]private GameObject BG;
        [SerializeField] private Button button;
        [SerializeField] private PlayerDeck Deck;
        [SerializeField] private float bonusCardLuck;
        [SerializeField] private float CommonChance;
        [SerializeField] private float RareChance;
        [SerializeField] private float SRareChance;
        [SerializeField] private float LegendaryChance;
        private int SelectedCard;

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
            
            SelectedCard = -1;
            button.interactable = false;
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
            Hand.SetActive(false);
            Button.SetActive(true);
            BG.SetActive(true);
            CardSeller.SetActive(true);
            for (int i = 0; i < Cards.Count - (BonusCard ? 0 : 1); i++)
            {
                Cards[i].CardHolder.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                Cards[i].CardHolder.rotate(false, .1f, i * 0.2f + 0.2f);
            }
        }

        public void ClickCard(int Card)
        {
            SelectedCard = Card;
            button.interactable = true;
        }

        public void CloseStore()
        {
            Time.timeScale = 1;
            InputManager.Instance.ToggleActionMap(InputManager.Actions.Player);
            Hand.SetActive(true);
            Button.SetActive(false);
            BG.SetActive(false);
            Deck.AddCard(Cards[SelectedCard].CardHolder.Card);
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