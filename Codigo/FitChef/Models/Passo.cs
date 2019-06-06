using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitChef.Models
{
    public class Passo
    { 
        public Passo(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public Passo()
        {
            Id = 0;
            Descricao = "";
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public string toString()
        {
            return Id + Descricao;
        }
    }
}