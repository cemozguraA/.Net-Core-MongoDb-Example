using MongoDbSample.Helpers;
using MongoDbSample.Models;
using System;
using System.Collections.Generic;

namespace MongoDbSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new MongoDbHelper("TestDB");
            db.Insert("Users", new User
            {
                FirstName = "Cem Özgür",
                LastName = "Aydın",
                Languages = new List<string>() {
                     "c#","js","sql","xam"
                 },
                SchoolDetail = new School
                {
                    City = "Adana",
                    Name = "AKL",
                    Zipcode = "1360"
                }
            }).Wait();
            var datas = db.LoadDatas<User>("Users");
            foreach (var item in datas.Result)
            {
                Console.WriteLine(item.FirstName);
            }

        }
    }
}
