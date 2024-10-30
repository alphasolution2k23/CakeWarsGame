using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AliScripts
{
    public static class Utils 
    {
        public static string FormatInt(int amount)
        {
            if (amount >= 1000000000)
                return (amount / 1000000000.0).ToString("0.##B"); // Billions
            else if (amount >= 1000000)
                return (amount / 1000000.0).ToString("0.##M"); // Millions
            else if (amount >= 1000)
                return (amount / 1000.0).ToString("0.##k"); // Thousands
            else
                return amount.ToString();
        }

    }
}
