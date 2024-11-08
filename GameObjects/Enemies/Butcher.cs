public class Butcher : Enemy
{

    // ASSASSIN - Ta mycket skada, vara osynlig, snabb rörlighet
    // KLUMPEN - Mycket skada, mycket hp, långsam
    // PILBÅGSSKYTT - Attackerar från avstånd, lite hp & resistance

    public bool hasShield;
    private int bigHit;
    private bool needsRest;
    private bool specialAttack;
    private bool justBlocked;

    public Butcher(int level, string name) : base(level)
    {
        double multiplier = level * 0.75;
        Random random = new Random();
        Name = name;
        Description = "Butcher";
        BaseHp = (100 + random.Next(0, 25)) * multiplier;
        CurrentHp = TotalHp;
        BaseDamage = (20 + random.Next(0, 10)) * multiplier;
        BaseResistance = (10 + random.Next(0, 5)) * multiplier;
        BaseAgility = 5 * multiplier;
        bigHit = 1;
        specialAttack = false;
        needsRest = false;
        healthBar = new HealthBar();

        hasShield = false;
    }
    public override void PrintCharacter(Enemy enemy)
    {
        // if (hasShield)
        // {
        //     Textures.PrintButcherShield();
        //     Thread.Sleep(400);
        //     Textures.PrintButcher();
        //     hasShield = false;
        // }
        // else
        // {
            Textures.PrintButcher();
        //}

    }
    public override void CharacterAttackAnimation(Enemy enemy)
    {
        if (bigHit == 1)
        {
            Textures.PrintButcherNeedsRest();
        }
        else if (specialAttack)
        {
            Textures.ButcherBigHitAnimation();
        }
        else
        {
            Textures.ButcherAttackAnimation();
        }
        
    }

    public override string Attack(Player player, out string attackMessage)
    {
        bigHit++;
        double damageDone = CalculateDamage(player, out bool attackCrit);
        if (bigHit == 3) // BIGHIT
        {
            damageDone = BigHit(damageDone);
            bigHit = 0;
            needsRest = true;
            specialAttack = true;
            player.CurrentHp -= damageDone;
            attackMessage = "MASSIVE HIT!";
            return $"<-- {damageDone:F0} DMG";
        }
        else if (needsRest) // Behöver vila
        {
            specialAttack = false;
            needsRest = false;
            attackMessage = "Butcher needs";
            return "to rest!";
        }
        else // Vanlig attack
        {
            specialAttack = false;
            return base.Attack(player, out attackMessage);
        }
    }

    public double BigHit(double damage)
    {
        damage = damage * 2;

        return damage;
    }

    public double BlockedAttack()
    {
        Textures.PrintButcherShield();
        Thread.Sleep(400);
        Textures.PrintButcher();
        double damage;
        damage = TotalDamage * 0.5;

        return damage;
    }



    public override string TakeDamage(double damage, bool crit, out string attackMessage)
    {
        Random rndShield = new Random();
        int magicNumber = rndShield.Next(1, 3);

        if (needsRest)
        {
            //justBlocked = false;
            return base.TakeDamage(damage, crit, out attackMessage);
        }

        hasShield = (magicNumber == 1) ? hasShield = true : hasShield = false;
        
        if (!hasShield)
        {
            return base.TakeDamage(damage, crit, out attackMessage);
        }
        else
        {
            damage = BlockedAttack();
            //justBlocked = true;
            CurrentHp -= damage;
            attackMessage = "Blocked!";
            return $"DMG {damage:F0} -->";
        }
    }
}