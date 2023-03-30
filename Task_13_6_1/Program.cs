using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Task_13_6_1
{
    class Program
    {
        /// <summary>
        /// Задание 13.6.1
        /// Программа сравнивает производительность вставки в List<T> и LinkedList<T>
        /// </summary>
        static void Main(string[] args)
        {
            string textFilePath = @$"..{Path.DirectorySeparatorChar}.." +
                $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Text1.txt";

            using (StreamReader reader = new StreamReader(textFilePath))
            {
                // Удаляем пунктуацию
                var noPunctuationText = new string(reader.ReadToEnd().Where(c => !char.IsPunctuation(c)).ToArray());
                // Разбиваем текст на слова и кладем слова в массив
                string[] words = noPunctuationText.Split(' ');

                string collListName = "List<string>";
                string collLinkedListName = "LinkedList<string>";

                var tuple1 = AddToList(words);
                Print(collListName, tuple1.Item1, tuple1.Item2);

                Console.WriteLine();

                var tuple2 = AddToLinkedList(words);
                Print(collLinkedListName, tuple2.Item1, tuple2.Item2);

                Console.WriteLine();

                if (tuple1.Item2 < tuple2.Item2)
                    PrintResult(collListName, collLinkedListName, tuple1.Item2, tuple2.Item2);

                else if (tuple1.Item2 > tuple2.Item2)
                    PrintResult(collLinkedListName, collListName, tuple2.Item2, tuple1.Item2);

                else if (tuple1.Item2 == tuple2.Item2)
                    Console.WriteLine("Методы равны по скорости для такого кол-ва слов");
            }

            Console.ReadKey();
        }

        static Tuple<int, long> AddToList(string[] words)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<string> list = new();

            foreach (var w in words)
                list.Add(w);

            stopWatch.Stop();
            return Tuple.Create(list.Count, stopWatch.ElapsedMilliseconds);          
        }

        static Tuple<int, long> AddToLinkedList(string[] words)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            LinkedList<string> linkedList = new();

            foreach (var w in words)
                linkedList.AddFirst(w);

            stopWatch.Stop();
            return Tuple.Create(linkedList.Count, stopWatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// Вывод информации о заполнении коллекции
        /// </summary>
        static void Print(string collName, int wordsCount, long time)
        {
            Console.WriteLine($"{collName} заполнен\n" +
                    $" Кол-во слов: {wordsCount}\n" +
                    $" Время заполнения: {time} мс.");
        }

        /// <summary>
        /// Вывод финального результата
        /// </summary>
        static void PrintResult(string collName1, string collName2, long time1, long time2)
        {
            // Подсчет процентов
            double perc = ((double)time1 / (double)time2) * 100.0;
            // Вывод результата
            Console.WriteLine($"{collName1} оказался быстрее {collName2} на {time2 - time1} мс ({Convert.ToInt32(perc)} %)");
        }
    }
}

