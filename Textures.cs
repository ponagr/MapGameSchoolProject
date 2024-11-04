
public static class Textures
{
    #region Player
    public static void AttackPlayerAnimation()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(0, 2);
        Console.WriteLine("           .     ");
        Console.SetCursorPosition(0, 3);
        Console.WriteLine("     0  ~ /       ");
        Console.SetCursorPosition(0, 4);
        Console.WriteLine(" []/||--T         ");
        Console.SetCursorPosition(0, 5);
        Console.WriteLine("    /\\  	     ");
        Console.SetCursorPosition(0, 6);
        Console.WriteLine("   /  \\          ");
        Console.WriteLine();

        Thread.Sleep(300);

        Console.SetCursorPosition(0, 2);
        Console.WriteLine("                   ");
        Console.SetCursorPosition(0, 3);
        Console.WriteLine("      0            ");
        Console.SetCursorPosition(0, 4);
        Console.WriteLine("    /||--+--*      ");
        Console.SetCursorPosition(0, 5);
        Console.WriteLine("   []/\\  	     ");
        Console.SetCursorPosition(0, 6);
        Console.WriteLine("    /  \\          ");
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void PrintPlayerCharacter(int startLine, int linePosition)
    {
        List<string> textForRow = new List<string>();
        textForRow.Add("       .");
        textForRow.Add("    0  | ");
        textForRow.Add("[]-||--T");
        textForRow.Add("   /\\  	");
        textForRow.Add("  /  \\");
        Console.ForegroundColor = ConsoleColor.Green;
        Write.MultipleLines(textForRow, startLine, linePosition);
        Console.ResetColor();
        Console.WriteLine();
    }
#endregion
#region Enemy
    // ENEMY ANIMATIONS
    public static void PrintEnemyCharacter(Enemy enemy)
    {
        if (enemy is Assassin)
        {
            Assassin assassin = enemy as Assassin;
            if (assassin.isVisable == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(40, 2);
                Console.WriteLine(".        ");
                Console.SetCursorPosition(40, 3);
                Console.WriteLine("|  0    ");
                Console.SetCursorPosition(40, 4);
                Console.WriteLine("T--||-[E]  ");
                Console.SetCursorPosition(40, 5);
                Console.WriteLine("   /\\  	");
                Console.SetCursorPosition(40, 6);
                Console.WriteLine("  |  \\");
                Console.ResetColor();
                Console.WriteLine();
            }
            else // FIXA SENARE!
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(40, 2);
                Console.WriteLine(".        ");
                Console.SetCursorPosition(40, 3);
                Console.WriteLine("|  0    ");
                Console.SetCursorPosition(40, 4);
                Console.WriteLine("T--||-[E]  ");
                Console.SetCursorPosition(40, 5);
                Console.WriteLine("   /\\  	");
                Console.SetCursorPosition(40, 6);
                Console.WriteLine("  |  \\");
                Console.ResetColor();
                Console.WriteLine();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(40, 2);
            Console.WriteLine(".        ");
            Console.SetCursorPosition(40, 3);
            Console.WriteLine("|  0    ");
            Console.SetCursorPosition(40, 4);
            Console.WriteLine("T--||-[E]  ");
            Console.SetCursorPosition(40, 5);
            Console.WriteLine("   /\\  	");
            Console.SetCursorPosition(40, 6);
            Console.WriteLine("  |  \\");
            Console.ResetColor();
            Console.WriteLine();
        }
    }

    public static void AttackEnemyAnimation()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(40, 2);
        Console.WriteLine("        ");
        Console.SetCursorPosition(40, 3);
        Console.WriteLine(" \\~  0  ");
        Console.SetCursorPosition(40, 4);
        Console.WriteLine("  T--||\\[]  ");
        Console.SetCursorPosition(40, 5);
        Console.WriteLine("     /\\  ");
        Console.SetCursorPosition(40, 6);
        Console.WriteLine("    |  \\ ");
        Console.WriteLine();

        Thread.Sleep(300);

