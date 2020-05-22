using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareNamespace.Classes.Lottery
{
    public class LotteryData
    {
        public bool? didGamble { get; set; }
        public int? guessNumber { get; set; }

        public void Gamble(int number)
        {
            didGamble = true;
            guessNumber = number;
        }

        public void Reset()
        {
            didGamble = false;
            guessNumber = null;
        }
        public override string ToString()
        {
            return $"didGamble: {didGamble} guessNumber: {guessNumber}";
        }
    }
}
