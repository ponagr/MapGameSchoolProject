using System.Drawing;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;


public static class GameLevel
{
    private static char Player = '@';
    private static char Enemy = '£';
    private static char Boss = 'B';
    private static char Coin = '$';
    private static char Wall = '|';
    private static char Terrain = '_';
    private static char Chest = '#';
    private static bool isOpen = false;
    private static char Trap = '¤';
    private static char Empty = ' ';
    private static char Door = '\\';
    private static char Door2 = '/';
    private static char Heart = '\u2665';
    private static char GoBack = '=';
    private static char Cellar = ')';
    private static char Merchant = 'M';

    public static int level;

    //public static List<Map> AllMaps = new List<Map>();



    //Metoder för alla olika utfall som kan ske på mappen, anropas via MovePlayer-metod
    //tar in nya positioner för att flytta spelaren och utför specifika händelser baserat på vilken metod som anropas från MovePlayer
    #region ENEMY
    private static void HandleEnemy(Player player, List<Enemy> enemies, char[,] gameMap, int newX, int newY)
    {
        Combat.FightMode(player, enemies[0]);
        if (enemies[0].CurrentHp <= 0)
        {
            enemies.RemoveAt(0);
            gameMap[newX, newY] = Empty;
        }
    }
    #endregion
    #region BOSS
    private static void HandleBoss(Player player, Enemy boss, char[,] gameMap, int newX, int newY)
    {
        Combat.FightMode(player, boss);
        if (boss.CurrentHp <= 0)
        {
            gameMap[newX, newY] = Empty;
        }
    }
    #endregion
    #region GOLD
    private static void HandleGold(Player player, char[,] gameMap, int posX, int posY, int newX, int newY)
    {
        Random random = new Random();
        player.Gold += random.Next(1, 6);
        Console.WriteLine($"+{random} guld");
        gameMap[newX, newY] = Player;
        gameMap[posX, posY] = Empty;
    }
    #endregion

    #region CHEST
    private static void HandleChest(List<Chest> chest, Player player, char[,] gameMap, int newX, int newY)
    {
        player.Loot(chest[0]);
        chest.RemoveAt(0);

        Console.ReadKey(true);
        Console.WriteLine("Du gick på en kista");
        gameMap[newX, newY] = Empty;
    }
    #endregion

    #region MERCHANT
    private static void HandleMerchant(Merchant merchant, Player player)
    {
        merchant.Interact(player);
    }
    #endregion

    #region HEART
    private static void HandleHeart(Player player, char[,] gameMap, int posX, int posY, int newX, int newY)
    {
        player.HealingPot.Ammount = 5;
        player.CurrentHp = player.TotalHp;
        gameMap[newX, newY] = Player;
        gameMap[posX, posY] = Empty;
    }
    #endregion

    #region TRAP
    private static void HandleTrap(Player player, char[,] gameMap, int posX, int posY, int newX, int newY)
    {
        player.CurrentHp -= 20;
        Console.WriteLine("Du trampade på en mina");
        gameMap[newX, newY] = Player;
        gameMap[posX, posY] = Empty;
    }
    #endregion

    #region NEXTLEVEL
    private static void NextLevel() //List<Map> maps
    {
        level++;
        //maps.Add(AllMaps[level]);
        Console.Clear();
        // Loada nästa level
        Console.WriteLine("Du klarade nivån");
        Textures.PrintLoading();
        //Texture för att visa att vi klarade leveln?
    }
    #endregion

    #region GOBACK
    private static void PreviousLevel()
    {
        level--;
        Console.Clear();
        // Loada nästa level
        Console.WriteLine("Du gick tillbaka en nivå");
        Textures.PrintLoading();
        //Texture för att visa att vi klarade leveln?
    }
    #endregion

    #region CELLAR
    private static void GoToCellar(Map map, Player player)
    {
        char[,] gameMap = map.CellarLevel;
        List<Enemy> enemies = map.Enemies;
        List<Chest> chests = map.Chests;
        Merchant? merchant = map.Merchant;
        bool inCellar = true;
        while (inCellar)
        {
            PrintGameBoard(gameMap, player);
            MovePlayer(gameMap, merchant, enemies, chests, player, out inCellar);
        }
    }

