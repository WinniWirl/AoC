using System.Text.RegularExpressions;
using AoC_Day;
using Helper;

namespace AoC2023_Day7
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<Hand> hands = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(line => new Hand(line)).ToList();
            hands = hands.OrderDescending().ToList();
            hands.ForEach(hand => Console.WriteLine($"{hand.hand} is {hand.findTypeFunction()}"));
            int result = 0;

            hands.ForEachIndexed((hand, index) => {
                result += hand.bid * (index + 1);
            });
            Console.WriteLine($"Solution Day {day} Part 1: {result}");
        }

        public void SolvePart2()
        {
            List<Hand> hands = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(line => new Hand(line, true)).ToList();
            hands = hands.OrderDescending().ToList();
            hands.ForEach(hand => Console.WriteLine($"{hand.hand} is {hand.findTypeFunction()}"));
            int result = 0;

            hands.ForEachIndexed((hand, index) => {
                result += hand.bid * (index + 1);
            });
            Console.WriteLine($"Solution Day {day} Part 2: {result}");
        }
    }

    class Hand : IComparable<Hand> {
        public string hand;
        public int bid;
        public Func<HandType> findTypeFunction;
        bool jokerIsWildcard;

        public Hand(string line, bool jokerIsWildcard = false) {
            List<string> tmp = line.Split(" ").ToList();
            this.hand = tmp[0];
            this.bid = int.Parse(tmp[1]);
            this.jokerIsWildcard = jokerIsWildcard;
            this.findTypeFunction = jokerIsWildcard ? GetBestTypeWithJoker : GetBestType;
        }

        private HandType GetBestType(char[] handAsChar) 
        {
            string orderedHand = new string(handAsChar.Order().ToArray());
            
            if(Regex.Match(orderedHand, @"(.)\1{4}").Success) return HandType.FIVE_OF_A_KIND;
            if(Regex.Match(orderedHand, @"(.)\1{3}").Success) return HandType.FOUR_OF_A_KIND;
            if(Regex.Match(orderedHand, @"(.)\1{2}").Success && Regex.Matches(orderedHand, @"(.)\1{1}").Count == 2) return HandType.FULL_HOUSE; 
            if(Regex.Match(orderedHand, @"(.)\1{2}").Success) return HandType.THREE_OF_A_KIND;
            if(Regex.Matches(orderedHand, @"(.)\1{1}").Count == 2) return HandType.TWO_PAIR;
            if(Regex.Match(orderedHand, @"(.)\1{1}").Success) return HandType.ONE_PAIR;
            return HandType.HIGH_CARD;
        }

        public HandType GetBestType() 
        {
            return GetBestType(this.hand.ToCharArray());
        }

        public HandType GetBestTypeWithJoker()
        {
            HandType best = HandType.HIGH_CARD;
            List<char> possibleCards = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'];
            foreach (char joker in possibleCards)
            {
                char[] handAsCharModified = this.hand.ToCharArray().Select(c => c == 'J' ? joker : c).ToArray();
                if(GetBestType(handAsCharModified) < best) {
                    best = GetBestType(handAsCharModified);
                }
            }
            return best;
        }

        public int CompareTo(Hand? other)
        {
            if(other == null) return 0; 
            if(this.findTypeFunction().CompareTo(other.findTypeFunction())!= 0) 
                return this.findTypeFunction().CompareTo(other.findTypeFunction());
            if(cheatHand().CompareTo(other.cheatHand()) != 0)
                return other.cheatHand().CompareTo(this.cheatHand());
            return 0;
        }

        public string cheatHand(){
            char[] handAsChar = this.hand.ToCharArray();
            string result = "";
            foreach (char c in handAsChar)
            {
                switch (c)
                {
                    case 'T': 
                        result = result + 'A';
                        break;
                    case 'J': 
                        result = result + (jokerIsWildcard ? '1' : 'B');
                        break;
                    case 'Q': 
                        result = result + 'C';
                        break;
                    case 'K': 
                        result = result + 'D';
                        break;
                    case 'A': 
                        result = result + 'E';
                        break;
                    default:
                        result = result + c;
                        break;
                }
            }
            return result;
        }
    }
        enum HandType{
        FIVE_OF_A_KIND,
        FOUR_OF_A_KIND,
        FULL_HOUSE,
        THREE_OF_A_KIND,
        TWO_PAIR,
        ONE_PAIR,
        HIGH_CARD
    }
}