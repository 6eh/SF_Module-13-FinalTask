using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task_13_6_2
{
    class Program
    {
        /// <summary>
        /// Задание 13.6.2
        /// Программа подсчитывает какие 10 слов в тексте наиболее популярны
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

                Dictionary<string, int> dictionary = new();

                foreach (var word in words)
                {
                    if (word != string.Empty)
                    {
                        // Или занести новое слово в ключ или (если дубль) добавить +1 в его значение
                        if (dictionary.TryAdd(word, 0) == false)
                            dictionary[word]++;
                    }
                }

                Console.WriteLine($"Всего слов: {words.Length}");
                Console.WriteLine($"Уникальных слов: {dictionary.Count}");
                Console.WriteLine("\n");
                Console.WriteLine("Топ 10 повторяющихся слов в тексте:");

                // Перебираем словарь с пересортировкой по значениям
                int i = 0;
                foreach (var w in dictionary.OrderByDescending(p => p.Value))
                {
                    Console.WriteLine($" {i+1}. {w.Key} ({w.Value} повт.)");
                    i++;
                    if (i == 10) break;
                }
            }

            Console.ReadKey();
        }

    }
}

