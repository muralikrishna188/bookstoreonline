using System;
using System.ComponentModel.DataAnnotations;

namespace catalog.Models  {
    public class Book  {
        public int ID  {get; set;}
        public String Name { get; set; }
        public int Pages { get; set; }
        public String Author { get; set; }
        public String ImageUrl  {get; set;}
        public double Price  {get; set;}
        public int InStock  {get; set;}
    }    
}