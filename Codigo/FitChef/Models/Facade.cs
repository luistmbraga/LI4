using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using FitChef.Data_Access;

namespace FitChef.Models
{
    public class Facade
    {
        private ExplicacaoDAO Explicacoes;
        private IngredienteDAO Ingredientes;
        private PassoDAO Passos;
        private ReceitaDAO Receitas;
        private UtensilioDAO Utensilios;
        private UtilizadorDAO Utilizadores;

        public Facade()
        {

        }

        public bool UserExists(string username)
        {
            Connection connection = new Connection();
            Utilizadores = new UtilizadorDAO(connection);

            bool resultado = Utilizadores.VerificaUser(username);

            return resultado;
        }

        public int returnUtilizador(string username)
        {
            Connection connection = new Connection();
            Utilizadores = new UtilizadorDAO(connection);
            try
            {
                int id = Utilizadores.FindIDByUsername(username);
                return id;

            } catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Utilizador não existe!");
            }
        }

        public string Login(string username, string password)
        {
            Connection connection = new Connection();
            Utilizadores = new UtilizadorDAO(connection);
            try
            {
                string tipo = Utilizadores.Login(username, password);
                return tipo;
            }
            catch (InvalidOperationException e)
            {
                throw new System.InvalidOperationException("The password is incorrect! Please, try again.");
            }
        }

        public bool AddUtilizador(int id, string username, string password, string nome,
                        string email, bool tipo)
        {
            Utilizador u = new Utilizador(id, username, password, nome, email, tipo);
            Connection connection = new Connection();
            Utilizadores = new UtilizadorDAO(connection);
            if (Utilizadores.FindByUsername(username) == true) {
                throw new System.InvalidOperationException("Already exists a same username... Try other!");
            }
            return Utilizadores.Insert(u);
        }

        
        public bool SetUserInfo(int id, string username, string password)
        {
            Connection c = new Connection();
            Utilizadores = new UtilizadorDAO(c);
            // Encontra o utilizador através do seu username
            Utilizador updated = Utilizadores.FindById(id);
            if (username != null) {  
                if(Utilizadores.FindByUsername(username) == true)
                {
                    throw new InvalidOperationException("There already exists a user with the same username!"); 
                } else
                {
                    updated.Username = username;
                }
            } 
            if (password != null) updated.Password = password;
           
            return Utilizadores.Update(updated);
        }

        

        /*
        // Retorna verdadeira se não houver incongruências nos gostos selecionados pelo utilizador
        public bool SetPreferencesUtilizador(int id, Collection<Int32> gostos, Collection<Int32> naogostos)
        {
            IConnection c = new Connection();
            Utilizadores = new UtilizadorDAO(c);
        }
        */


        public bool CheckAmbiguidade(Collection<Int32> gostos, Collection<Int32> evitados)
        {
            foreach (int i in gostos)
            {
                foreach (int j in evitados)
                {
                    if (i == j) { return false; }
                }
            }
            return true;
        }

        public void SetPreferencesUtilizador(int id, Collection<Int32> gostos, Collection<Int32> naogostos)
        {
            Connection c = new Connection();
            Utilizadores = new UtilizadorDAO(c);
            Ingredientes = new IngredienteDAO(c);

            if (!CheckAmbiguidade(gostos, naogostos)) { throw new System.InvalidOperationException("You have incompatible preferences! Plese try again."); }

            Collection<Ingrediente> gostosI = new Collection<Ingrediente>();
            Collection<Ingrediente> naogostosI = new Collection<Ingrediente>();

            foreach (int i in gostos)
            {
                gostosI.Add(Ingredientes.FindById(i));
            }

            foreach (int j in naogostos)
            {
                naogostosI.Add(Ingredientes.FindById(j));
            }

            Utilizadores.AtualizaPreferencias(id, gostosI, naogostosI);
        }

        public Dictionary<int, String> GetAllReceitas()
        {
            Connection c = new Connection();
            Receitas = new ReceitaDAO(c);
            Collection<Receita> receitasSys = Receitas.FindAll();
            Dictionary<int, String> resultado = new Dictionary<int, string>();

            foreach (Receita r in receitasSys)
            {
                String value = r.Nome + " " + r.InfNutricional.ToString() + " " + "calories";
                resultado.Add(r.Id, value);
            }

            return resultado;
        }

