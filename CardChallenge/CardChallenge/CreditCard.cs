using System;

namespace CardChallenge
{
    public class CreditCard
    {

        public string cardName;

        public int cardId;

        public CardType cardType;

        public double balance;

        // Gets the amount of interest to be generated for a single month based on the card's type
        public double CalcCardInterest()
        {
            var interest = cardType.GetInterest(cardType);

            return balance * interest;
        }


    }
}
