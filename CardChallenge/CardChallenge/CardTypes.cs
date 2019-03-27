using System;
using System.Collections.Generic;
using System.Text;

namespace CardChallenge
{
    public class CardType : Enumeration
    {
        public static readonly CardType Discover = new CardType(0, "Discover");
        public static readonly CardType Visa = new CardType(1, "Visa");
        public static readonly CardType MasterCard = new CardType(2, "MasterCard");

        private CardType() { }
        private CardType(int value, string displayName) : base(value, displayName) { }

        // Returns the interest amount per card type
        public double GetInterest(CardType type)
        {
            switch (type.DisplayName)
            {
                case "Discover":
                    return .01;
                case "MasterCard":
                    return .05;
                case "Visa":
                    return .1;
                default:
                    return 1;
            }
        }
    }
}

    
