using System;
using System.Collections.Generic;
using System.Text;

namespace CardChallenge
{
    public class Wallet
    {
        public string walletName;

        public int walletId;

        public List<CreditCard> cards;

        // Gets the total interest for all cards in the wallet
        public double GetTotalInterest()
        {
            double totalInterest = 0;

            foreach(CreditCard card in cards)
            {
                totalInterest += card.CalcCardInterest();
            }

            return totalInterest;
        }

    }
}
