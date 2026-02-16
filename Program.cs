using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1_SmartWatch
{
    class Program
    {
        static void Main(string[] args)
        {
            // Налаштування кодування
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            List<SmartWatch> watches = new List<SmartWatch>();

            // Тестові дані
            watches.Add(new SmartWatch { ModelName = "Apple Watch 8", Price = 400, BatteryLevel = 80, OsType = WatchOS.AppleWatchOS, IsWaterproof = true, ManufactureDate = new DateTime(2022, 10, 10) });
            watches.Add(new SmartWatch { ModelName = "Galaxy Watch 5", Price = 300, BatteryLevel = 45, OsType = WatchOS.AndroidWear, IsWaterproof = true, ManufactureDate = new DateTime(2022, 08, 15) });
            watches.Add(new SmartWatch { ModelName = "Xiaomi Band 7", Price = 50, BatteryLevel = 10, OsType = WatchOS.HarmonyOS, IsWaterproof = false, ManufactureDate = new DateTime(2023, 01, 20) });

            Console.WriteLine("Лабораторна робота №2");

            while (true)
            {
                // Меню
                Console.WriteLine("\n--- МЕНЮ ---");
                Console.WriteLine("1 – Додати об’єкт");
                Console.WriteLine("2 – Переглянути всі об’єкти");
                Console.WriteLine("3 – Знайти об’єкт");
                Console.WriteLine("4 – Продемонструвати поведінку");
                Console.WriteLine("5 – Видалити об’єкт");
                Console.WriteLine("0 – Вийти з програми");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        AddWatch(watches);
                        break;
                    case "2":
                        ShowAll(watches);
                        break;
                    case "3":
                        FindWatch(watches);
                        break;
                    case "4":
                        DemoBehavior(watches);
                        break;
                    case "5":
                        RemoveWatch(watches);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("[Помилка] Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }

        // --- Допоміжні методи ---

        static void PrintTable(List<SmartWatch> list)
        {
            Console.WriteLine(new string('-', 115));
            Console.WriteLine("| {0,-3} | {1,-20} | {2,-10} | {3,-8} | {4,-15} | {5,-6} | {6,-12} | {7,-10} |",
                "#", "Модель", "Ціна ($)", "Заряд", "ОС", "Вода?", "Дата", "Гарантія");
            Console.WriteLine(new string('-', 115));

            for (int i = 0; i < list.Count; i++)
            {
                var w = list[i];
                // Скорочення назви, якщо вона задовга
                string shortName = w.ModelName.Length > 20 ? w.ModelName.Substring(0, 17) + "..." : w.ModelName;

                Console.WriteLine("| {0,-3} | {1,-20} | {2,-10} | {3,-8} | {4,-15} | {5,-6} | {6,-12} | {7,-10} |",
                    i + 1, 
                    shortName, 
                    w.Price, 
                    w.BatteryLevel + "%", 
                    w.OsType, 
                    w.IsWaterproof ? "+" : "-", 
                    w.ManufactureDate.ToShortDateString(),
                    w.WarrantyStatus); 
            }
            Console.WriteLine(new string('-', 115));
        }

        static void ShowAll(List<SmartWatch> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("[Інфо] Список порожній.");
                return;
            }
            Console.WriteLine("\n--- Переглянути всі об’єкти ---");
            PrintTable(list);
        }

        // --- Основний функціонал ---

        // 1. Додавання
        static void AddWatch(List<SmartWatch> list)
        {
            SmartWatch sw = new SmartWatch();
            Console.WriteLine("\n--- Додати об’єкт ---");

            while (true)
            {
                try
                {
                    Console.Write("Введіть назву моделі: ");
                    sw.ModelName = Console.ReadLine()!;
                    break;
                }
                catch (Exception ex) { Console.WriteLine($"[Помилка] {ex.Message}"); }
            }
            while (true)
            {
                try
                {
                    Console.Write("Введіть ціну: ");
                    sw.Price = double.Parse(Console.ReadLine()!);
                    break;
                }
                catch (FormatException) { Console.WriteLine("[Помилка] Введіть коректне число."); }
                catch (Exception ex) { Console.WriteLine($"[Помилка] {ex.Message}"); }
            }
            while (true)
            {
                try
                {
                    Console.Write("Введіть рівень заряду (0-100): ");
                    sw.BatteryLevel = int.Parse(Console.ReadLine()!);
                    break;
                }
                catch (FormatException) { Console.WriteLine("[Помилка] Введіть ціле число."); }
                catch (Exception ex) { Console.WriteLine($"[Помилка] {ex.Message}"); }
            }
            while (true)
            {
                try
                {
                    Console.Write("Введіть дату (yyyy-mm-dd): ");
                    sw.ManufactureDate = DateTime.Parse(Console.ReadLine()!);
                    break;
                }
                catch (FormatException) { Console.WriteLine("[Помилка] Невірний формат дати."); }
                catch (Exception ex) { Console.WriteLine($"[Помилка] {ex.Message}"); }
            }
            while (true)
            {
                try
                {
                    Console.Write("Водостійкий? (true/false): ");
                    sw.IsWaterproof = bool.Parse(Console.ReadLine()!);
                    break;
                }
                catch { Console.WriteLine("[Помилка] Введіть 'true' або 'false'."); }
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Оберіть ОС (0-AndroidWear, 1-AppleWatchOS, 2-Tizen, 3-HarmonyOS, 4-GarminOS): ");
                    int osIdx = int.Parse(Console.ReadLine()!);
                    if (Enum.IsDefined(typeof(WatchOS), osIdx))
                    {
                        sw.OsType = (WatchOS)osIdx;
                        break;
                    }
                    else { Console.WriteLine("[Помилка] Невірний код ОС."); }
                }
                catch { Console.WriteLine("[Помилка] Введіть цифру."); }
            }
            list.Add(sw);
            Console.WriteLine("[Успіх] Об'єкт додано.");
        }

        // 3. Пошук
        static void FindWatch(List<SmartWatch> list)
        {
            if (list.Count == 0) { Console.WriteLine("[Інфо] Список порожній."); return; }

            Console.WriteLine("\n--- Знайти об’єкт ---");
            Console.WriteLine("1 - За назвою");
            Console.WriteLine("2 - За ціною");
            Console.WriteLine("3 - За зарядом");
            Console.WriteLine("4 - За ОС");
            Console.WriteLine("5 - За водостійкістю");
            Console.Write("Вибір: ");

            string choice = Console.ReadLine()!;
            List<SmartWatch> results = new List<SmartWatch>();

            try
            {
                switch (choice)
                {
                    case "1":
                        var names = list.Select(w => w.ModelName).Distinct().OrderBy(n => n);
                        Console.WriteLine($"[Доступні]: {string.Join(", ", names)}");
                        Console.Write("Введіть назву (або частину): ");
                        string q = Console.ReadLine()!.ToLower();
                        results = list.Where(w => w.ModelName.ToLower().Contains(q)).ToList();
                        break;

                    case "2":
                        var prices = list.Select(w => w.Price).Distinct().OrderBy(p => p);
                        Console.WriteLine($"[Доступні]: {string.Join(", ", prices)}");
                        Console.Write("Введіть ціну: ");
                        double p = double.Parse(Console.ReadLine()!);
                        results = list.Where(w => w.Price == p).ToList();
                        break;

                    case "3":
                        var levels = list.Select(w => w.BatteryLevel).Distinct().OrderBy(b => b);
                        Console.WriteLine($"[Доступні]: {string.Join(", ", levels)}");
                        Console.Write("Введіть рівень заряду: ");
                        int b = int.Parse(Console.ReadLine()!);
                        results = list.Where(w => w.BatteryLevel == b).ToList();
                        break;

                    case "4":
                        var osList = list.Select(w => w.OsType).Distinct().OrderBy(o => o);
                        Console.WriteLine($"[Доступні]: {string.Join(", ", osList)}");
                        Console.Write("Введіть номер ОС (0-AndroidWear, 1-AppleWatchOS...): ");
                        if (int.TryParse(Console.ReadLine(), out int osIdx) && Enum.IsDefined(typeof(WatchOS), osIdx))
                            results = list.Where(w => w.OsType == (WatchOS)osIdx).ToList();
                        else Console.WriteLine("[Помилка] Некоректна ОС.");
                        break;

                    case "5":
                        int wpCount = list.Count(w => w.IsWaterproof);
                        Console.WriteLine($"[Статистика] Водостійких: {wpCount}, Звичайних: {list.Count - wpCount}");
                        Console.Write("Шукати водостійкі? (1-Так, 0-Ні): ");
                        string wp = Console.ReadLine()!;
                        bool targetWp = (wp == "1");
                        results = list.Where(w => w.IsWaterproof == targetWp).ToList();
                        break;

                    default:
                        Console.WriteLine("[Помилка] Невірний вибір.");
                        return;
                }

                if (results.Count > 0)
                {
                    Console.WriteLine($"\nЗнайдено записів: {results.Count}");
                    PrintTable(results);
                }
                else Console.WriteLine("[Інфо] Нічого не знайдено.");
            }
            catch { Console.WriteLine("[Помилка] Некоректне введення даних."); }
        }

        // 4. Демонстрація поведінки
        static void DemoBehavior(List<SmartWatch> list)
        {
            if (list.Count == 0) { Console.WriteLine("[Інфо] Список порожній."); return; }
            Console.WriteLine("\n--- Продемонструвати поведінку ---");
            ShowAll(list);
            Console.Write("Введіть номер об'єкта для тесту: ");
            if (int.TryParse(Console.ReadLine()!, out int idx) && idx > 0 && idx <= list.Count)
            {
                SmartWatch selected = list[idx - 1];
                Console.WriteLine($"\n--- Тест об'єкта: {selected.ModelName} (ID: {selected.Id}) ---");
                Console.WriteLine($"Статус гарантії: {selected.WarrantyStatus}"); 
                selected.MakeCall("+380501234567");
                selected.InstallApp("Diya");
                selected.Charge(20);
            }
            else Console.WriteLine("[Помилка] Невірний номер.");
        }

        // 5. Видалення
        static void RemoveWatch(List<SmartWatch> list)
        {
            if (list.Count == 0) { Console.WriteLine("[Інфо] Список порожній."); return; }

            Console.WriteLine("\n--- Видалити об’єкт ---");
            Console.WriteLine("1 - За номером (один об'єкт)");
            Console.WriteLine("2 - За назвою");
            Console.WriteLine("3 - За ціною");
            Console.WriteLine("4 - За зарядом");
            Console.WriteLine("5 - За ОС");
            Console.Write("Вибір: ");

            string choice = Console.ReadLine()!;
            List<SmartWatch> toRemove = new List<SmartWatch>();

            try
            {
                switch (choice)
                {
                    case "1":
                        ShowAll(list);
                        Console.Write("Введіть номер для видалення: ");
                        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= list.Count)
                        {
                            var item = list[idx - 1];
                            list.RemoveAt(idx - 1);
                            Console.WriteLine($"[Успіх] Видалено: {item.ModelName}");
                            return;
                        }
                        Console.WriteLine("[Помилка] Невірний номер.");
                        return;

                    case "2":
                        var names = list.Select(w => w.ModelName).Distinct().OrderBy(n => n);
                        Console.WriteLine($"[Доступні]: {string.Join(", ", names)}");
                        Console.Write("Введіть назву для видалення: ");
                        string name = Console.ReadLine()!.ToLower();
                        toRemove = list.Where(w => w.ModelName.ToLower().Contains(name)).ToList();
                        break;

                    case "3":
                        var prices = list.Select(w => w.Price).Distinct().OrderBy(p => p);
                        Console.WriteLine($"[Доступні]: {string.Join(", ", prices)}");
                        Console.Write("Введіть ціну для видалення: ");
                        double price = double.Parse(Console.ReadLine()!);
                        toRemove = list.Where(w => w.Price == price).ToList();
                        break;

                    case "4":
                        var levels = list.Select(w => w.BatteryLevel).Distinct().OrderBy(b => b);
                        Console.WriteLine($"[Доступні]: {string.Join(", ", levels)}");
                        Console.Write("Введіть заряд для видалення: ");
                        int bat = int.Parse(Console.ReadLine()!);
                        toRemove = list.Where(w => w.BatteryLevel == bat).ToList();
                        break;

                    case "5":
                        var osList = list.Select(w => w.OsType).Distinct().OrderBy(o => o);
                        Console.WriteLine($"[Доступні]: {string.Join(", ", osList)}");
                        Console.Write("Введіть номер ОС: ");
                        int osIdx = int.Parse(Console.ReadLine()!);
                        if (Enum.IsDefined(typeof(WatchOS), osIdx))
                            toRemove = list.Where(w => w.OsType == (WatchOS)osIdx).ToList();
                        break;

                    default:
                        Console.WriteLine("[Помилка] Невірний вибір.");
                        return;
                }

                if (toRemove.Count > 0)
                {
                    Console.WriteLine($"\nЗнайдено для видалення: {toRemove.Count} шт.");
                    foreach (var item in toRemove)
                    {
                        Console.WriteLine($"[Видалення] {item.ModelName} ({item.Price}$)");
                        list.Remove(item);
                    }
                    Console.WriteLine("[Успіх] Операція завершена.");
                }
                else
                {
                    Console.WriteLine("[Інфо] Нічого не знайдено для видалення.");
                }
            }
            catch
            {
                Console.WriteLine("[Помилка] Некоректне введення даних.");
            }
        }
    }
}