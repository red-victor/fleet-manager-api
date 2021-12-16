using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager
{
    public static class Utils
    {
        public static string[] GetTicketTypes()
        {
            var enumLength = Enum.GetNames(typeof(TicketType)).Length;
            string[] values = new string[enumLength];

            for (int i = 0; i < enumLength; i++)
            {
                values[i] = ((TicketType) i).ToString();
            }

            return values;
        }

        public static string[] GetStatusTypes()
        {
            var enumLength = Enum.GetNames(typeof(StatusType)).Length;
            string[] values = new string[enumLength];

            for (int i = 0; i < enumLength; i++)
            {
                values[i] = ((StatusType)i).ToString();
            }

            return values;
        }
    }
}
