using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardChallenge;
using System.Collections.Generic;

namespace CardTests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        // Get total interest for 1 user with 1 wallet and 3 cards, balance of 100$ per card
        public void SingleUser_SingleWallet_MultipleCards_TotalInterest()
        {
            CreditCard visaCard = new CreditCard() { cardName = "Visa Card", cardId = 1, balance = 100, cardType = CardType.Visa, };
            CreditCard mcCard = new CreditCard() { cardName = "MC Card", cardId = 2, balance = 100, cardType = CardType.MasterCard, };
            CreditCard discoverCard = new CreditCard() { cardName = "Discover Card", cardId = 3, balance = 100, cardType = CardType.Discover, };

            Wallet myWallet = new Wallet() { cards = new List<CreditCard>() { visaCard, mcCard, discoverCard }, walletName = "My First Wallet" };

            Client myClient = new Client() { clientId = 1, clientName = "Test Client", clientWallets = new List<Wallet>() { myWallet } };

            var expectedTotalInterest = 16;
            var calculatedTotalInterest = myClient.GetTotalInterest();

            Assert.AreEqual(expectedTotalInterest, calculatedTotalInterest);
        }

        [TestMethod]
        // Get interest per card for 1 user with 1 wallet and 3 cards, balance of 100$ per card
        public void SingleUser_SingleWallet_MultipleCards_InterestByCard()
        {
            CreditCard visaCard = new CreditCard() { cardName = "Visa Card", cardId = 1, balance = 100, cardType = CardType.Visa, };
            CreditCard mcCard = new CreditCard() { cardName = "MC Card", cardId = 2, balance = 100, cardType = CardType.MasterCard, };
            CreditCard discoverCard = new CreditCard() { cardName = "Discover Card", cardId = 3, balance = 100, cardType = CardType.Discover, };

            Wallet myWallet = new Wallet() { cards = new List<CreditCard>() { visaCard, mcCard, discoverCard }, walletName = "My First Wallet" };

            Client myClient = new Client() { clientId = 1, clientName = "Test Client", clientWallets = new List<Wallet>() { myWallet } };

            var expectedInterestByCard = new Dictionary<int, double>();
            expectedInterestByCard.Add(1, 10);
            expectedInterestByCard.Add(2, 5);
            expectedInterestByCard.Add(3, 1);
            var calculatedInterestByCard = myClient.GetInterestByCard();

            var equal = TestDictEquality(expectedInterestByCard, calculatedInterestByCard);

            Assert.IsTrue(equal);
        }

        [TestMethod]
        // Get interest per wallet for 1 user with multiple wallets
        public void SingleUser_MultipleWallets_InterestByWallet()
        {
            CreditCard visaCard = new CreditCard() { cardName = "Visa Card", cardId = 1, balance = 100, cardType = CardType.Visa, };
            CreditCard mcCard = new CreditCard() { cardName = "MC Card", cardId = 2, balance = 100, cardType = CardType.MasterCard, };
            CreditCard discoverCard = new CreditCard() { cardName = "Discover Card", cardId = 3, balance = 100, cardType = CardType.Discover, };

            Wallet myWallet = new Wallet() { cards = new List<CreditCard>() { visaCard, discoverCard }, walletName = "My First Wallet", walletId = 1 };
            Wallet secondWallet = new Wallet() { cards = new List<CreditCard>() { mcCard }, walletName = "My Second Wallet", walletId = 2 };

            Client myClient = new Client() { clientId = 1, clientName = "Test Client", clientWallets = new List<Wallet>() { myWallet, secondWallet } };

            var expectedInterestByWallet = new Dictionary<int, double>();
            expectedInterestByWallet.Add(1, 11);
            expectedInterestByWallet.Add(2, 5);
            var calculatedInterestByWallet = myClient.GetInterestByWallet();

            var equal = TestDictEquality(expectedInterestByWallet, calculatedInterestByWallet);

            Assert.IsTrue(equal);
        }

        [TestMethod]
        // Get interest for 1 user with multiple wallets
        public void SingleUser_MultipleWallets_TotalInterest()
        {
            CreditCard visaCard = new CreditCard() { cardName = "Visa Card", cardId = 1, balance = 100, cardType = CardType.Visa, };
            CreditCard mcCard = new CreditCard() { cardName = "MC Card", cardId = 2, balance = 100, cardType = CardType.MasterCard, };
            CreditCard discoverCard = new CreditCard() { cardName = "Discover Card", cardId = 3, balance = 100, cardType = CardType.Discover, };

            Wallet myWallet = new Wallet() { cards = new List<CreditCard>() { visaCard, discoverCard }, walletName = "My First Wallet" };
            Wallet secondWallet = new Wallet() { cards = new List<CreditCard>() { mcCard }, walletName = "My Second Wallet" };

            Client myClient = new Client() { clientId = 1, clientName = "Test Client", clientWallets = new List<Wallet>() { myWallet, secondWallet } };

            var expectedInterest = 16;
            var calculatedInterest = myClient.GetTotalInterest();

            Assert.AreEqual(expectedInterest, calculatedInterest);
        }

        [TestMethod]
        // Get interest per person for multiple users with 1 wallet each
        public void MultipleUsers_TotalInterestPerPerson()
        {
            CreditCard visaCard = new CreditCard() { cardName = "Visa Card", cardId = 1, balance = 100, cardType = CardType.Visa, };
            CreditCard mcCard = new CreditCard() { cardName = "MC Card", cardId = 2, balance = 100, cardType = CardType.MasterCard, };
            CreditCard discoverCard = new CreditCard() { cardName = "Discover Card", cardId = 3, balance = 100, cardType = CardType.Discover, };

            Wallet myWallet = new Wallet() { cards = new List<CreditCard>() { mcCard, discoverCard }, walletName = "John's Wallet" };
            Wallet secondWallet = new Wallet() { cards = new List<CreditCard>() { visaCard }, walletName = "Tom's Wallet" };

            Client myClient = new Client() { clientId = 1, clientName = "John", clientWallets = new List<Wallet>() { myWallet } };
            Client client2 = new Client() { clientId = 2, clientName = "Tom", clientWallets = new List<Wallet>() { secondWallet } };

            ClientGroup clientGroup = new ClientGroup() { clientList = new List<Client>() { myClient, client2 }, groupName = "Client Group 1" };


            var expectedInterestByUser = new Dictionary<int, double>();
            expectedInterestByUser.Add(1, 6);
            expectedInterestByUser.Add(2, 10);
            var calculatedInterest = clientGroup.GetInterestPerClient();

            var equal = TestDictEquality(expectedInterestByUser, calculatedInterest);

            Assert.IsTrue(equal);
        }

        [TestMethod]
        // Get interest per wallet for multiple users
        public void MultipleUsers_TotalInterestPerWallet()
        {
            CreditCard visaCard = new CreditCard() { cardName = "Visa Card", cardId = 1, balance = 100, cardType = CardType.Visa, };
            CreditCard mcCard = new CreditCard() { cardName = "MC Card", cardId = 2, balance = 100, cardType = CardType.MasterCard, };
            CreditCard discoverCard = new CreditCard() { cardName = "Discover Card", cardId = 3, balance = 100, cardType = CardType.Discover, };

            Wallet myWallet = new Wallet() { cards = new List<CreditCard>() { mcCard, discoverCard }, walletName = "John's Wallet", walletId = 4 };
            Wallet secondWallet = new Wallet() { cards = new List<CreditCard>() { visaCard }, walletName = "Tom's Wallet", walletId = 3 };

            Client myClient = new Client() { clientId = 1, clientName = "John", clientWallets = new List<Wallet>() { myWallet } };
            Client client2 = new Client() { clientId = 2, clientName = "Tom", clientWallets = new List<Wallet>() { secondWallet } };

            ClientGroup clientGroup = new ClientGroup() { clientList = new List<Client>() { myClient, client2 }, groupName = "Client Group 1" };


            var expectedInterestByUser = new Dictionary<int, double>();
            expectedInterestByUser.Add(4, 6);
            expectedInterestByUser.Add(3, 10);
            var calculatedInterest = clientGroup.GetInterestPerWallet();

            var equal = TestDictEquality(expectedInterestByUser, calculatedInterest);

            Assert.IsTrue(equal);
        }


        public bool TestDictEquality(Dictionary<int, double> dict1, Dictionary<int, double> dict2)
        {
            bool equal = false;
            if (dict1.Count == dict2.Count) // Require equal count.
            {
                equal = true;
                foreach (var pair in dict1)
                {
                    double value;
                    if (dict2.TryGetValue(pair.Key, out value))
                    {
                        // Require value be equal.
                        if (value != pair.Value)
                        {
                            equal = false;
                            break;
                        }
                    }
                    else
                    {
                        // Require key be present.
                        equal = false;
                        break;
                    }
                }
            }
            return equal;
        }

    }
}