        bool CompativeisIngredientes(Ingrediente i, Collection<Ingrediente> l)
        {
            foreach (Ingrediente ing in l)
            {
                if (ing.Id == i.Id) return false;
            }

            return true;
        }

        public Dictionary<int, string> GetReceitasPref(int idUtilizador)
        {
            Connection c = new Connection();
            Utilizadores = new UtilizadorDAO(c);
            Receitas = new ReceitaDAO(c);
            Ingredientes = new IngredienteDAO(c);
            Collection<Ingrediente> ingredientesEvitar = Ingredientes.getEvitadosFromUtilizador(idUtilizador);
            Collection<Receita> receitaSys = Receitas.FindAll();
            Collection<Receita> receitasGostos = new Collection<Receita>();
            Dictionary<int, string> resultado = new Dictionary<int, string>();

            foreach (Receita r in receitaSys)
            {
                Collection<Ingrediente> ingredientes = Receitas.FindIngredientesFromReceita(r.Id);
                bool adicionar = true;
                foreach (Ingrediente i in ingredientes)
                {
                    if (!CompativeisIngredientes(i, ingredientesEvitar)) { adicionar = false; break; }
                }
                if (adicionar) receitasGostos.Add(r);
            }

            if (receitasGostos.Count() == 0)
            {
                throw new System.InvalidOperationException("Your loved and hated ingredientes are not compatible with no one recipe in system!");
            }

            foreach (Receita r in receitasGostos)
            {
                string value = "";
                value += r.Nome;
                value += " " + r.InfNutricional.ToString() + " calories";
                resultado.Add(r.Id, value);
            }

            return resultado; 
        }


        public Dictionary<String, String> GetIngredientesEmenta(Collection<int> ementa)
        {
            Connection c = new Connection();
            Receitas = new ReceitaDAO(c);
            Ingredientes = new IngredienteDAO(c);
            Dictionary<String, String> resultado = new Dictionary<string, string>();
            Dictionary<int, int> resultadoAux = new Dictionary<int, int>();

            foreach (int idReceita in ementa)
            {
                Collection<Ingrediente> ingr = Receitas.FindIngredientesFromReceita(idReceita);
                foreach (Ingrediente i in ingr)
                {
                    if (!resultadoAux.ContainsKey(i.Id))
                    {
                        resultadoAux.Add(i.Id, i.Quantidade);
                    }
                    else
                    {
                        int quant = resultadoAux[i.Id] + i.Quantidade;   //resultadoAux.ElementAt(i.Id);
                        resultadoAux.Remove(i.Id);
                        resultadoAux.Add(i.Id, quant);
                    }
                }
            }

            foreach (int i in resultadoAux.Keys)
            {
                Ingrediente novo = Ingredientes.FindById(i);
                String key = novo.Nome;
                String value;
                if (novo.Unidade == "enough") { value = "just enough"; }
                else { value = resultadoAux[i].ToString() + " " + novo.Unidade; }
                resultado.Add(key, value);
            }

            return resultado;
        }

        public Dictionary<int, String> GetAllIngredientes()
        {
            Connection c = new Connection();
            Ingredientes = new IngredienteDAO(c);
            Dictionary<int, String> resultado = new Dictionary<int, String>();
            Collection<Ingrediente> aux = Ingredientes.FindAll();
            foreach (Ingrediente i in aux)
            {
                resultado.Add(i.Id, i.Nome);
            }

            return resultado;
        }

        public Dictionary<int, Collection<String>> getPreferencias(int idU)
        {
            Connection c = new Connection();
            Ingredientes = new IngredienteDAO(c);
            Collection<Ingrediente> ingre = Ingredientes.getGostosFromUtilizador(idU);
            Dictionary<int, Collection<String>> resultado = new Dictionary<int, Collection<string>>();

            foreach (Ingrediente i in ingre)
            {
                Collection<String> value = new Collection<string>();
                value.Add(i.Nome);
                value.Add(i.Unidade);
                resultado.Add(i.Id, value);
            }

            return resultado;
        }

