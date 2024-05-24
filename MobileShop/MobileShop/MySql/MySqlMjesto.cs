using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using MobileShop.Model;
using MySql.Data.MySqlClient;

namespace MobileShop.MySql
{
  public class MySqlMjesto
    {
        private static readonly string SELECT = "SELECT * FROM `mjesto` WHERE naziv=@naziv";
        private static readonly string INSERT = "INSERT INTO `mjesto`(posta, naziv) VALUES (@posta, @naziv)";
        private static readonly string SELECT_POSTA_BY_NAZIV = "SELECT posta FROM `mjesto` WHERE naziv=@naziv";


        public static List<Mjesto> GetMjesto(String naziv)
        {
            List<Mjesto> result = new List<Mjesto>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT;
                cmd.Parameters.AddWithValue("@naziv", naziv);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Mjesto()
                    {
                        posta = (int)reader.GetValue(0),
                        naziv = reader.GetString(1)
                      


                    });
                }
            }
            catch (Exception ex)
            {
                //throw new DataAccessException("Exception in MySqlGroup", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }
        public static int getPostaByNaziv(string naziv)
        {
            int result = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT_POSTA_BY_NAZIV;
                cmd.Parameters.AddWithValue("@naziv", naziv);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = reader.GetInt32(0);

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
        public static void InsertMjesto(Mjesto mjesto)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@posta", mjesto.posta);
                cmd.Parameters.AddWithValue("@naziv", mjesto.naziv);

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

    }
}
