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
   public class MySqlTelefoni
    {
        //private static readonly string SELECT_ALL = "select * from telefon_sve_info where modelTelefona=@modelTelefona";
        private static readonly string SELECT_ALL = "select modelTelefona, brojUredjajaNaStanju FROM `mobilni_telefon`";
        private static readonly string SELECT_BY_MODEL = "SELECT * FROM `telefon_sve_info` WHERE modelTelefona = @modelTelefona";
        private static readonly string UPDATE = "UPDATE `mobilni_telefon` SET brojUredjajaNaStanju=@brojUredjajaNaStanju WHERE modelTelefona=@modelTelefona";
        private static readonly string DELETE = "DELETE FROM `mobilni_telefon` WHERE Artikal_idArtikal = @Artikal_idArtikal";
        private static readonly string SELECT_ID_BY_MODEL = "SELECT Artikal_idArtikal FROM `mobilni_telefon` WHERE modelTelefona=@modelTelefona";
        private static readonly string SELECT_BY_MODEL_BROJ_ELEMENATA = "SELECT brojUredjajaNaStanju FROM `mobilni_telefon` WHERE modelTelefona = @modelTelefona";
        private static readonly string INSERT_ARTIKAL = "INSERT INTO `artikal`(cijena) VALUES (@cijena)";
        private static readonly string INSERT_UREDJAJ = "INSERT INTO `uredjaj`(boja, MOBILNI_TELEFON_Artikal_idArtikal, internaMemorija, RAM_memorija) VALUES (@boja, @MOBILNI_TELEFON_Artikal_idArtikal, @internaMemorija, @RAM_memorija)";
        private static readonly string INSERT_TELEFON = "INSERT INTO `mobilni_telefon`(modelTelefona, dijagonalaEkrana, kapacitetBaterije, operativniSistem, tezina, objavljen, brojUredjajaNaStanju, procesor, Artikal_idArtikal, kamera) VALUES (@modelTelefona, @dijagonalaEkrana, @kapacitetBaterije, @operativniSistem, @tezina, @objavljen, @brojUredjajaNaStanju, @procesor, @Artikal_idArtikal, @kamera)";

        public static List<MobilniTelefonDTO> getModels()
        {
            List<MobilniTelefonDTO> result = new List<MobilniTelefonDTO>();
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
                    result.Add(
                       new MobilniTelefonDTO()
                       {
                           model = reader.GetString(0),
                           brojUredjajaNaStanju=(short)reader.GetValue(1)
                       }
                 );
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
        public static List<MobilniTelefon2DTO> getAllByModel(string modelTelefona)
        {
            List<MobilniTelefon2DTO> result = new List<MobilniTelefon2DTO>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT_BY_MODEL;
                cmd.Parameters.AddWithValue("@modelTelefona", modelTelefona);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(
                       new MobilniTelefon2DTO()
                       {
                           model = reader.GetString(2),
                           dijagonalaEkrana = reader.GetString(3),
                           kapacitetBaterije = reader.GetString(4),
                           operativniSistem = reader.GetString(5),
                           tezina = reader.GetInt32(6),
                           datumObjavljivanja = reader.GetString(7),
                           procesor = reader.GetString(9),
                           kamera = reader.GetString(11),
                           boja = reader.GetString(12),
                           internaMemorija = reader.GetString(14),
                           radnaMemorija = reader.GetString(15),
                           cijena = (short)reader.GetValue(1)

                       }
                 );

                 
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
        public static int getIdMobilnogTelefona(string model)
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
                cmd.Parameters.AddWithValue("@modelTelefona", model);
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
        public static void deleteTelefon(string model, int id)
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
                cmd1.CommandText = SELECT_BY_MODEL_BROJ_ELEMENATA;
                cmd1.Parameters.AddWithValue("@modelTelefona", model);
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
                    cmd2.Parameters.AddWithValue("@modelTelefona", model);
                    int rowsAffected = cmd2.ExecuteNonQuery();
                    MySqlUtil.CloseQuietly(conn);

                }
                else
                {
                    conn = MySqlUtil.GetConnection();
                    cmd3 = conn.CreateCommand();
                    cmd3.CommandText = DELETE;
                    cmd3.Parameters.AddWithValue("@Artikal_idArtikal", id);
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
        public static void insertTelefon(string modelTelefona, short cijena, string dijagonalaEkrana, string kapacitetBaterije, string operativniSistem,int tezina, DateTime objavljen, string procesor, string kamera,string boja, string internaMem, string ram)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd1, cmd2, cmd3, cmd4, cmd5;
            MySqlDataReader reader = null;
            int brojUredjajaNaStanju = 0;
            try
            {
                //
                conn = MySqlUtil.GetConnection();
                cmd1 = conn.CreateCommand();
                cmd1.CommandText = SELECT_BY_MODEL_BROJ_ELEMENATA;
                cmd1.Parameters.AddWithValue("@modelTelefona", modelTelefona);
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
                    cmd2.Parameters.AddWithValue("@modelTelefona", modelTelefona);
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
                    int id = (int)cmd3.LastInsertedId;
                    MySqlUtil.CloseQuietly(conn);
                    conn = MySqlUtil.GetConnection();
                    cmd5 = conn.CreateCommand();
                    int broj = 1;
                    cmd5.CommandText = INSERT_TELEFON;
                    cmd5.Parameters.AddWithValue("@modelTelefona", modelTelefona);
                    cmd5.Parameters.AddWithValue("@dijagonalaEkrana", dijagonalaEkrana);
                    cmd5.Parameters.AddWithValue("@kapacitetBaterije", kapacitetBaterije);
                    cmd5.Parameters.AddWithValue("@operativniSistem", operativniSistem);
                    cmd5.Parameters.AddWithValue("@tezina", tezina);
                    cmd5.Parameters.AddWithValue("@objavljen", objavljen);
                    cmd5.Parameters.AddWithValue("@brojUredjajaNaStanju", broj);
                    cmd5.Parameters.AddWithValue("@procesor", procesor);
                    cmd5.Parameters.AddWithValue("@Artikal_idArtikal", id);
                    cmd5.Parameters.AddWithValue("@kamera", kamera);
                    cmd5.ExecuteNonQuery();
                    MySqlUtil.CloseQuietly(conn);
                    conn = MySqlUtil.GetConnection();
                    cmd4 = conn.CreateCommand();
                    cmd4.CommandText = INSERT_UREDJAJ;
                    cmd4.Parameters.AddWithValue("@boja", boja);
                    cmd4.Parameters.AddWithValue("@MOBILNI_TELEFON_Artikal_idArtikal", id);
                    cmd4.Parameters.AddWithValue("@internaMemorija", internaMem);
                    cmd4.Parameters.AddWithValue("@RAM_memorija", ram);
                    cmd4.ExecuteNonQuery();
                    

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Data);
                Console.WriteLine(ex.Source);

            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

    }
}
