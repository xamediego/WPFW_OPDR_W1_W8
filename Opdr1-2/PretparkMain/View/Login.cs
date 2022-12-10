using System;

namespace ConsoleApplication1.View
{
    public static class Login
    {
        public static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Login\n2. Register\n3. Verify\n4. Exit");
                string option = Console.ReadLine();
                if (option == "4") break;
                Options(option);
            }
        }

        private static void Options(string input)
        {
            if (input == "1") LoginEvent();
            if (input == "2") RegisterEvent();
            if (input == "3") VerifyEvent();
        }

        private static void LoginEvent()
        {
            LoginForm.MainForm();
        }

        private static void RegisterEvent()
        {
            RegisterForm.MainForm();
        }
        
        
        private static void VerifyEvent()
        {
            VerifyView.MainView();
        }
        
    }
}