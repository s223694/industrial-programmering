using System;

namespace MyNewApp
{
    public enum Move
    {
        Rock,
        Paper,
        Scissors
    }

    public enum Outcome
    {
        PlayerWins,
        ComputerWins,
        Tie
    }

    public class GameLogic
    {
        private readonly Random _rng = new();

        public Move GetComputerMove()
        {
            
            return (Move)_rng.Next(0, 3);
        }

        public Outcome DetermineOutcome(Move player, Move computer)
        {
            if (player == computer)
                return Outcome.Tie;

            return (player, computer) switch
            {
                (Move.Rock, Move.Scissors) => Outcome.PlayerWins,
                (Move.Paper, Move.Rock) => Outcome.PlayerWins,
                (Move.Scissors, Move.Paper) => Outcome.PlayerWins,
                _ => Outcome.ComputerWins
            };
        }
    }
}
