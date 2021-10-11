using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keyBoardConsole
{
    class Program
    {
        static string wordsFile = "../../DATA/Words.txt";
        static string textFile = "../../DATA/Texts.txt";
        static void Main()
        {
            Console.Write("Выберите режим тренировки:\n"
                + "1 - тренировка с отдельными буквами\n"
                + "2 - тренировка со словами\n"
                + "3 - тренировка с текстом\n");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    logicRandomLetters();
                    
                    break;
                case 2:
                    Console.Clear();
                    logicRandomWords();
                    break;
                case 3:
                    Console.Clear();
                    logicText();
                    break;
            }
        }

        private static void logicRandomLetters()
        {
            int correctAnswersCount = 0;
            int wrongAnswersCount = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for ( ; ; )
            {
                
                int letterNum;
                Random random = new Random();
                letterNum = random.Next(97, 123);
                char letter = (char)letterNum;
                Console.WriteLine("Введите букву: " + letter);
                string input = Console.ReadLine();
                switch (input)
                {
                    case "STOP":
                        Console.WriteLine("Ваши результаты: \nПравильных букв: " + correctAnswersCount + "\nНеправильных букв: " + wrongAnswersCount 
                            + "\nПроцент правильных ответов: " + correctAnswersCount * 100 / (wrongAnswersCount + correctAnswersCount) + "%");
                        stopwatch.Stop();
                        Console.WriteLine("На ввод отдельной буквы затрачено " + stopwatch.ElapsedMilliseconds / (correctAnswersCount + wrongAnswersCount) + " миллисекунд");
                        Console.ReadLine();
                        return;
                    default: 
                        char c = input[0];
                        if (c != letter)
                        {
                            Console.WriteLine("MISTAKE!");
                            wrongAnswersCount++;
                            stopwatch.Stop();
                            System.Threading.Thread.Sleep(300);
                            stopwatch.Start();
                        } else
                        {
                            correctAnswersCount++;
                        }
                        Console.Clear();
                        break;
                }
            }

            
        }

        static bool firsttry = true;
        static int correctWords = 0;
        static int wrongWords = 0;
        static int wordsCount = 0;
        static int lettersCount = 0;
        private static void logicRandomWords()
        {
           
            string[] inputFromFile = File.ReadAllLines(wordsFile, Encoding.Default);
            Stopwatch stopwatch = new Stopwatch();
            for ( ; ; )
            {
                Random rnd = new Random();
                string word = inputFromFile[rnd.Next(inputFromFile.GetUpperBound(0))];
                
                stopwatch.Start();
                if (checkWord(word, stopwatch) == "STOP")
                {
                    break;
                }           
                Console.Clear();
            }

            Console.WriteLine("На ввод отдельного слова затрачено в среднем " + stopwatch.ElapsedMilliseconds / (wordsCount) + " миллисекунд " +
                "\nПри этом ввод отдельной буквы в среднем занимает " + stopwatch.ElapsedMilliseconds / lettersCount + " миллисекунд");
            Console.WriteLine("Ваши результаты: \nПравильных слов: " + correctWords + "\nНеправильных слов: " + wrongWords
                            + "\nПроцент правильных: " + correctWords * 100 / (wrongWords + correctWords) + "%");
            Console.ReadLine();
        }
        
        static string checkWord(string word, Stopwatch stopwatch) 
        {
            Console.WriteLine(firsttry ? "Введите слово: " + word : "Повторите попытку со словом " + word);
            string input = Console.ReadLine();
            lettersCount += input.Length;
            if (input == "STOP")
            {
                return "STOP";
            }

            if (word.Length != input.Length)
            {
                Console.WriteLine("Неправильно!");

                stopwatch.Stop();
                System.Threading.Thread.Sleep(300);
                stopwatch.Start();

                wrongWords++;
                firsttry = false;

                checkWord(word, stopwatch);
            }

            else if (word != input)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] != input[i])
                    {
                        Console.WriteLine("Ошибка в букве " + word[i] + "\n");
                    }
                }

                stopwatch.Stop();
                Console.WriteLine("Повторить попытку? (Yes/No)");
                string answer = Console.ReadLine();
                stopwatch.Start();
                
                switch (answer)
                {
                    case "Yes":
                        wrongWords++;
                        firsttry = false;
                        checkWord(word, stopwatch);
                        break;
                    case "No":
                        wrongWords++;
                        break;
                }
            }
            wordsCount++;
            correctWords++;
            return "continue";
        }

        private static void logicText()
        {
            string[] inputFromFile = File.ReadAllLines(textFile, Encoding.Default);
            for ( ; ; )
            {
                Random rnd = new Random();
                string text = inputFromFile[rnd.Next(inputFromFile.GetUpperBound(0))];
            } 
        }
    }
}
