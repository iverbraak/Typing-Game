using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace HomeworkOne
{
    class ZombieData
    {
        private List <string> zombies;
        private List <string> phrases;

        public ZombieData()
        {
            //empty lists
            zombies = new List<string>();
            phrases = new List<string>();

        }
        //reads all 10 phrases 
        public bool LoadPhrases(string fileName)
        {
            try
            {
                //adds phrase to the list
                StreamReader phrasesInput = new StreamReader(fileName);
                string line = null;
                while((line = phrasesInput.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    phrases.Add(line);
                }
                phrasesInput.Close();
                return true;
            }
            //catches the file not found exception
            catch(FileNotFoundException fnf)
            {
                Console.WriteLine(fnf.Message);
                return false;
            }
        }
        /// <summary>
        /// loads a zombie from one of the zombie text files
        /// </summary>
        /// <returns>zombie.txt plus it's index</returns>
        public bool LoadZombies()
        {
            //starts with zombie1.txt since there would be no zombie0.txt, and increases the index until it can't find another iteration of it
            string fileName = "zombie1.txt";
            StreamReader zombiesInput;
            int i = 2;
            try
            {
                while (File.Exists(fileName))
                {
                    zombiesInput = new StreamReader(fileName);
                    fileName = "zombie" + i + ".txt";
                    i++;
                    string zombie = "";
                    string zombieLine;
                    while((zombieLine = zombiesInput.ReadLine()) != null)
                    {
                        zombie += zombieLine + "\n";
                    }
                    Console.WriteLine(zombie);
                    zombies.Add(zombie);

                    zombiesInput.Close();
                }
                if(i > 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (IndexOutOfRangeException ior)
            {
                Console.WriteLine(ior.Message);
                return false;
            }
        }
        /// <summary>
        /// checks the list of phrases and picks a random phrase
        /// </summary>
        /// <returns>phrase</returns>
        public string RandomPhrase()
        {
            Random rand = new Random();
            int i = rand.Next(phrases.Count);
            return phrases[i];
        }
        /// <summary>
        /// checks the list of zombies and picks a random zombie
        /// </summary>
        /// <returns>zombie</returns>
        public string RandomZombie()
        {
            Random rand = new Random();
            int j = rand.Next(zombies.Count);
            return zombies[j];
        }
    }
}
