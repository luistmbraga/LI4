using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitChef.Models
{
    public class Receita
    {

        public Receita(int id, string nome, int infNutricional)
        {
            Id = id;
            Nome = nome;
            InfNutricional = infNutricional;
        }

        public Receita()
        {
            Id = 0;
            Nome = "";
            InfNutricional = 0;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int InfNutricional { get; set; }

        public string toString()
        {
            return Id + Nome + InfNutricional;
        }
    }
}