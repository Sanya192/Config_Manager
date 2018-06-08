using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Config_Helper {
    public class Config_Manager {
        public Dictionary<string, string> Settings { get; set; }
        public string Path { get; set; }
        public Config_Manager(string path) {
            path = path.Replace(" ", String.Empty);
            if (!File.Exists(path)) {
                Settings=CreateConfig();
                Console.WriteLine("Do you want to save it?");
                string answer = Console.ReadLine();
                if (answer.StartsWith("y")||answer.StartsWith("Y")||answer=="".Replace(" ",string.Empty)) {
                    SaveConfig(path);
                }
            }
            else {
                Settings = LoadConfig(path);
                Path = path;
            }
        }
        public Dictionary<string,string> CreateConfig() {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            string input;
            int comment = 0;
            while (true) {
                Console.WriteLine("Please write your next settings name!(empty if this is the end,start with # for comments)");
                input = Console.ReadLine().Replace(" ", String.Empty);
                if (input == "0" || input == "")
                    break;
                else if (settings.ContainsKey(input))
                    Console.WriteLine("Settings with this name already exists!!");
                else if (input.StartsWith("#")) {
                    //Console.WriteLine("Please write your comment");
                    settings.Add($"#comment{comment++}",input);
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
            return settings;
        }
        public Dictionary<string, string> LoadConfig(string path) {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            var input=File.ReadAllLines(path)
                .Select(p => p.Replace(" ", string.Empty))
                .Select(p => p.Split(':'));
            int comment = 0;
            foreach (var item in input) {
                if (item[0].StartsWith("#")) {
                    settings.Add($"#comment{comment++}", item[0]);
                }
                else {
                    settings.Add(item[0], item[1]);
                }
            }
            return settings;
        }
        public void SaveConfig(string path) {
            Console.WriteLine("Which name should i save it?:");
            //string path = Console.ReadLine().Replace(" ", String.Empty);
            using (var sw = new StreamWriter(path))
                foreach (var item in Settings) {
                    if (item.Key.StartsWith("#")) {
                        sw.WriteLine($"{item.Value}");
                    }
                    else
                    sw.WriteLine($"{item.Key}: {item.Value}");
                }
        }
        //usefull but not used
        public string SaveConfig() {
            Console.WriteLine("Which name should i save it?:");
            string path = Console.ReadLine().Replace(" ", String.Empty);
            using (var sw = new StreamWriter(path))
                foreach (var item in Settings) {
                    if (item.Key.StartsWith("#")) {
                        sw.WriteLine($"#{item.Value}");
                    }
                    sw.WriteLine($"{item.Key}: {item.Value}");
                }
            return path;
        }
    }
}
