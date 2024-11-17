using System;
using System.Collections.Generic;

class HashTableWithChains
{
    private const int TableSize = 10; // Размер хеш-таблицы
    private readonly List<List<int>> _table; // Таблица, основанная на списках (метод цепочек)

    public HashTableWithChains()
    {
        // Инициализация хеш-таблицы
        _table = new List<List<int>>();
        for (int i = 0; i < TableSize; i++)
        {
            _table.Add(new List<int>());
        }
    }

    // Хеш-функция для вычисления индекса
    private int HashFunction(int value)
    {
        return Math.Abs(value % TableSize);
    }

    // Метод добавления элемента в хеш-таблицу
    public void Insert(int value)
    {
        int index = HashFunction(value); // Вычисляем индекс
        _table[index].Add(value); // Добавляем значение в цепочку
    }

    // Метод поиска элемента в хеш-таблице
    public bool Search(int value)
    {
        int index = HashFunction(value); // Вычисляем индекс
        return _table[index].Contains(value); // Проверяем наличие в цепочке
    }

    // Метод вывода хеш-таблицы
    public void PrintTable()
    {
        Console.WriteLine("\nХеш-таблица:");
        for (int i = 0; i < _table.Count; i++)
        {
            Console.Write($"Индекс {i}: ");
            if (_table[i].Count == 0)
            {
                Console.WriteLine("пусто");
            }
            else
            {
                Console.WriteLine(string.Join(", ", _table[i]));
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        const int n = 8; // Количество элементов в массиве
        const int lowerBound = 53000; // Нижняя граница диапазона
        const int upperBound = 78000; // Верхняя граница диапазона

        var random = new Random();
        var array = new int[n];
        var hashTable = new HashTableWithChains();

        Console.WriteLine("Выберите способ ввода данных:");
        Console.WriteLine("1. Автоматический ввод");
        Console.WriteLine("2. Ручной ввод");
        Console.Write("Ваш выбор: ");
        int choice = int.TryParse(Console.ReadLine(), out int result) ? result : 1;

        // Ввод массива
        if (choice == 2)
        {
            Console.WriteLine($"Введите {n} чисел в диапазоне от {lowerBound} до {upperBound}:");
            for (int i = 0; i < n; i++)
            {
                while (true)
                {
                    Console.Write($"Число {i + 1}: ");
                    if (int.TryParse(Console.ReadLine(), out int input) && input >= lowerBound && input <= upperBound)
                    {
                        array[i] = input;
                        break;
                    }
                    Console.WriteLine("Ошибка ввода! Введите корректное число.");
                }
            }
        }
        else
        {
            Console.WriteLine("Автоматический ввод данных...");
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(lowerBound, upperBound + 1);
            }
        }

        // Заполнение хеш-таблицы
        foreach (var value in array)
        {
            hashTable.Insert(value);
        }

        // Вывод исходного массива
        Console.WriteLine("\nИсходный массив:");
        Console.WriteLine(string.Join(", ", array));

        // Вывод хеш-таблицы
        hashTable.PrintTable();

        // Поиск элемента
        Console.Write("\nВведите число для поиска: ");
        if (int.TryParse(Console.ReadLine(), out int searchValue))
        {
            if (hashTable.Search(searchValue))
            {
                Console.WriteLine($"Число {searchValue} найдено в хеш-таблице.");
            }
            else
            {
                Console.WriteLine($"Число {searchValue} не найдено в хеш-таблице.");
            }
        }
        else
        {
            Console.WriteLine("Ошибка ввода! Неверное значение.");
        }
    }
}
