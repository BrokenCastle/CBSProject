using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Classes
{
    public class MyLocation
    {
        public string Name { get; set; }
        public string X { get; set; }
        public string Y { get; set; }

        //Constructor(Yapıcı) metot oluşturuyoruz...
        public MyLocation(string name, string x, string y)
        {
            Name = name;
            X = x;
            Y = y;
        }
    }
}