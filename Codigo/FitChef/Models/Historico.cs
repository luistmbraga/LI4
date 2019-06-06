using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitChef.Models
{
    public class Historico
    {
        public Historico(int id, string nome, int quantas, TimeSpan tempo)
        {
            IdReceita = id;
            NomeReceita = nome;
            Quantas = quantas;
            TempoMedio = tempo;
        }

        public Historico()
        {
            IdReceita = 0;
            NomeReceita = "";
            Quantas = 0;
            TempoMedio = new TimeSpan(0, 0, 0);
        }

        public int IdReceita { get; set; }
        public string NomeReceita { get; set; }
        public int Quantas { get; set; }
        public TimeSpan TempoMedio { get; set; }

        public string ToString()
        {
            return IdReceita + "," + NomeReceita + "," + Quantas + "," + TempoMedio;
        }
    }
}