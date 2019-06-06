using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitChef.Models
{
    public class Dificuldade
    {

        public Dificuldade(string comentario, int id, int idU, int idR)
        {
            Comentario = comentario;
            Id = id;
            IdU = idU;
            IdR = idR;
        }
        
        public Dificuldade()
        {
            Comentario = "";
            Id = 0;
            IdU = 0;
            IdR = 0;
        }

        public string Comentario { get; set; }
        public int Id { get; set; }
        public int IdU { get; set; }
        public int IdR { get; set; }

        public string toString()
        {
            return Comentario + Id + IdU + IdR;
        }
    }
}