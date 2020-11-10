using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCount
{
    public class Statistics
    {
        public String FileContent {get; private set;}
        public Dictionary<String, int> WordCount { get; private set; }

        public Statistics()
        {
            WordCount = new Dictionary<String, int>();
        }
        public void Load(String file)
        {
            FileContent = System.IO.File.ReadAllText(file);
        }

        public void CountWords()
        {
            String[] words = FileContent.Split();
            words = words.Where(s => s.Length > 0).ToArray();
            for(int i = 0; i < words.Length; i++)
            {
                while (words[i].Length > 0 && !Char.IsLetter(words[i][0]))
                {
                    words[i] = words[i].Remove(0, 1);
                }

                while (words[i].Length > 0 && !Char.IsLetter(words[i][^1]))
                {
                    words[i] = words[i].Remove(words[i].Length - 1, 1);
                }

                if (String.IsNullOrEmpty(words[i])) continue;
                words[i] = words[i].ToLower();

                if (WordCount.ContainsKey(words[i]))
                {
                WordCount[words[i]]++;
                }
                else
                {
                    WordCount.Add(words[i], 1);
                }
            }




        }

    }
}
