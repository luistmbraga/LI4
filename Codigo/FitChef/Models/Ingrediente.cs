using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitChef.Models
{
    public class Ingrediente
    {
        public Ingrediente(int id,string nome,string unidade, int quantidade)
        {
            Id = id;
            Nome = nome;
            Unidade = unidade;
            Quantidade = quantidade;
        }
        
        public Ingrediente()
        {
            Id = 0;
            Nome = "";
            Unidade = "";
            Quantidade = 0;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Unidade { get; set; }
        public int Quantidade { get; set; }

        public string ToString()
        {
            return Id + Nome + Unidade + Quantidade;
        }
    }
}