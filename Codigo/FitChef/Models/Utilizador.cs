using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitChef.Models
{
    public class Utilizador
    {
       public Utilizador(int id, string username, string password, string nome,
                        string email, bool tipo)
        {
            Id = id;
            Username = username;
            Password = password;
            Nome = nome;
            Email = email;
            Tipo = tipo;        // true -> cliente normal ; false -> ... (nutricionista)
        }
        
        public Utilizador()
        {
            Id = 0;
            Username = "";
            Password = "";
            Nome = "";
            Email = "";
            Tipo = true;
        }

        public int Id { get; set; } 
        public string Username { get; set; } 
        public string Password { get; set; } 
        public string Nome { get; set; } 
        public string Email { get; set; } 
        public bool Tipo { get; set; }

        public string ToString()
        {
            return Id + Username + Password + Nome + Email + Tipo;
        }
    }
}