    #region MOVEMENT
    public static void MovePlayer(List<Map> map, Player player)  //Merchant ska läggas till i Map istället för att skickas in separat
    {
        int posX = 0;   //posX,posY är positionen som player har för tillfället
        int posY = 0;
        int newX;       //newX,newY är den nya positionen som vi vill förflytta våran player till
        int newY;
        Console.CursorVisible = false;

        Merchant? merchant = map[level].Merchant;    //Hämtar merchant, enemylista, chestlista, gameMap och boss via Map-objektet
        char[,] gameMap = map[level].Maplevel;
        List<Enemy> enemies = map[level].Enemies;
        Enemy boss = map[level].Boss;
        List<Chest> chests = map[level].Chests;

        var keyPressed = Console.ReadKey(true);

        for (int i = 0; i < gameMap.GetLength(0); i++)      //hitta positionen för player och ge dessa värden till posX och posY
        {
            for (int j = 0; j < gameMap.GetLength(1); j++)
            {
                if (gameMap[i, j] == Player)
                {
                    posX = i;
                    posY = j;
                }
            }
        }
        newX = posX;
        newY = posY;

        //Ger värde till newX och newY baserat på åt vilket håll vi väljer att gå, via WASD
        #region UP
        if (keyPressed.Key == ConsoleKey.W)
        {
            newX = posX - 1;
            newY = posY;
        }
        #endregion

        #region LEFT
        if (keyPressed.Key == ConsoleKey.A)
        {
            newX = posX;
            newY = posY - 1;
        }
        #endregion

        #region Down
        if (keyPressed.Key == ConsoleKey.S)
        {
            newX = posX + 1;
            newY = posY;
        }
        #endregion

        #region Right
        if (keyPressed.Key == ConsoleKey.D)
        {
            newX = posX;
            newY = posY + 1;
        }
        #endregion

        //Anropar metoder baserat på newX och newY positionerna
        #region MOVEMENTACTIONS
        if (gameMap[newX, newY] == Empty)
        {
            gameMap[newX, newY] = Player; // Byter plats
            gameMap[posX, posY] = Empty; // Där vi stod blir tom
        }
        else if (gameMap[newX, newY] == Enemy)
        {
            HandleEnemy(player, enemies, gameMap, newX, newY);
        }
        else if (gameMap[newX, newY] == Coin)
        {
            HandleGold(player, gameMap, posX, posY, newX, newY);
        }
        else if (gameMap[newX, newY] == Trap)
        {
            HandleTrap(player, gameMap, posX, posY, newX, newY);
        }
        else if (gameMap[newX, newY] == Chest)
        {
            HandleChest(chests, player, gameMap, newX, newY);
        }
        else if (gameMap[newX, newY] == Heart)
        {
            HandleHeart(player, gameMap, posX, posY, newX, newY);
        }
        else if (gameMap[newX, newY] == Boss)
        {
            HandleBoss(player, boss, gameMap, newX, newY);
        }
        else if (gameMap[newX, newY] == Merchant)
        {
            HandleMerchant(merchant, player);
        }
        else if (gameMap[newX, newY] == Door || gameMap[newX, newY] == Door2)
        {
            NextLevel();
        }
        else if (gameMap[newX, newY] == GoBack)
        {
            PreviousLevel();
        }
        else if (gameMap[newX, newY] == Cellar)
        {
            GoToCellar(map[level], player);
        }
        else if (gameMap[newX, newY] == Wall || gameMap[newX, newY] == Terrain)
        {
            Console.WriteLine("Du kan inte gå här");
        }
        else
        {
            Console.WriteLine("Du kan inte gå hit");
        }
        #endregion

        #region INVENTORY
        if (keyPressed.Key == ConsoleKey.C) //Visa playerStats
        {
            player.OpenInventory(player);
        }
        #endregion  

        #region HEAL
        if (keyPressed.Key == ConsoleKey.Q)
        {
            player.Heal();  //Använder en Health-Potion
        }
        #endregion
    }
    #endregion