        public Dictionary<int, Collection<String>> GetEmentaSemanal(int idU)
        {
            Connection c = new Connection();
            Utilizadores = new UtilizadorDAO(c);
            Receitas = new ReceitaDAO(c);
            Ingredientes = new IngredienteDAO(c);
            Collection<Ingrediente> ingredientesEvitar = Ingredientes.getEvitadosFromUtilizador(idU);
            Collection<Receita> receitaSys = Receitas.FindAll();
            Collection<Receita> receitasGostos = new Collection<Receita>();
            Dictionary<int, Collection<String>> resultado = new Dictionary<int, Collection<String>>();

            foreach (Receita r in receitaSys)
            {
                Collection<Ingrediente> ingredientes = Receitas.FindIngredientesFromReceita(r.Id);
                bool adicionar = true;
                foreach (Ingrediente i in ingredientes)
                {
                    if (!CompativeisIngredientes(i, ingredientesEvitar)) { adicionar = false; break; }
                }
                if (adicionar) receitasGostos.Add(r);
            }

            int falta = 0;

            if ((falta = receitasGostos.Count()) == 0)
                throw new System.InvalidOperationException("Change your personal preferences! There aren't any available recipes!");

            /*
            if (falta < 14)
            {
                int i = 14 - falta;
                for (int j = 0; j < i; j++)
                    receitasGostos.Add(receitasGostos.ElementAt(j));

            }*/

            foreach (Receita r in receitasGostos)
            {
                Collection<String> value = new Collection<String>();
                value.Add(r.Nome);
                value.Add(r.InfNutricional.ToString());
                resultado.Add(r.Id, value);
            }

            return resultado;
        }



        /**
        * VerificaReceita(String rec) 20/25
        * 
        * Verifica se uma receita existe dado um nome de receita.
        */
        public bool VerificaReceita(String receita)
        {
            Connection c = new Connection();
            Receitas = new ReceitaDAO(c);
            return Receitas.VerificaReceita(receita);
        }

        /**
         * AddReceita() 21/25
         * 
         * Adiciona uma receita ao sistema.
         * 
         * Retorna true ou false conforme inseriu ou não inseriu.
         */
        public int AddReceita(String nome, int infNutricional)
        {
            Connection c = new Connection();
            Receitas = new ReceitaDAO(c);
            //o id é sempre 0, porque não é necessário para o insert
            Receita rec = new Receita(0, nome, infNutricional);

            return Receitas.Insert(rec);
        }

        /**
        * AlterarReceita() 22/25
        * 
        * Alterar uma receita.
        * 
        * Retorna true ou false conforme alterou ou não.
        */
        public bool AlterarReceita(int id, String nome, int infNutricional)
        {
            Connection c = new Connection();
            Receitas = new ReceitaDAO(c);

            Receita rec = new Receita(id, nome, infNutricional);

            return Receitas.Update(rec);
        } 

        public Collection<String> GetUtensiliosFromReceita(int id)
        {
            Connection c = new Connection();
            Receitas = new ReceitaDAO(c);
            Collection<string> resultado = new Collection<string>();
            Collection <Utensilio> uts = Receitas.FindUtensiliosFromReceita(id);
            foreach(Utensilio u in uts)
            {
                resultado.Add(u.Nome);
            }
            return resultado;
        }

        public Dictionary<int, string> GetPassosFromReceita(int id)
        {
            Connection c = new Connection();
            Receitas = new ReceitaDAO(c);
            Dictionary<int, string> resultado = new Dictionary<int, string>();
            Collection<Passo> passos = Receitas.FindPassosFromReceita(id);
            foreach(Passo p in passos)
            {
                resultado.Add(p.Id ,p.Descricao);
            }
            return resultado;
        }

        /**
         * destivaUser() 23/25
         * 
         * Desativa um utilizador.
         */

