namespace ProductCatalog.PL.Helpers
{
    public static class LogError
    {
        public static void LogErrorInAFile(Exception e, string request)
        {
            string folderPath = @"C:\Logs";
            string logFilePath = Path.Combine(folderPath, "log.txt");

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine(DateTime.Now.ToString() + e.Message + $" In {request} Request");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
