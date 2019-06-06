using MySql.Data.MySqlClient;

namespace FitChef.Data_Access
{
    public interface IConnection
    {
        // Fecha a conecção à BD
        void Close();
        // Abre a conecção à BD
        MySqlConnection Open();
        // Retorna à conecção, caso não esteja criada abre
        MySqlConnection Fetch();
    }
}