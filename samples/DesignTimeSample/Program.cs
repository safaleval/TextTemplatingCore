using Newtonsoft.Json;
using System.Linq;
using System;

namespace DesignTimeSample
{
    public class Program
    {
        public static void Main()
        {
            var people = Enumerable.Range(0, 5).Select(num =>
                new Person
                {
                    Name = "Mr " + num,
                    Age = num + 20
                }
            );
            foreach (var p in people)
            {
                JsonConvert.SerializeObject(p);
            }
            Console.WriteLine("Done");
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
