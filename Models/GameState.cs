using System;
using System.Collections.Generic;

namespace NineMensMorris.Models
{
    public static class GameState
    {
        private static Turn _turnOfThePlayer;
        private static PeriodOfTheGame _gamePeriod;
        public static event EventHandler TurnIsChanged;
        public static event EventHandler PeriodIsChanged;
        
        public static event EventHandler<GameStateEventArgs.TwoChipsIsLeftArgs> _twoChipsIsLeftEvent;
 
        public static Dictionary<ButtonPosition, ChipState> ButtonStates;
        public static ushort MoveNumber { get; internal set; }
        public static EventHandler<GameStateEventArgs.TwoChipsIsLeftArgs> TwoChipsIsLeftEvent
        {
            set
            {
                if (_twoChipsIsLeftEvent == null)
                {
                    _twoChipsIsLeftEvent += value;
                }
            }
            get
            {
                return _twoChipsIsLeftEvent;
            }
        }
        public static PeriodOfTheGame GamePeriod
        {
            get => _gamePeriod;
            set
            {
                _gamePeriod = value;
                PeriodIsChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        public static Turn TurnOfThePlayer
        {
            get => _turnOfThePlayer;
            set
            {
                _turnOfThePlayer = value;
                TurnIsChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        public static byte QuantityOfChipsOfPlayer1 { get; internal set; } //TODO: implement scenario
        public static byte QuantityOfChipsOfPlayer2 { get; internal set; }
        public static void IncrementMoveNumber() => MoveNumber++;
        public static void IncrementQuantityOfChipsOfPlayer2() => QuantityOfChipsOfPlayer2++;
        public static void DecrementQuantityOfChipsOfPlayer2() => QuantityOfChipsOfPlayer2--;
        public static void IncrementQuantityOfChipsOfPlayer1() => QuantityOfChipsOfPlayer1++;
        public static void DecrementQuantityOfChipsOfPlayer1() => QuantityOfChipsOfPlayer1--;
        public static Models.Turn GetOponentsTurn()
        {
            return TurnOfThePlayer == Models.Turn.Player1 ? Models.Turn.Player2 : Models.Turn.Player1;
        }
        static GameState()
        {
            MoveNumber = 0;
            ButtonStates = new(GameLogic.GameInfo.QuantityOfTheButtons);
            for (ButtonPosition state = ButtonPosition.a1; state<=ButtonPosition.g1; state++)
            {
                ButtonStates.Add(state, ChipState.None);
            }
            TurnOfThePlayer = Turn.Player1;
            GamePeriod = PeriodOfTheGame.SetUp;
            CountQuantityOfPlayersChips();
        }
        public static void ResetGameState()
        {
            foreach (var state in ButtonStates)
            {
                ButtonStates[state.Key] = ChipState.None;
            }
            Models.GameState.GamePeriod = PeriodOfTheGame.SetUp;
            QuantityOfChipsOfPlayer1 = 0;
            QuantityOfChipsOfPlayer2 = 0;
            TurnOfThePlayer = Turn.Player1;
        }
        public static void GiveTheTurnToOponent()
        {
            SwitchTurn();
            ControlGamePeriod();
            IncrementMoveNumber();
        }
        private static void ControlGamePeriod()
        {
            if (GamePeriod == Models.PeriodOfTheGame.SetUp)
            {
                if (MoveNumber == GameLogic.GameInfo.TurnWhenPeriodIsChanged)
                {
                    GamePeriod = Models.PeriodOfTheGame.HuntingTime;
                }
            }

            else
            {
                if (QuantityOfChipsOfPlayer1 == GameLogic.GameInfo.LooserQuantityOfChips)
                {
                    TwoChipsIsLeftEvent?.Invoke(null, new GameStateEventArgs.TwoChipsIsLeftArgs(Turn.Player1));
                }
                else
                {
                    if (QuantityOfChipsOfPlayer2 == GameLogic.GameInfo.LooserQuantityOfChips)
                    {
                        TwoChipsIsLeftEvent?.Invoke(null, new GameStateEventArgs.TwoChipsIsLeftArgs(Turn.Player2));
                    }

                }
            }
        }
        private static void SwitchTurn()
        {
            if (TurnOfThePlayer == Turn.Player1)
            {
                TurnOfThePlayer = Turn.Player2;
            }
            else
            {
                TurnOfThePlayer = Turn.Player1;
            }
        }
        private static void CountQuantityOfPlayersChips()
        {
            foreach (var state in ButtonStates)
            {
                switch (state.Value)
                {
                    case ChipState.Player1:
                        QuantityOfChipsOfPlayer1++;
                        break;
                    case ChipState.Player2:
                        QuantityOfChipsOfPlayer2++;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
