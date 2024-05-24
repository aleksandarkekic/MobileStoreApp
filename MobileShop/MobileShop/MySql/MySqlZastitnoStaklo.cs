using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using MobileShop.Model;
using MySql.Data.MySqlClient;
//using MobileShop.Exceptions;
using System;
namespace MobileShop.MySql
{
   public class MySqlZastitnoStaklo
    {
        private static readonly string SELECT_ALL = "SELECT * FROM `staklo_info`";
        private static readonly string DELETE = "DELETE FROM `staklo` WHERE ZASTITA_ZA_EKRAN_TELEFONSKA_OPREMA_Artikal_idArtikal=@ZASTITA_ZA_EKRAN_TELEFONSKA_OPREMA_Artikal_idArtikal";
        private static readonly string SELECT_BY_MODEL = "SELECT brojUredjajaNaStanju FROM `staklo` WHERE model=@model";
        private static readonly string UPDATE = "UPDATE `staklo` SET brojUredjajaNaStanju=@brojUredjajaNaStanju WHERE model=@model";
        private static readonly string SELECT_ID_BY_MODEL = "SELECT ZASTITA_ZA_EKRAN_TELEFONSKA_OPREMA_Artikal_idArtikal FROM `staklo` WHERE model=@model";
        private static readonly string INSERT_ARTIKAL = "INSERT INTO `artikal`(cijena) VALUES (@cijena)";
        private static readonly string INSERT_STAKLO = "INSERT INTO `staklo`(ZASTITA_ZA_EKRAN_TELEFONSKA_OPREMA_Artikal_idArtikal, model, brojUredjajaNaStanju) VALUES (@ZASTITA_ZA_EKRAN_TELEFONSKA_OPREMA_Artikal_idArtikal, @model, @brojUredjajaNaStanju)";
        private static readonly string INSERT_TELEFONSKA_OPREMA = "INSERT INTO `telefonska_oprema`(Artikal_idArtikal) VALUES (@Artikal_idArtikal)";
        private static readonly string INSERT_ZASTITA_ZA_EKRAN = "INSERT INTO `zastita_za_ekran`(TELEFONSKA_OPREMA_Artikal_idArtikal) VALUES (@TELEFONSKA_OPREMA_Artikal_idArtikal)";
        public static List<ZastitnoStakloDTO> getZastinaStakla()
        {
            List<ZastitnoStakloDTO> result = new List<ZastitnoStakloDTO>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT_ALL;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new ZastitnoStakloDTO()
                    {
                        cijena = (short)reader.GetValue(0),
                        model = reader.GetString(1),
                        brojUredjajaNaStanju= (short)reader.GetValue(3)


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
        public static int getIdZastinogStakla(string model)
        {
            int result = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT_ID_BY_MODEL;
                cmd.Parameters.AddWithValue("@model", model);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = reader.GetInt32(0);

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
        public static void deleteStaklo(string model,int id)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd1, cmd2, cmd3;
            MySqlDataReader reader = null;
            int brojUredjajaNaStanju = 0;
            try
            {
                //
                conn = MySqlUtil.GetConnection();
                cmd1 = conn.CreateCommand();
                cmd1.CommandText = SELECT_BY_MODEL;
                cmd1.Parameters.AddWithValue("@model", model);
                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    brojUredjajaNaStanju = reader.GetInt32(0);
                }
                MySqlUtil.CloseQuietly(conn);
                if (brojUredjajaNaStanju > 1)
                {
                    brojUredjajaNaStanju -= 1;
                    conn = MySqlUtil.GetConnection();
                    cmd2 = conn.CreateCommand();
                    cmd2.CommandText = UPDATE;
                    cmd2.Parameters.AddWithValue("@brojUredjajaNaStanju", brojUredjajaNaStanju);
                    cmd2.Parameters.AddWithValue("@model", model);
                    int rowsAffected = cmd2.ExecuteNonQuery();
                    MySqlUtil.CloseQuietly(conn);

                }
                else
                {
                    conn = MySqlUtil.GetConnection();
                    cmd3 = conn.CreateCommand();
                    cmd3.CommandText = DELETE;
                    cmd3.Parameters.AddWithValue("@ZASTITA_ZA_EKRAN_TELEFONSKA_OPREMA_Artikal_idArtikal", id);
                    cmd3.ExecuteNonQuery();
                }

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
        public static void insertStaklo(string model, short cijena)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd1, cmd2, cmd3, cmd4, cmd5, cmd6;
            MySqlDataReader reader = null;
            int brojUredjajaNaStanju = 0;
            try
            {
                //
                conn = MySqlUtil.GetConnection();
                cmd1 = conn.CreateCommand();
                cmd1.CommandText = SELECT_BY_MODEL;
                cmd1.Parameters.AddWithValue("@model", model);
                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    brojUredjajaNaStanju = reader.GetInt32(0);
                }
                MySqlUtil.CloseQuietly(conn);
                if (brojUredjajaNaStanju > 0)
                {
                    brojUredjajaNaStanju += 1;
                    conn = MySqlUtil.GetConnection();
                    cmd2 = conn.CreateCommand();
                    cmd2.CommandText = UPDATE;
                    cmd2.Parameters.AddWithValue("@brojUredjajaNaStanju", brojUredjajaNaStanju);
                    cmd2.Parameters.AddWithValue("@model", model);
                    int rowsAffected = cmd2.ExecuteNonQuery();
                    MySqlUtil.CloseQuietly(conn);

                }
                else
                {
                    conn = MySqlUtil.GetConnection();
                    cmd3 = conn.CreateCommand();
                    cmd3.CommandText = INSERT_ARTIKAL;
                    cmd3.Parameters.AddWithValue("@cijena", cijena);
                    cmd3.ExecuteNonQuery();
                    MySqlUtil.CloseQuietly(conn);
                    conn = MySqlUtil.GetConnection();
                    cmd5 = conn.CreateCommand();
                    int id = (int)cmd3.LastInsertedId;
                    cmd5.CommandText = INSERT_TELEFONSKA_OPREMA;
                    cmd5.Parameters.AddWithValue("@Artikal_idArtikal", id);
                    cmd5.ExecuteNonQuery();
                    MySqlUtil.CloseQuietly(conn);
                    conn = MySqlUtil.GetConnection();
                    cmd6 = conn.CreateCommand();
                    cmd6.CommandText = INSERT_ZASTITA_ZA_EKRAN;
                    cmd6.Parameters.AddWithValue("@TELEFONSKA_OPREMA_Artikal_idArtikal", id);
                    cmd6.ExecuteNonQuery();
                    MySqlUtil.CloseQuietly(conn);
                    conn = MySqlUtil.GetConnection();
                    cmd4 = conn.CreateCommand();
                    Console.WriteLine("Vraca se id: " + id);
                    int broj = 1;
                    cmd4.CommandText = INSERT_STAKLO;
                    cmd4.Parameters.AddWithValue("@ZASTITA_ZA_EKRAN_TELEFONSKA_OPREMA_Artikal_idArtikal", id);
                    cmd4.Parameters.AddWithValue("@model", model);
                    cmd4.Parameters.AddWithValue("@brojUredjajaNaStanju", broj);
                    cmd4.ExecuteNonQuery();


                }

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
