using RestaurantApp.Application.Logging;
using System;
using System.Text;

namespace RestaurantApp.API.ErrorLogging
{
    public class ConsoleErrorLogger : IErrorLogger
    {
        public void Log(AppError error)
        {
            var errorDate = DateTime.UtcNow;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Error Code: " + error.ErrorId.ToString());
            builder.AppendLine("Date: " + errorDate.ToLongDateString());
            builder.AppendLine("User: "+ error.Username);
            builder.AppendLine("Exception message: "+ error.Exception.Message);
            builder.AppendLine("Exception stack trace: ");
            builder.AppendLine(error.Exception.StackTrace);

            Console.WriteLine(builder.ToString()); 
        }
    }
}
