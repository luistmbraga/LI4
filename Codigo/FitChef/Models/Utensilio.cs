using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitChef.Models
{
    public class Utensilio
    {

        public Utensilio(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
        
        public Utensilio()
        {
            Id = 0;
            Nome = "";
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public string ToString()
        {
            return Id + Nome;
        }
    }
}