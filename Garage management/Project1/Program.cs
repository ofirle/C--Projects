using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    class Program
    {

        // $G$ SFN-999 (-3) Why after every opration do you ask to quit? there is the exit option ! bad UI 
        public static void Main()
        {
            UserInterface UI = new UserInterface();
            UI.PrintMenu();
            UI.InputAfterMenu();
        }
    }
}
