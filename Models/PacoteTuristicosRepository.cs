using System;
using MySqlConnector;
using Destino_Certo.Models;
using System.Collections.Generic;

namespace Destino_Certo.Models
{
    public class PacoteTuristicosRepository
    {
         private const string baseDados = "Database=Destino_Certo;Data Source=localhost;User Id=root;";

        public void Insert(PacotesTuristicos novoPacote)
        {
            //Abrindo Conexão com o banco de dados
            MySqlConnection conexao = new MySqlConnection(baseDados);
            conexao.Open();

            //Criando Scrip Comando Sql
            string sql = "INSERT INTO Pacotes_Turisticos(Nome, Destino, Origem, Atrativos, Saida, Retorno, Usuario) VALUES (@Nome, @Destino, @Origem, @Atrativos, @Saida, @Retorno, @Usuario)";

            //preparar um comando, passando sql + conexao com sgbd
            MySqlCommand comando = new MySqlCommand(sql, conexao);

            //tratando injection 
            comando.Parameters.AddWithValue("@Nome", novoPacote.Nome);
            comando.Parameters.AddWithValue("@Destino", novoPacote.Destino);
            comando.Parameters.AddWithValue("@Origem", novoPacote.Origem);
            comando.Parameters.AddWithValue("@Atrativos", novoPacote.Atrativos);
            comando.Parameters.AddWithValue("@Saida", novoPacote.Saida);
            comando.Parameters.AddWithValue("@Retorno", novoPacote.Retorno);
            comando.Parameters.AddWithValue("@Usuario", novoPacote.Usuario);
            

            //execultando coimando sql
            comando.ExecuteNonQuery();

            //fechando conexão
            conexao.Close();

        }

        public void Alterar (PacotesTuristicos novoPacote)
        {
            //abrir a conexão com o banco de dados
            MySqlConnection conexao = new MySqlConnection(baseDados);
            conexao.Open();

            //query em sql para alterar (update)
            string sql = "UPDATE Pacotes_Turisticos set Nome=@Nome, Destino=@Destino, Origem=@Origem, Atrativos=@Atrativos, Saida=@Saida, Retorno=@Retorno, Usuario=@Usuario WHERE IdPacote_Turistico=@Id";

            //preparar um comando, passando : sql + conexão com o sgbd
            MySqlCommand comando = new MySqlCommand(sql, conexao);

            //tramento devido ao sql injection
            comando.Parameters.AddWithValue("@Id",novoPacote.Id);
            comando.Parameters.AddWithValue("@Nome",novoPacote.Nome);
            comando.Parameters.AddWithValue("@Destino",novoPacote.Destino);
            comando.Parameters.AddWithValue("@Origem",novoPacote.Origem);
            comando.Parameters.AddWithValue("@Atrativos",novoPacote.Atrativos);
            comando.Parameters.AddWithValue("@Saida",novoPacote.Saida);
            comando.Parameters.AddWithValue("@Retorno",novoPacote.Retorno);
            comando.Parameters.AddWithValue("@Usuario",novoPacote.Usuario);

            //execultar o comando no banco de dados
            comando.ExecuteNonQuery();

            //fechar conexão banco de dados
            conexao.Close();
        }

        public void Excluir (PacotesTuristicos velhoPacote)
        {
            //abrir a conexao com o banco de dados
            MySqlConnection conexao = new MySqlConnection(baseDados);
            conexao.Open();

            //query em sql para excluir (delete)
            string sql = "DELETE FROM Pacotes_Turisticos WHERE IdPacote_Turistico=@Id";

            //PREPARAR UM COMANDO 
            MySqlCommand comando = new MySqlCommand(sql, conexao);

            //tratamento devido ao sql injection
            comando.Parameters.AddWithValue("@Id",velhoPacote.Id);

            //execultando comando
            comando.ExecuteNonQuery();

            //fexhando conexão com o banco de dados
            conexao.Close();
        }

        public List<PacotesTuristicos> Listar()
        {
            //abrir conexao com o banco de dados
            MySqlConnection conexao = new MySqlConnection(baseDados);
            conexao.Open();

            //query em sql para selecionar (select)
            string sql = "SELECT * FROM Pacotes_Turisticos ORDER BY Nome";

            //preparar o comando passando : sql + conexao banco de dados
            MySqlCommand comando = new MySqlCommand(sql, conexao);

            //executar comando no sgbd e retornar uma lista de dados
            MySqlDataReader reader = comando.ExecuteReader();

            //criando lista para receber os dados
            List<PacotesTuristicos> lista = new List<PacotesTuristicos>();

            //comando condição para percorrer todos os registros retornados no sgbd(obj.  Reader)
            while(reader.Read())
            {
                //instanciando classe
                PacotesTuristicos p = new PacotesTuristicos();

                p.Id = reader.GetInt32("IdPacote_Turistico");

                if(!reader.IsDBNull(reader.GetOrdinal("Nome")))
                p.Nome = reader.GetString("Nome");
                
                if(!reader.IsDBNull(reader.GetOrdinal("Destino")))
                p.Destino = reader.GetString("Destino");
                
                if(!reader.IsDBNull(reader.GetOrdinal("Origem")))
                p.Origem = reader.GetString("Origem");
                
                if(!reader.IsDBNull(reader.GetOrdinal("Atrativos")))
                p.Atrativos = reader.GetString("Atrativos");
                
                p.Saida = reader.GetDateTime("Saida");

                p.Retorno = reader.GetDateTime("Retorno");
                
                p.Usuario = reader.GetInt32("Usuario");

               //add na lista de pacotes
               lista.Add(p);

            }
            
               //fechando conexão com o sgbd
               conexao.Close();

               //retornamos a lista com todos os registros armazenados
               return lista;
                
        
        }
        
    }
}