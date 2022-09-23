using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    public record GameInfo
    {
        public const byte QuantityOfTheChipsForOneSide = 9;
        public const byte QuantityOfTheButtons = 24;
        public const byte TurnWhenPeriodIsChanged = 17;
        public const byte LooserQuantityOfChips = 2;
    }
}
