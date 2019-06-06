using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitChef.Models
{
    public class Explicacao
    {
        public Explicacao(string url, bool video, int id, string duvida, int idP)
        {
            Url = url;
            Video = video;
            Id = id;
            Duvida = duvida;
            IdP = idP;
        } 

        public Explicacao()
        {
            Url = "";
            Video = false;
            Id = 0;
            Duvida = "";
            IdP = 0;
        }

        public string Url { get; set; }
        public bool Video { get; set; }
        public int Id { get; set; }
        public string Duvida { get; set; }
        public int IdP { get; set; }

        public string toString()
        {
            return Url + Video + Id + Duvida + IdP;
        }
    }
}