        Console.SetCursorPosition(40, 3);
        Console.WriteLine("      0  ");
        Console.SetCursorPosition(40, 4);
        Console.WriteLine("*--+--||\\    ");
        Console.SetCursorPosition(40, 5);
        Console.WriteLine("      /\\[]  ");
        Console.SetCursorPosition(40, 6);
        Console.WriteLine("     |  \\ ");
        Console.WriteLine();
        Console.ResetColor();
    }

    #endregion

#region Bow
    public static void PrintEnemyWithBow()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(40, 2);
        Console.WriteLine("        ");
        Console.SetCursorPosition(40, 3);
        Console.WriteLine("          O  ");
        Console.SetCursorPosition(40, 4);
        Console.WriteLine("     <--{-||  ");
        Console.SetCursorPosition(40, 5);
        Console.WriteLine("          /\\  ");
        Console.SetCursorPosition(40, 6);
        Console.WriteLine("        /  | ");
        Console.WriteLine();
    }

    public static void EnemyWithBowAnimation()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(40, 2);
        Console.WriteLine("        ");
        Console.SetCursorPosition(40, 3);
        Console.WriteLine("          O  ");
        Console.SetCursorPosition(40, 4);
        Console.WriteLine("   <--  {-||  ");
        Console.SetCursorPosition(40, 5);
        Console.WriteLine("          /\\  ");
        Console.SetCursorPosition(40, 6);
        Console.WriteLine("        /  | ");
        Console.WriteLine();

        Thread.Sleep(200);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(40, 2);
        Console.WriteLine("        ");
        Console.SetCursorPosition(40, 3);
        Console.WriteLine("          O  ");
        Console.SetCursorPosition(40, 4);
        Console.WriteLine("  <--   {-||  ");
        Console.SetCursorPosition(40, 5);
        Console.WriteLine("          /\\  ");
        Console.SetCursorPosition(40, 6);
        Console.WriteLine("        /  | ");
        Console.WriteLine();

        Thread.Sleep(200);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(40, 2);
        Console.WriteLine("        ");
        Console.SetCursorPosition(40, 3);
        Console.WriteLine("          O  ");
        Console.SetCursorPosition(40, 4);
        Console.WriteLine("<--    {-||  ");
        Console.SetCursorPosition(40, 5);
        Console.WriteLine("          /\\  ");
        Console.SetCursorPosition(40, 6);
        Console.WriteLine("        /  | ");
        Console.WriteLine();

    }
#endregion
#region DEAD
    public static void PrintDeadText() // Likadan fast grön eller liknande till vår gubbe?
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.SetCursorPosition(35, 2);
        Console.WriteLine("____  ____   _   ____  ");
        Console.SetCursorPosition(35, 3);
        Console.WriteLine("|| \\\\ ||    /\\\\  || \\\\ ");
        Console.SetCursorPosition(35, 4);
        Console.WriteLine("||  ||||-- //_\\\\ ||  ||");
        Console.SetCursorPosition(35, 5);
        Console.WriteLine("||_// ||__//   \\\\||_// ");

        Console.SetCursorPosition(35, 6);
        Console.WriteLine("                         "); // För att ta bort gammal text
        Console.SetCursorPosition(35, 7);
        Console.WriteLine("                         "); // --"--

        Thread.Sleep(100);
        Console.SetCursorPosition(35, 6);
        Console.WriteLine("        |          | ");

        Thread.Sleep(200);
        Console.SetCursorPosition(35, 6);
        Console.WriteLine("  |     |          | ");

        Thread.Sleep(250);
        Console.SetCursorPosition(35, 6);
        Console.WriteLine("  | |   | |        | ");

        Thread.Sleep(200);
        Console.SetCursorPosition(35, 7);
        Console.WriteLine("        |            ");

        Thread.Sleep(250);
        Console.SetCursorPosition(35, 7);
        Console.WriteLine("  |     |            ");

        Thread.Sleep(300);
        Console.SetCursorPosition(35, 7);
        Console.WriteLine("  |     |      |     ");

        Thread.Sleep(300);
        Console.SetCursorPosition(35, 8);
        Console.WriteLine("        |            ");
        Console.ResetColor();
    }