        /**
         * getHistorico(int idCliente) 24/25
         * 
         * Retorna um historico com o nome da receita e os dados do historico.
         */
        public Dictionary<int, string> GetHistorico(int idCliente)
        {
            Connection c = new Connection();
            Utilizadores = new UtilizadorDAO(c);
            Collection<Historico> hist = Utilizadores.GetHistorico(idCliente);
            Dictionary<int,string> resultado = new Dictionary<int, string>();

            foreach (Historico h in hist)
            {
                string value = h.NomeReceita;
                value += " | " + h.Quantas + "times | " + h.TempoMedio;
                resultado.Add(h.IdReceita, value);
            }
            return resultado;
        }

        public Dictionary<int, Collection<string>> GetExpFromPasso(int id)
        {
            Connection c = new Connection();
            Passos = new PassoDAO(c);
            Dictionary<int, Collection<string>> resultado = new Dictionary<int, Collection<string>>();
            Collection<Explicacao> explicacoes = Passos.FindExplicacoesFromPasso(id);

            foreach(Explicacao ex in explicacoes)
            {
                Collection<string> value = new Collection<string>();
                value.Add(ex.Duvida);
                value.Add(ex.Url);
                resultado.Add(ex.Id, value);
            }

            return resultado;
        }

        public void ReceitaRealizada(int idUtilizador, int idReceita, long time, Collection<string> dificuldade)
        {
            Connection c = new Connection();
            Receitas = new ReceitaDAO(c);
            Receitas.InsertReceitaRealizada(idUtilizador, idReceita, time);
            Receitas.InsertDificuldades(idUtilizador, idReceita, dificuldade);
        }

        /**
         * GetDetalhes() 25/25
         * 
         * Retorna as dificuldades dado uma receita e um user.
         */
         public Collection<string> GetDificuldadesFromReceita(int idUtilizador, int idReceita)
        {
            Connection c = new Connection();
            Utilizadores = new UtilizadorDAO(c);
            Collection<string> res = Utilizadores.GetDificuldadesFromReceita(idUtilizador, idReceita);
            return res;
        }


        public int addIngrediente(int id, string nome, string unidade, int quantidade)
        {
            Ingrediente i = new Ingrediente(id, nome, unidade, quantidade);
            Connection connect = new Connection();
            Ingredientes = new IngredienteDAO(connect);
            if (Ingredientes.FindByName(nome) == true)
            {
                throw new InvalidOperationException("There already exists an ingredient with the same name...");
            }
            return Ingredientes.Insert(i);
        }

        public int addPasso(int id, string explic)
        {
            Passo p = new Passo(id, explic);
            Connection connect = new Connection();
            Passos = new PassoDAO(connect);
            return Passos.Insert(p);
        }

        public bool addExplicacao(int id, string url, bool video, string duvida, int id_passo)
        {
            Explicacao e = new Explicacao(url, video, id, duvida, id_passo);
            Connection con = new Connection();
            Explicacoes = new ExplicacaoDAO(con);
            return Explicacoes.Insert(e);
        }

        public int addUtensilio(int id, string utensilio)
        {
            Utensilio u = new Utensilio(id, utensilio);
            Connection con = new Connection();
            Utensilios = new UtensilioDAO(con);
            return Utensilios.Insert(u);
        }

        public bool addReceitaUtensilio(int id_r, int id_u)
        {
            Connection con = new Connection();
            Receitas = new ReceitaDAO(con);
            return Receitas.AddReceitaUtensilio(id_r, id_u);
        }

        public void insertReceitaPassoIngrediente(int id_rec, Dictionary<int, Dictionary<int, int>> map)
        {
            Connection con = new Connection();
            Receitas = new ReceitaDAO(con);
            int i = 0;

            foreach (int k in map.Keys)
            {
                foreach (int q in map[k].Keys)
                {
                    i++;
                    Receitas.InsertRIP(id_rec, k, q, map[k][q], i);
                }
            }
        }

        public Dictionary<int, string> getAllPassos()
        {
            Connection c = new Connection();
            Passos = new PassoDAO(c);
            Collection<Passo> passosSys = Passos.FindAll();
            Dictionary<int, string> res = new Dictionary<int, string>();

            foreach (Passo p in passosSys)
            {
                String value = p.Descricao;
                res.Add(p.Id, value);
            }
            return res;
        }

       
       
    }
}