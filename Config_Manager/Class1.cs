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
                Console.WriteLine("Please write your next settings name!(empty if this is the end, # for comments)");
                input = Console.ReadLine().Replace(" ", String.Empty);
                if (input == "0" || input == "")
                    break;
                else if (settings.ContainsKey(input))
                    Console.WriteLine("Settings with this name already exists!!");
                else if (input.StartsWith("#")) {
                    Console.WriteLine("Please write your comment");
                    settings.Add($"#comment{comment++}", Console.ReadLine().Replace(" ", String.Empty));
                }
                else {
                    Console.WriteLine("Please write it's value!");
                    settings.Add(input, Console.ReadLine().Replace(" ", String.Empty));
                }
            }
            Console.WriteLine("This is how the config looks:");
            foreach (var item in settings) {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            Console.WriteLine("Which name should i save it?:");
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
