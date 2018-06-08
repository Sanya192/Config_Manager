using System;
using System.Collections.Generic;
using System.IO;

namespace Config_Helper {
    public class Config_Manager {
        public Dictionary<string, string> Settings { get; set; }
        public string Path { get; set; }
        public Config_Manager(string path) {
            Path = path;
        }
        public void CreateConfig() {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            string input;
            int comment = 0;
            while (true) {
                Console.WriteLine("Írd be a következő beállítás nevét(Üres ha itt a vége,# ha komment)");
                input = Console.ReadLine().Replace(" ", String.Empty);
                if (input == "0" || input == "")
                    break;
                else if (settings.ContainsKey(input))
                    Console.WriteLine("Ilyen nevű beállítás már van!!");
                else if (input.StartsWith("#")) {
                    Console.WriteLine("Kérem a kommentet");
                    settings.Add($"#komment{comment++}", Console.ReadLine().Replace(" ", String.Empty));
                }
                else {
                    Console.WriteLine("Kérem az értékét!");
                    settings.Add(input, Console.ReadLine().Replace(" ", String.Empty));
                }
            }
            Console.WriteLine("Így néz ki a config kérem a file:");
            foreach (var item in settings) {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            Console.WriteLine("Hova mentsem el?:");
            using (var sw = new StreamWriter(Console.ReadLine().Replace(" ", String.Empty)))
                foreach (var item in settings) {
                    if (item.Key.StartsWith("#")) {
                        sw.WriteLine($"#{item.Value}");
                    }
                    sw.WriteLine($"{item.Key}: {item.Value}");
                }
        }
    }
}
