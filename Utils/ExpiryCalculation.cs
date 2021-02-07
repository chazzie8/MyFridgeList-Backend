using System;

namespace MyFridgeListWebapi.Utils
{
    public static class ExpiryCalculation
    {
        public static string GetStatus(DateTime expiryDate)
        {
            var currentDate = DateTime.Now;
            var daysLeft = (expiryDate.Date - currentDate.Date).TotalDays;
            if (daysLeft >= 4)
            {
                return "good";
            }

            if (daysLeft >= 0 && daysLeft < 4)
            {
                return "almostExpired";
            }

            if (daysLeft < 0)
            {
                return "expired";
            }

            return null;
        }
    }
}