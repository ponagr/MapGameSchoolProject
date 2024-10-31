using System.Formats.Asn1;
using System.Runtime.InteropServices;

public class Player : GameObject
{
    public int CurrentXp { get; set; }
    public int MaxXp { get; set; }
    public int Level { get; set; }
    public int Gold { get; set; }
    public Consumable HealingPot { get; set; }

    public double BonusAgility { get; set; }
    public double BonusHp { get; set; }
    public double BonusDamage { get; set; }
    public double BonusResistance { get; set; }
    public override double TotalHp
    {
        get { return BaseHp + BonusHp; }
    }
    public override double TotalDamage
    {
        get { return BaseDamage + BonusDamage; }
    }
    public override double TotalAgility
    {
        get { return BaseAgility + BonusAgility; }
    }
    public override double TotalResistance
    {
        get { return BaseResistance + BonusResistance; }
    }

    public Inventory Inventory { get; set; }

    public Item[] EquippedGear { get; set; } = new Item[6];

    public Player(string name)
    {
        Name = name;
        Level = 1;
        Gold = 0;
        CurrentXp = 0;
        MaxXp = 100;
        BaseHp = 100;
        CurrentHp = BaseHp;
        BaseDamage = 20;
        BaseResistance = 5;
        BaseAgility = 10;

        HealingPot = new Consumable();
        Inventory = new Inventory();
    }

