using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileShop.Model;
using MySql.Data.MySqlClient;

namespace MobileShop.MySql
{
  public class MySqlAdresa
    {
        private static readonly string INSERT = "INSERT INTO `adresa_korisnika`(brojUlice, nazivUlice, MJESTO_posta) VALUES (@brojUlice, @nazivUlice,@MJESTO_posta)";
        private static readonly string SELECT= "SELECT idAdrese FROM `adresa_korisnika` WHERE brojUlice = @brojUlice AND nazivUlice=@nazivUlice AND MJESTO_posta=@MJESTO_posta";

        public static void InsertAdresa(Adresa adresa)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@brojUlice", adresa.brojUlice);
                cmd.Parameters.AddWithValue("@nazivUlice", adresa.nazivUlice);
                cmd.Parameters.AddWithValue("@MJESTO_posta", adresa.posta);
                cmd.ExecuteNonQuery();
                // mjesto.GroupId = (int)cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }
        public static int getIdAdresa(string brojUlice, string nazivUlice, int posta)
        {
            int result = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT;
                cmd.Parameters.AddWithValue("@brojUlice", brojUlice);
                cmd.Parameters.AddWithValue("@nazivUlice", nazivUlice);
                cmd.Parameters.AddWithValue("@MJESTO_posta", posta);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = (int)reader.GetValue(0);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }
    }
}
