


namespace Chesso
{
    internal static class Program
    {
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
           // for(; ; ) //To make program annoying and virus, uncomment if you dare. Please do.
                Application.Run(new Form1());
        }
    }
}