    public void CountStats()
    {
        double bonusDamage = 0;
        double bonusHp = 0;
        double bonusAgility = 0;
        double bonusResistance = 0;
        for (int i = 0; i < EquippedGear.Length; i++)
        {
            if (EquippedGear[i] != null)
            {
                bonusDamage += EquippedGear[i].Damage;
                bonusHp += EquippedGear[i].Health;
                bonusAgility += EquippedGear[i].Agility;
                bonusResistance += EquippedGear[i].Resistance;
            }
        }
        BonusAgility = bonusAgility;
        BonusHp = bonusHp;
        BonusDamage = bonusDamage;
        BonusResistance = bonusResistance;
        CurrentHp = TotalHp; // För att man ska få maxhp när man uppgraderar armor
    }
    #region INVENTORY
    public void InventoryInfo(Player player) // NÄR VI TRYCKER C
    {
        while (true)
        {
            Console.Clear();
            UI(player);
            ShowStats();
            Console.WriteLine();
            HealingPot.ShowItem();
            Console.WriteLine();
            Inventory.ShowInventory();
            Console.WriteLine();
            ShowWornGear();
            Console.WriteLine("\nTryck 'E' för att hantera equipments");
            Console.WriteLine("Tryck 'C' för att gå tillbaka");
            var keyInput = Console.ReadKey(true);
            if (keyInput.Key == ConsoleKey.E)
            {
                InventoryMenu();
            }
            else if (keyInput.Key == ConsoleKey.C)
            {
                return;
            }
            Console.WriteLine();
        }

    }
    public void InventoryMenu()
    {
        Console.Clear();
        ShowWornGear();
        Console.WriteLine();
        Inventory.ShowEquipmentInventory();

        Console.WriteLine("Välj ett item för att interagera ([C] - tillbaka)");
        var input = Console.ReadKey(true);
        if (input.Key == ConsoleKey.C)
        {
            return;
        }
        string strInput = input.KeyChar.ToString();
        int i = int.Parse(strInput);
        CheckGearType(Inventory.inventory[i]);

    }
    #endregion
    #region GEAR
    public void CheckGearType(Item itemToEquip)
    {
        if (itemToEquip is TWeapon)
        {
            EquipGear(itemToEquip, EquippedGear[0], 0);
        }
        if (itemToEquip is TBreastPlate)
        {
            EquipGear(itemToEquip, EquippedGear[2], 2);
        }
        if (itemToEquip is TLegs)
        {
            EquipGear(itemToEquip, EquippedGear[4], 4);
        }
        if (itemToEquip is TBoots)
        {
            EquipGear(itemToEquip, EquippedGear[5], 5);
        }
        if (itemToEquip is TGloves)
        {
            EquipGear(itemToEquip, EquippedGear[3], 3);
        }
        if (itemToEquip is THelm)
        {
            EquipGear(itemToEquip, EquippedGear[3], 1);
        }
        CountStats();
        return;
    }
    public void EquipGear(Item itemToEquip, Item equippedItem, int equippedGearIndex)  //Anropas från EquipGear-Metoden
    {
        if (equippedItem == null)
        {
            EquippedGear[equippedGearIndex] = itemToEquip; // Om itemToEquip är TWeapon, så är equippedGearIndex = 0, lägg in itemToEquip i EquippedGear[0], osv
            Inventory.inventory.Remove(itemToEquip);    //Om vi equippar, ta bort från inventory så vi inte får dublett
            Console.WriteLine($"Du tog på dig {itemToEquip.ItemName}, {itemToEquip.ItemType}");
        }
        else if (equippedItem != null)
        {
            Item item; // Skapar en tom referens
            GearChoice(itemToEquip, equippedItem, out item); // item är en av de 2 första

            Inventory.inventory.Add(equippedItem);
            EquippedGear[equippedGearIndex] = item;
            Inventory.inventory.Remove(item);
        }
    }
    public void GearChoice(Item itemToEquip, Item equippedItem, out Item item)
    {
        item = equippedItem;
        CompareGearStats(itemToEquip.Health, equippedItem.Health, "Health");    //Skickar in en specifik stat att jämföra och skriva ut
        CompareGearStats(itemToEquip.Damage, equippedItem.Damage, "Damage");
        CompareGearStats(itemToEquip.Resistance, equippedItem.Resistance, "Resistance");
        CompareGearStats(itemToEquip.Agility, equippedItem.Agility, "Agility");

        Console.WriteLine("Vill du byta? J/N");
        var input = Console.ReadKey(true);
        if (input.Key == ConsoleKey.J)
        {
            item = itemToEquip;     //Om "J", så byter vi item från equippedItem till itemToEquip
            return;
        }
        else if (input.Key == ConsoleKey.N)
        {
            return;
        }

    }
    public void CompareGearStats(double itemToEquipStats, double equippedItemStats, string stat) // Räknar ut differensen och skriver ut text i grön eller rött beroende på + eller -
    {
        if (itemToEquipStats > equippedItemStats)
        {
            double diff = itemToEquipStats - equippedItemStats;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"+{diff} {stat}");
            Console.ResetColor();
        }
        else if (itemToEquipStats < equippedItemStats)
        {
            double diff = equippedItemStats - itemToEquipStats;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"-{diff} {stat}");
            Console.ResetColor();
        }
    }
    public void ShowWornGear()
    {
        Console.WriteLine("Worn Equipment:");
        if (EquippedGear[0] != null)
        {
            EquippedGear[0].ShowStats();
        }
        else
        {
            Console.WriteLine("Weapon: (Empty)");
        }
        if (EquippedGear[2] != null)
        {
            EquippedGear[2].ShowStats();
        }
        else
        {
            Console.WriteLine("Chest: (Empty)");
        }
        if (EquippedGear[1] != null)
        {
            EquippedGear[1].ShowStats();
        }
        else
        {
            Console.WriteLine("Helm: (Empty)");
        }
        if (EquippedGear[5] != null)
        {
            EquippedGear[5].ShowStats();
        }
        else
        {
            Console.WriteLine("Boots: (Empty)");
        }
        if (EquippedGear[3] != null)
        {
            EquippedGear[3].ShowStats();
        }
        else
        {
            Console.WriteLine("Gloves: (Empty)");
        }
        if (EquippedGear[4] != null)
        {
            EquippedGear[4].ShowStats();
        }
        else
        {
            Console.WriteLine("Legs: (Empty)");
        }

    }
    #endregion
    #region LOOT
    public void Loot(Item item)     //Lägg till Item till inventory
    {
        Console.WriteLine();
        if (Inventory.inventory.Count < 15)
        {
            Console.WriteLine($"{Name} har lootat {item.ItemName},{item.ItemType}");
            if (item is Consumable)
            {
                if (HealingPot.Ammount < HealingPot.MaxAmmount)
                {
                    HealingPot.Ammount += 1;
                }
                else
                {
                    Console.WriteLine("Du har det maximala antalet Healing Potions redan");
                }
            }
            else
            {
                Inventory.inventory.Add(item);
            }

        }
        else
        {
            Console.WriteLine("Inventory är full");
        }
    }
    #endregion
    #region HEAL
    public string Heal()
    {
        double healAmmount;
        if (HealingPot.Ammount > 0)
        {
            double missingHealth = TotalHp - CurrentHp;     //Räkna ut hur mycket hp spelaren saknar
            // Random random = new Random();
            healAmmount = HealingPot.Healing; //50 + random.Next(0, 20);
            if (healAmmount > missingHealth)    //Om Heal är mer än spelarens saknade hp, heala till fullt, så att currentHealth inte kan bli mer än TotalHp
            {
                healAmmount = missingHealth;
            }
            CurrentHp += healAmmount;
            HealingPot.Ammount--;
            return $"+{healAmmount}HP";
        }
        else
        {
            return " ";
        }
    }
    #endregion
    #region ATTACK
    public string Attack(Enemy enemy, out string critical)
    {
        Random rndCrit = new Random();
        double damageDone;
        int critChange = Convert.ToInt32(BaseAgility); // Om Agility är 10
        int crit = rndCrit.Next(0, 101); // 0 - 10
        double damage;
        bool attackCrit = false;

        if (crit <= critChange)
        {
            damage = TotalDamage * 1.8;
            attackCrit = true;
        }
        else
        {
            damage = TotalDamage;
        }

        Random rndDamage = new Random();
        Random rndDodge = new Random();
        int dodgeChange = Convert.ToInt32(BaseAgility);
        int dodge = rndDodge.Next(0, 101);
        if (dodge <= dodgeChange)
        {
            critical = "";
            return $"{enemy.Name} DODGED";
        }
        else if (attackCrit)
        {
            damageDone = damage + rndDamage.Next(0, 10) - enemy.TotalResistance;
            enemy.CurrentHp -= damageDone;
            critical = "CRITICAL";
            return $"{damageDone:F0} DMG -->";
        }
        else
        {
            damageDone = damage + rndDamage.Next(0, 10) - enemy.TotalResistance;
            enemy.CurrentHp -= damageDone;
            critical = "";
            return $"{damageDone:F0} DMG -->";
        }
    }
    #endregion
    #region XP OCH LEVELUP
    public void EnemyKilled(Enemy enemy)
    {



        CurrentXp += enemy.XpDrop;
        Console.SetCursorPosition(0, 8);
        // Console.WriteLine($"{enemy.Name} dog");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"+{enemy.XpDrop} XP        ");

        Console.SetCursorPosition(0, 9);
        Clear.Row(9);
        Console.WriteLine("             \n            \n              \n          "); // För att input-text ska försvinna
        Console.ResetColor();
        Textures.PrintDeadText();

        if (CurrentXp >= MaxXp)
        {
            int transferXpToNextLevel = CurrentXp - MaxXp;  //Räkna ut hur mycket xp som ska överföras till nästa level
            CurrentXp = transferXpToNextLevel;
            MaxXp += 50;    //Öka xp som krävs för att gå upp till nästa level
            LevelUp();
        }
    }
    public void LevelUp()
    {
        Level++;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{Name} reached level: {Level}");
        double BaseHpAdded = BaseHp * 0.2;
        double BaseDamageAdded = BaseDamage * 0.2;
        double BaseResistanceAdded = BaseResistance * 0.2;
        double BaseAgilityAdded = BaseAgility * 0.2;
        BaseHp = BaseHp + BaseHpAdded;
        BaseDamage = BaseDamage + BaseDamageAdded;
        BaseResistance = BaseResistance + BaseResistanceAdded;
        BaseAgility = BaseAgility + BaseAgilityAdded;
        CurrentHp = BaseHp;
        Console.WriteLine($"+{BaseHpAdded:F0} Hp");
        Console.WriteLine($"+{BaseDamageAdded:F0} Damage");
        Console.WriteLine($"+{BaseResistanceAdded:F0} Resistance");
        Console.WriteLine($"+{BaseAgilityAdded:F0} Agility");
        Console.ResetColor();
    }
    #endregion
    #region UI OCH STATS
    public void ShowHp()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{CurrentHp:F0}/{TotalHp:F0}({PercentHp:F0}%)");
        Console.ResetColor();
    }

    public void ShowStats()     //Visa spelarens stats
    {
        Console.WriteLine("\n\n");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Health: {TotalHp}");
        Console.WriteLine($"Damage: {TotalDamage}");
        Console.WriteLine($"Resistance: {TotalResistance}");
        Console.WriteLine($"Agility: {TotalAgility}");
        Console.ResetColor();
        Console.WriteLine();
    }

    public void ShowXp()
    {
        Console.Write($"Level: {Level} ({CurrentXp}/{MaxXp})XP");
    }

    public void UI(Player player)   //Skriv ut spelarens Hp, Guld och Xp
    {
        Console.WriteLine();
        int curretLine = Console.CursorTop;
        HealthBar.PrintPlayerHealthBar(player);
        ShowHp();
        Console.SetCursorPosition(29, curretLine);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Coins: {Gold}");
        Console.SetCursorPosition(50, curretLine);
        Console.ForegroundColor = ConsoleColor.Magenta;
        ShowXp();
        Console.ResetColor();
    }
    #endregion



}