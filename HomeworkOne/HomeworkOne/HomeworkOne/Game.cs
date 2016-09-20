using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HomeworkOne
{
    class Game
    {
        //attributes 
        private int playerLife;
        private int score;
        private ZombieData zd;
        private int zombieTimer;
        private int letterIndex;
        private string currentZombie;
        private string currentPhrase;
        private string userInput;


        public Game()
        {
            //sets player's lives
            playerLife = 5;

            //if a phrase hasn't been loaded or a zombie hasn't been loaded it will 'complain'
            zd = new ZombieData();
            if(zd.LoadPhrases("phrases.txt") == false || zd.LoadZombies() == false)
            {
                Console.WriteLine("Complain.");
                return;
            }
            //sets all obvious data to zero or an empty string
            score = 0;
            zombieTimer = 0;
            currentPhrase = "";
            currentZombie = "";
            userInput = "";
        }
        //starts the game while the player has lives
        public void PlayGame()
        {
            while(playerLife  > 0)
            {
                //if there is no zombie or there is no current phrase it will reset the current phrase or zombie to a new random iteration
                if(currentZombie == "" || currentPhrase == "")
                {
                    currentPhrase = zd.RandomPhrase();
                    currentZombie = zd.RandomZombie();
                    Console.WriteLine("Current Phrase: " + currentPhrase);
                    Console.WriteLine(currentZombie);
                }
                //checks what the user is typing, makes sure what the user is typing is the right letter in the right position
                //of the current phrase and if it is the player will get a !, if not they will get a :( 
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    string letter = key.KeyChar.ToString().ToUpper();
                    if(currentPhrase.ElementAt(letterIndex).ToString().ToUpper() != letter)
                    {
                        Console.WriteLine(":(");
                    }
                    else
                    {
                        letterIndex++;
                        Console.WriteLine("!");
                    }
                }
                //wait
                System.Threading.Thread.Sleep(50);
                zombieTimer += 50;

                //makes sure that the letter index does not go past the current phrase's count
                if(letterIndex >= currentPhrase.Count())
                {
                    currentPhrase = "";
                    currentZombie = "";
                    letterIndex = 0;
                    score += 50;
                    zombieTimer = 0;
                }
                //waits for the given amount of time and once it hits that time the player will be hit by the zombie and a life will be deducted
                if (4000 - (zombieTimer + score) < 0)
                {
                    Console.WriteLine("You were hit!");
                    playerLife--;
                    Console.WriteLine("Current Lives: " + playerLife);
                    zombieTimer = 0;
                }
            }
            //final output
            Console.WriteLine("You are dead.");
            Console.WriteLine("Score: " + score);
        }
    }
}
