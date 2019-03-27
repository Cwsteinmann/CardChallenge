using System;
using System.Collections.Generic;
using System.Text;

namespace CardChallenge
{
    public class Client
    {
        public int clientId;

        public string clientName;

        // Other client info here

        public List<Wallet> clientWallets;

        // Gets the total interest for all cards in all wallets
        public double GetTotalInterest()
        {
            double totalInterest = 0;

            foreach(Wallet wallet in clientWallets)
            {
                totalInterest += wallet.GetTotalInterest();
            }

            return totalInterest;
        }

        //Gets the total interest for all cards in all wallets, returned as a dict of CardId and TotalInterest amount
        public Dictionary<int, double> GetInterestByCard()
        {
            Dictionary<int, double> cardInterest = new Dictionary<int, double>();

            foreach(Wallet wallet in clientWallets)
            {
                foreach(CreditCard card in wallet.cards)
                {
                    var interest = card.CalcCardInterest();
                    cardInterest.Add(card.cardId, interest);
                }
            }

            return cardInterest;
        }

        // Gets the total interest for all cards in each wallet, returned as a dict of WalletId and TotalInterest amount
        public Dictionary<int, double> GetInterestByWallet()
        {
            Dictionary<int, double> walletInterest = new Dictionary<int, double>();

            foreach (Wallet wallet in clientWallets)
            {
                walletInterest.Add(wallet.walletId, wallet.GetTotalInterest());
            }

            return walletInterest;
        }

        


    }
}
