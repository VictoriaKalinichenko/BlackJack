using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.Randomize
{
    public class GameRandomize : IRandomize
    {

        #region NameGenerate
        public string NameGenerate()
        {
            string name = "";

            Random random = new Random();
            int sizeOfName = random.Next(5, 9);

            char[] consonants = new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' }; // Согласные
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u', 'y', }; // Гласные

            // 0 - гласная, 1 - согласная
            byte[] letterTypeSequence = GenerateLetterTypeSequenceForName(sizeOfName, random);
            
            for (int i = 0; i < sizeOfName; i++)
            {
                if (letterTypeSequence[i] == 0)
                {
                    name += vowels[random.Next(vowels.Length)].ToString();
                }

                if (letterTypeSequence[i] == 1)
                {
                    name += consonants[random.Next(consonants.Length)].ToString();
                }
            }

            return name;
        }

        byte[] GenerateLetterTypeSequenceForName(int sizeOfName, Random random)
        {
            byte[] isVowelLetter = new byte[sizeOfName];

            for (int i = 0; i < sizeOfName; i++)
            {
                isVowelLetter[i] = 0;

                if (i == 0)
                {
                    isVowelLetter[i] = (byte)random.Next(2);
                }

                if (i >= 1 && isVowelLetter[i - 1] == 0)
                {
                    isVowelLetter[i] = 1;
                }

                if (i > 1 && isVowelLetter[i - 1] == 1 && isVowelLetter[i - 2] == 0)
                {
                    isVowelLetter[i] = (byte)random.Next(2);
                }
            }

            return isVowelLetter;
        }

        #endregion

        public int BetGenerate()
        {
            int bet = 0;

            Random random = new Random();
            bet = random.Next(20) * 50 + 50;
            
            return bet;
        }

        public int CardIdSelection(int AmountOfCard)
        {
            int cardId;

            Random random = new Random();
            cardId = random.Next(AmountOfCard) + 1;

            return cardId;
        }
    }
}
