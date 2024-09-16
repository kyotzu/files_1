using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите путь к папке, которую нужно очистить:");
        string directoryPath = Console.ReadLine();

        if (Directory.Exists(directoryPath))
        {
            TimeSpan maxAge = TimeSpan.FromMinutes(30);
            CleanDirectory(directoryPath, maxAge);
            Console.WriteLine("Очистка завершена.");
        }
        else
        {
            Console.WriteLine("Указанная папка не существует.");
        }
    }

    static void CleanDirectory(string path, TimeSpan maxAge)
    {
        try
        {
            var now = DateTime.Now;

            foreach (string filePath in Directory.GetFiles(path))
            {
                DateTime lastAccessTime = File.GetLastAccessTime(filePath);
                if (now - lastAccessTime > maxAge)
                {
                    Console.WriteLine($"Удаление файла: {filePath}");
                    File.Delete(filePath);
                }
            }

            foreach (string dirPath in Directory.GetDirectories(path))
            {
                DateTime lastAccessTime = Directory.GetLastAccessTime(dirPath);
                if (now - lastAccessTime > maxAge)
                {
                    Console.WriteLine($"Удаление папки: {dirPath}");
                    Directory.Delete(dirPath, true);
                }
                else
                {
                    CleanDirectory(dirPath, maxAge);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}

