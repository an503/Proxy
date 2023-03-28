using System;

namespace ProxyPatternExample
{
    public interface IGameService
    {
        string GetWord();
        bool CheckGuess(string guess);
    }

    public class GameService : IGameService
    {
        private string word;

        public GameService()
        {
            // Инициализация слова по умолчанию
            word = "apple";
        }

        public string GetWord()
        {
            // Логика получения слова из внешнего источника
            return word;
        }

        public bool CheckGuess(string guess)
        {
            // Логика проверки угаданного слова
            return guess == word;
        }
    }

    public class GameServiceProxy : IGameService
    {
        private GameService gameService;
        private bool isAuthorized;

        public GameServiceProxy()
        {
            gameService = new GameService();
        }

        public string GetWord()
        {
            if (isAuthorized)
            {
                return gameService.GetWord();
            }
            else
            {
                throw new Exception("Unauthorized access");
            }
        }

        public bool CheckGuess(string guess)
        {
            if (isAuthorized)
            {
                return gameService.CheckGuess(guess);
            }
            else
            {
                throw new Exception("Unauthorized access");
            }
        }

        public void Authorize(string password)
        {
            // Логика проверки пароля
            isAuthorized = (password == "secret");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GameServiceProxy gameProxy = new GameServiceProxy();
            gameProxy.Authorize("secret");
            string word = gameProxy.GetWord();
            Console.WriteLine("Guess the word: {0}", word);
            string guess = Console.ReadLine();
            if (gameProxy.CheckGuess(guess))
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("You lose!");
            }
            Console.ReadLine();
        }
    }
}

