using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dummy.GameMessage;

namespace Dummy.Domain.Logic
{
 
    
    public static class CardDealer
    {
        public static int CardsCount  = 52;
        public static int UserCardsCount = 5;

        public static List<PokerCard> _cards = new List<PokerCard>();

        static CardDealer()
        {
            for (int i = 2; i <= 14; ++i )
            {
                for (int j = 0; j < 4; ++j)
                {
                    _cards.Add(new PokerCard((CardPoint)i, (CardColor)j));
                }
            }
        }

        public static void DealCard(int userCount, out List<List<PokerCard>> allUserCards, out List<PokerCard> bottomCards)
        {
            allUserCards = new List<List<PokerCard>>();
            bottomCards = new List<PokerCard>();
            Random ra = new Random();
            List<PokerCard> tempCards = new List<PokerCard>(CardsCount);
            for (int userIndex = 0; userIndex < userCount; ++userIndex)
            {
                List<PokerCard> oneUserCards = new List<PokerCard>();
                for (int icard = 0; icard < UserCardsCount; ++icard)
                {
                    int randIndex = ra.Next(0, _cards.Count - 1);
                    PokerCard card = _cards[randIndex];
                    card.Status = CardStatus.CardInHand;
                    if (card.IsSpecialCard())
                    {
                        card.Status |= CardStatus.CardSpecialPoint;
                    }
                    
                    oneUserCards.Add(card);
                    tempCards.Add(card);
                    _cards.RemoveAt(randIndex);
                }
                allUserCards.Add(oneUserCards);
            }
            int leftCount = _cards.Count;
            for (int i = 0; i < leftCount; ++i)
            {
                int randIndex = ra.Next(0, _cards.Count - 1);
                PokerCard card = _cards[randIndex];
                card.Status = CardStatus.CardInBottom;
                if (card.IsSpecialCard())
                {
                    card.Status |= CardStatus.CardSpecialPoint;
                }
                bottomCards.Add(card);
                tempCards.Add(card);
                _cards.RemoveAt(randIndex);
            }
            _cards = tempCards;
        }
    }
}