#endregion
}





























/*

POTION, HJÄLM, RUSTNING, VAPEN, XP

      _____
     /=====\
    ||     ||
    ||     ||  HJÄLM
     \\_ _// 
      /   \


       ____    POTION
       |  |		
       |  |		
      /----\
     / . ,  \
    (________)	


              ___  ____
             /   \/    \  DRESS
            /_/\     /\_\
               /_____\
              /_______\
             /_________\
            /___________\


        /\  10 RADER
       /  \
       |  |
       |  |
       |  |
       |  |
       <  >
   {}==|  |=={}
        []
        []
        ==


    /\
   {  } 
    ||
    ||  
    ||    Magestaff?
    ||
    ||
    || 
   {  }
    \/ 
     


    _       _
   / \     / \
  /   \___/   \
 (     ___     )  AXE
  \   /| |\   /
   \_/ | | \_/
       | |  
       | |
       |_|
       (_)
       (_)
       (_)



   2222     22222222
 22    22   22
22     22   22         2   2  2222
     22     22222222    2 2   2   2
   22              22    2    2222
22          22     22   2 2   2  
222222222    222222    2   2  2

()  ____  ()
 \\/    \//
  | >  < |
 (  xxxx  )
  \______/
   //  \\
  ()    ()
   
    xxxx
 xxxxxxxxxx
xx xxxxxx xx RUSTNING ::S:S:S
xx xxxxxx xx
   xxxxxx
   xxxxxx
   ------

        /\
       / /
      / /
     / / 
    / /
  ~/ /~
  /_/


        /\
       / /
      / /
     / / 
  _ / /_
 (_/ /_)
  /_/
  o
  
  _____       _____
 [xxxxx]     /=====\
[xxOxOxx]   ||     ||
[xxxxxxx]   ||     ||  HJÄLM
 \xxxxx/     \\_ _// 
              /   \

//        
 //       O 		    O 		  O	
 //      ||{}->        ||{}--> 	 ||{} --> 
 //      /\	           /\	     /\		ASSASSIN
 //     /  \	      /  \		/  \
 //
       O  | 		 O   /		O   /*_
      ||--T 		||--/	   ||--/	
      /\		    /\	       /\
     /  \	       /  |	      /  |


	  .		      
       0  | 		 
  [.]-||--T 		
      /\  		
     /  \	       

  \ o	
   ||\
   /\  
  | /

    	
  _ o
   ||_
  | \
  '  '

   \o/
   //
  | \
  '  '
 ____  ____   _   ____
 || \\ ||    /\\  || \\
 ||  ||||-- //_\\ ||  ||
 ||_// ||__//   \\||_//

 ____  ____   _   ____
 || \\ ||    /\\  || \\
 ||  ||||-- //_\\ ||  ||
 ||_// ||__// | \\||_//
   | |   | |      | | 
   |       |      |   
           |

    \  /
     \/
   DE/\D		
    /  \	

       .
      / \	 
      | |
      | |
      [ ]
       I
       o	
  
         
       . 
      / \  	 
      | | 
      | |
      [ ]
       o
       I
		

 //	          ,			
 //       0  |     \   Ö  
 //      ||--T      \--|| MAGE
 //      /\            /\
 //     L  \          /  \
 //
//    |   O   
 //   |--||-0 WARRIOR
 //      /\
 //     /  \
 //
 //   ;  00   
 //   |=-[]-=' ROBOT
 //      /\
 //     d  b


 //       O   
 //      ||-}-->
 //      /\
 //     |  \

  //      O   
 //      ||-}  -->
 //      /\
 //     |  \

   //     O   
 //      ||-}   -->
 //      /\
 //     /  \

 
   //     O   
 //      ||-}     -->
 //      /\
 //     /  \


    ____
    |  |		
    |  |		
   /----\
  / . ,  \
 (________)		
  

   _--~_
  (_____)
  |  $  |
   \___/
    

*/