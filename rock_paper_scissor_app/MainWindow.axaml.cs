using Avalonia.Controls;
using Avalonia.Interactivity;

namespace MyNewApp
{
    public partial class MainWindow : Window
    {
        // game logic
        private readonly GameLogic _game = new();

        // score variables
        private int _playerScore;
        private int _computerScore;
        private int _tieScore;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void PlayRound(Move playerMove)
        {
            

            //  Computer move
            var computerMove = _game.GetComputerMove();
            ComputerChoiceText.Text = computerMove switch
            {
                Move.Rock => "🪨",
                Move.Paper => "📄",
                Move.Scissors => "✂️",
                _ => "?"
            };

            //  Determine outcome
            var outcome = _game.DetermineOutcome(playerMove, computerMove);

            switch (outcome)
            {
                case Outcome.PlayerWins:
                    _playerScore++;
                    ResultText.Text = "You win 🎉";
                    break;

                case Outcome.ComputerWins:
                    _computerScore++;
                    ResultText.Text = "Computer wins 🤖";
                    break;

                case Outcome.Tie:
                    _tieScore++;
                    ResultText.Text = "It's a tie 😐";
                    break;
            }

            // Update the scoreboard
            UpdateScoreboard();
        }

        private void UpdateScoreboard()
        {
            PlayerScoreText.Text = _playerScore.ToString();
            ComputerScoreText.Text = _computerScore.ToString();
            TieScoreText.Text = _tieScore.ToString();
        }

        // Button handlers
        private void OnRockClicked(object? sender, RoutedEventArgs e) => PlayRound(Move.Rock);
        private void OnPaperClicked(object? sender, RoutedEventArgs e) => PlayRound(Move.Paper);
        private void OnScissorsClicked(object? sender, RoutedEventArgs e) => PlayRound(Move.Scissors);
    }
}