    //Skriver ut info ovanför mappen
    private static void MapInfo()
    {
        Console.WriteLine();
        PrintColor.Green($" Player: {Player}  ", "Write");
        PrintColor.Red($"Enemy: {Enemy}  ", "Write");
        PrintColor.DarkYellow($"Chest: {Chest}  ", "Write");
        PrintColor.Yellow($"Coin: {'\u00A9'}  ", "Write");
        PrintColor.Gray($"Trap: {Trap}  ", "Write");
        PrintColor.Red($"Boss: {Boss}  ", "Write");
        PrintColor.Green($"Door: {Door}  ", "Write");
        Console.ResetColor();
        Console.WriteLine();
    }

    #region PRINTGAMEBOARD
    public static void PrintGameBoard(List<Map> map, Player player)  //Tar in och skriver ut den leveln som skickas in till metoden
    {
        Console.Clear();
        // INFO OM KARTAN
        MapInfo();
        Console.CursorVisible = false;
        char[,] gameMap = map[level].Maplevel;

        // SKRIVER UT MAP, med olika textfärger baserat på char
        for (int i = 0; i < gameMap.GetLength(0); i++)
        {
            for (int j = 0; j < gameMap.GetLength(1); j++)
            {
                if (gameMap[i, j] == Player)
                    PrintColor.Green($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == Enemy)
                    PrintColor.Red($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == Chest && !isOpen)
                    PrintColor.Yellow($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == Chest && isOpen) // ANVÄNDS INTE ÄN
                    PrintColor.Gray($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == Trap)
                    PrintColor.Gray($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == Boss)
                    PrintColor.Red($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == Coin)
                    PrintColor.DarkYellow($" {'\u00A9'} ", "Write");

                else if (gameMap[i, j] == Wall || gameMap[i, j] == Terrain)
                    PrintColor.BackgroundDarkCyan("   ", "Write");

                else if (gameMap[i, j] == Door || gameMap[i, j] == Door2)
                    PrintColor.DarkGreen($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == GoBack)
                    PrintColor.BackgroundGreen($" {gameMap[i, j]} ", "Write");
                else if (gameMap[i, j] == Cellar)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write($" {gameMap[i, j]} ");
                    Console.ResetColor();
                }
                else
                    Console.Write($" {gameMap[i, j]} ");
            }
            Console.WriteLine();
        }
        PlayerUI.UI(player);    //visa UI under mappen
    }
    #endregion
    #region CELLARLEVEL
    public static void PrintGameBoard(char[,] gameMap, Player player)  //Tar in och skriver ut den leveln som skickas in till metoden
    {
        Console.Clear();
        // INFO OM KARTAN
        MapInfo();
        Console.CursorVisible = false;

        // SKRIVER UT MAP, med olika textfärger baserat på char
        for (int i = 0; i < gameMap.GetLength(0); i++)
        {
            for (int j = 0; j < gameMap.GetLength(1); j++)
            {
                if (gameMap[i, j] == Player)
                    PrintColor.Green($"{gameMap[i, j]}  ", "Write");

                else if (gameMap[i, j] == Enemy)
                    PrintColor.Red($"{gameMap[i, j]}  ", "Write");

                else if (gameMap[i, j] == Chest && !isOpen)
                    PrintColor.Yellow($"{gameMap[i, j]}  ", "Write");

                else if (gameMap[i, j] == Chest && isOpen) // ANVÄNDS INTE ÄN
                    PrintColor.Gray($"{gameMap[i, j]}  ", "Write");

                else if (gameMap[i, j] == Trap)
                    PrintColor.Gray($"{gameMap[i, j]}  ", "Write");

                else if (gameMap[i, j] == Boss)
                    PrintColor.Red($"{gameMap[i, j]}  ", "Write");

                else if (gameMap[i, j] == Coin)
                    PrintColor.DarkYellow($"{gameMap[i, j]}  ", "Write");

                else if (gameMap[i, j] == Wall || gameMap[i, j] == Terrain)
                    PrintColor.BackgroundDarkCyan("   ", "Write");

                else if (gameMap[i, j] == Door || gameMap[i, j] == Door2)
                    PrintColor.DarkGreen($"{gameMap[i, j]}  ", "Write");

                else if (gameMap[i, j] == GoBack)
                    PrintColor.BackgroundGreen($"{gameMap[i, j]}  ", "Write");
                else if (gameMap[i, j] == Cellar)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{gameMap[i, j]}  ");
                    Console.ResetColor();
                }
                else
                    Console.Write(gameMap[i, j] + "  ");
            }
            Console.WriteLine();
        }
        PlayerUI.UI(player);    //visa UI under mappen
    }

    #region CELLARMOVEMENT
    public static void MovePlayer(char[,] gameMap, Merchant merchant, List<Enemy> enemies, List<Chest> chests, Player player, out bool inCellar)
    {
        int posX = 0;   //posX,posY är positionen som player har för tillfället
        int posY = 0;
        int newX;       //newX,newY är den nya positionen som vi vill förflytta våran player till
        int newY;
        inCellar = true;
        Console.CursorVisible = false;
        // char[,] gameMap = map[level].Maplevel;
        // List<Enemy> enemies = map[level].Enemies;
        // Enemy boss = map[level].Boss;
        // List<Chest> chests = map[level].Chests;

        var keyPressed = Console.ReadKey(true);

        for (int i = 0; i < gameMap.GetLength(0); i++)      //hitta positionen för player och ge dessa värden till posX och posY
        {
            for (int j = 0; j < gameMap.GetLength(1); j++)
            {
                if (gameMap[i, j] == Player)
                {
                    posX = i;
                    posY = j;
                }
            }
        }
        newX = posX;
        newY = posY;
        #endregion

        //Ger värde till newX och newY baserat på åt vilket håll vi väljer att gå, via WASD
        #region UP
        if (keyPressed.Key == ConsoleKey.W)
        {
            newX = posX - 1;
            newY = posY;
        }
        #endregion

        #region LEFT
        if (keyPressed.Key == ConsoleKey.A)
        {
            newX = posX;
            newY = posY - 1;
        }
        #endregion

        #region Down
        if (keyPressed.Key == ConsoleKey.S)
        {
            newX = posX + 1;
            newY = posY;
        }
        #endregion

        #region Right
        if (keyPressed.Key == ConsoleKey.D)
        {
            newX = posX;
            newY = posY + 1;
        }
        #endregion

        //Anropar metoder baserat på newX och newY positionerna
        #region MOVEMENTACTIONS
        if (gameMap[newX, newY] == Empty)
        {
            gameMap[newX, newY] = Player; // Byter plats
            gameMap[posX, posY] = Empty; // Där vi stod blir tom
        }
        else if (gameMap[newX, newY] == Enemy)
        {
            HandleEnemy(player, enemies, gameMap, newX, newY);
        }
        else if (gameMap[newX, newY] == Coin)
        {
            HandleGold(player, gameMap, posX, posY, newX, newY);
        }
        else if (gameMap[newX, newY] == Trap)
        {
            HandleTrap(player, gameMap, posX, posY, newX, newY);
        }
        else if (gameMap[newX, newY] == Chest)
        {
            HandleChest(chests, player, gameMap, newX, newY);
        }
        else if (gameMap[newX, newY] == Merchant)
        {
            HandleMerchant(merchant, player);
        }
        // else if (gameMap[newX, newY] == Door || gameMap[newX, newY] == Door2)
        // {
        //     level++;
        //     Console.Clear();
        //     // Loada nästa level
        //     Console.WriteLine("Du klarade nivån");
        //     Textures.PrintLoading();
        //     //Texture för att visa att vi klarade leveln?
        //     Thread.Sleep(2000);
        // }
        else if (gameMap[newX, newY] == Heart)
        {
            HandleHeart(player, gameMap, posX, posY, newX, newY);
        }
        // else if (gameMap[newX, newY] == Boss)
        // {
        //     HandleBoss(player, boss, gameMap, newX, newY);
        // }
        // else if (gameMap[newX, newY] == GoBack)
        // {
        //     level--;
        //     Console.Clear();
        //     // Loada nästa level
        //     Console.WriteLine("Du gick tillbaka en nivå");
        //     Textures.PrintLoading();
        //     //Texture för att visa att vi klarade leveln?
        //     Thread.Sleep(2000);
        // }
        else if (gameMap[newX, newY] == Cellar)
        {
            inCellar = false;
            return;
        }
        else if (gameMap[newX, newY] == Wall || gameMap[newX, newY] == Terrain)
        {
            Console.WriteLine("Du kan inte gå här");
        }
        else
        {
            Console.WriteLine("Du kan inte gå hit");
        }
        #endregion

        #region INVENTORY
        if (keyPressed.Key == ConsoleKey.C) //Visa playerStats
        {
            player.OpenInventory(player);
        }
        #endregion  

        #region HEAL
        if (keyPressed.Key == ConsoleKey.Q)
        {
            player.Heal();  //Använder en Health-Potion
        }
        #endregion
    }
    #endregion









    #region DARKLEVEL
    public static void PrintDarkLevel(List<Map> map, Player player)  //Tar in och skriver ut den leveln som skickas in till metoden
    {
        Console.Clear();
        char[,] gameMap = map[level].Maplevel;
        // INFO OM KARTAN
        MapInfo();
        Console.CursorVisible = false;
        // SKRIVER UT MAP, med olika textfärger baserat på char

        int posX = 0;   //posX,posY är positionen som player har för tillfället
        int posY = 0;
        // int newX;       //newX,newY är den nya positionen som vi vill förflytta våran player till
        // int newY;
        Console.WriteLine();
        for (int i = 0; i < gameMap.GetLength(0); i++)      //hitta positionen för player och ge dessa värden till posX och posY
        {
            for (int j = 0; j < gameMap.GetLength(1); j++)
            {
                if (gameMap[i, j] == Player)
                {
                    posX = i;
                    posY = j;
                }
            }
        }
        // newX = posX;
        // newY = posY;
        for (int i = posX -4; i < posX +4; i++)
        {
            if (i >= 0 && i < gameMap.GetLength(0))
            {
                for (int j = posY -4; j < posY +4; j++)
                {
                    if (j >= 0 && j < gameMap.GetLength(1))
                    {
                        if (gameMap[i, j] == Player)
                            PrintColor.Green($"{gameMap[i, j]}  ", "Write");

                        else if (gameMap[i, j] == Enemy)
                            PrintColor.Red($"{gameMap[i, j]}  ", "Write");

                        else if (gameMap[i, j] == Chest && !isOpen)
                            PrintColor.Yellow($"{gameMap[i, j]}  ", "Write");

                        else if (gameMap[i, j] == Chest && isOpen) // ANVÄNDS INTE ÄN
                            PrintColor.Gray($"{gameMap[i, j]}  ", "Write");

                        else if (gameMap[i, j] == Trap)
                            PrintColor.Gray($"{gameMap[i, j]}  ", "Write");

                        else if (gameMap[i, j] == Boss)
                            PrintColor.Red($"{gameMap[i, j]}  ", "Write");

                        else if (gameMap[i, j] == Coin)
                            PrintColor.DarkYellow($"{gameMap[i, j]}  ", "Write");

                        else if (gameMap[i, j] == Wall || gameMap[i, j] == Terrain)
                            PrintColor.BackgroundDarkGray("   ", "Write");

                        else if (gameMap[i, j] == Door || gameMap[i, j] == Door2)
                            PrintColor.DarkGreen($"{gameMap[i, j]}  ", "Write");

                        else if (gameMap[i, j] == GoBack)
                            PrintColor.BackgroundGreen($"{gameMap[i, j]}  ", "Write");
                        else if (gameMap[i, j] == Cellar)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write($"{gameMap[i, j]}  ");
                            Console.ResetColor();
                        }
                        else
                            Console.Write(gameMap[i, j] + "  ");
                    }
                
                }
                Console.WriteLine();
            }
            
        }

    }
    #endregion
}
#endregion