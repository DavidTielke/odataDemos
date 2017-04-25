using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODataAttributeBasedRouting.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Person()
        {
            
        }

        public Person(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}