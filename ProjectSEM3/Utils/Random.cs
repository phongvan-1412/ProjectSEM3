using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Utils
{
    public static class AppRandom
    {
        public static string String(int length = 6)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static List<T> Lists<T>(this List<T> list, int count)
        {
            var random = new Random();
            return list.OrderBy(arg => random.Next()).Take(count).ToList();
        }
    }
}