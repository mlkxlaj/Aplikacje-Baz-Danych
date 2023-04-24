using CsvHelper;
using System;
using System.Text.Json;

namespace Zadanie2
{
    

    internal class Program
    {
        
        static async Task Main(string[] args)
        {
            var path = @"C:\Users\mkowa\RiderProjects\cwiczenia2_jb-mlkxlaj\Data\dane.csv";
            var result = await File.ReadAllLinesAsync(path);
            var listOfStudents = new List<Student>();

            var i = 0;
            foreach (var student in result)
            {
                
                var splited = student.Split(",");
                if(splited.Length == 9) {
                    Console.Write(i + " ");
                    for (int j = 0; j < splited.Length; j++)
                    {
                        Console.Write(splited[j]+" ");
                    }
                    Console.WriteLine();
                    i++;
                }
                

            }
            var jsonString = JsonSerializer.Serialize(new { Author = "Mikolaj Kowaszewicz", CreateDate = DateTime.Now, Students = listOfStudents });
            //File.WriteAllText("", jsonString);
        }
    }
}