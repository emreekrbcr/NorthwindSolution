using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Deneme deneme2 = new Deneme();
            deneme2.Sayi = 5;
            deneme2.SetWord("ankara");

            var propertyInfo = typeof(Deneme).GetField("Word", BindingFlags.NonPublic | BindingFlags.Instance);

            Console.WriteLine(propertyInfo?.GetValue(deneme2));
        }
    }


    class Deneme
    {
        private string Word;
        internal int Sayi { get; set; }

        public void SetWord(string value)
        {
            Word = value;
        }
    }
}