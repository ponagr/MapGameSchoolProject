﻿class Program
{
    static void Main(string[] args)
    {
        bool gameOver = false;
        Player player = new Player("Player");
        Enemy enemy = new Enemy(player);

        List<Map> maps = new List<Map>();
        maps.Add(AddMaps.Level1(player)); 
        maps.Add(AddMaps.Level2(player));
        //Items.ItemsToAdd();     //Lägger till items så att dessa existerar i spelet och kan lootas via kistor
        GameLevel.level = 0;
        while (!gameOver)
        {
            GameLevel.PrintGameBoard(maps, player);     //Skriver ut mappen
            GameLevel.MovePlayer(maps, player);  //Inväntar sedan input från användaren, flyttar sedan player baserat på input, 
            if (player.CurrentHp < 1)                                  //börjar sedan om loop och skriver ut mapp igen, om inte player.CurrentHp är 0, isåfall avslutas loop(GameOver)
            {
                Console.Clear();
                Console.WriteLine("Du dog"); // LÄGG IN EN ANIMATION
                Textures.PrintDeadText();
                gameOver = true;
            }
        }
    }
}