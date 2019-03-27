using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardChallenge
{
    public class ClientGroup
    {
        public string groupName;

        public List<Client> clientList;

        // Gets the total interest for a client, returns a dict of ClientId and TotalInterest amount
        public Dictionary<int, double> GetInterestPerClient()
        {
            Dictionary<int, double> interestPerClient = new Dictionary<int, double>();

            foreach(Client client in clientList)
            {
                interestPerClient.Add(client.clientId, client.GetTotalInterest());
            }

            return interestPerClient;
        }

        // Gets the total interest of each wallet of each client in the client group.  Returns a dict of WalletId and TotalInterest amount
        public Dictionary<int, double> GetInterestPerWallet()
        {
            Dictionary<int, double> interestPerWallet = new Dictionary<int, double>();
            
            foreach(Client client in clientList)
            {
                Dictionary<int, double> tempDict = new Dictionary<int, double>();
                tempDict = client.GetInterestByWallet();

                //Assuming that wallets are added programatically / stored in a DB to prevent duplicate keys
                tempDict.ToList().ForEach(x => interestPerWallet.Add(x.Key, x.Value));
            }

            return interestPerWallet;

        }

    }
}
