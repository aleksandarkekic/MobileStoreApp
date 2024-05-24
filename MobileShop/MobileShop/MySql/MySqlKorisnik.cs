using MobileShop.Model;
using static MobileShop.Hash.Hash;
using MySql.Data.MySqlClient;
//using MobileShop.Exceptions;
using System;
using System.Collections.Generic;

namespace MobileShop.MySql
{
    public class MySqlKorisnik
    {
        private static readonly string SELECT = "SELECT * FROM `korisnik` WHERE korisnickoIme = @korisnickoIme";
        private static readonly string SELECT_ALL = "SELECT * FROM `korisnik`";
        private static readonly string DELETE = "DELETE FROM `korisnik` WHERE idKorisnika = @idKorisnika";
        private static readonly string INSERT = "INSERT INTO `korisnik`(imeKorisnika, prezimeKorisnika, lozinkaKorisnika, korisnickoIme, ADRESA_KORISNIKA_idAdrese, uloga) VALUES (@imeKorisnika, @prezimeKorisnika, @lozinkaKorisnika, @korisnickoIme, @ADRESA_KORISNIKA_idAdrese, @uloga)";

        public static void InsertKorisnik(string imeKorisnika, string prezimeKorisnika, string lozinkaKorisnika, string korisnickoIme, int idAdresa, string uloga)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@imeKorisnika", imeKorisnika);
                cmd.Parameters.AddWithValue("@prezimeKorisnika", prezimeKorisnika);
                cmd.Parameters.AddWithValue("@lozinkaKorisnika", Hash_SHA1(lozinkaKorisnika));
                cmd.Parameters.AddWithValue("@korisnickoIme", korisnickoIme);
                cmd.Parameters.AddWithValue("@ADRESA_KORISNIKA_idAdrese", idAdresa);
                cmd.Parameters.AddWithValue("@uloga", uloga);
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
        public static List<Korisnik> GetKorisnik(String name)
        {
            List<Korisnik> result = new List<Korisnik>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT;
                cmd.Parameters.AddWithValue("@korisnickoIme", name);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Korisnik()
                    {
                        id = reader.GetInt32(0),
                        ime = reader.GetString(1),
                        prezime = reader.GetString(2),
                        sifra = reader.GetString(3),
                        korisnickoIme = reader.GetString(4),
                        idAdrese = reader.GetInt32(5),
                        uloga = reader.GetString(6)


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
        public static List<KorisnikDTO> GetAllUsers()
        {
            List<KorisnikDTO> result = new List<KorisnikDTO>();
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
                    result.Add(new KorisnikDTO()
                    {
                        id = reader.GetInt32(0),
                        ime = reader.GetString(1),
                        prezime = reader.GetString(2),
                        korisnickoIme = reader.GetString(4),
                        uloga = reader.GetString(6)


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
        public static void DeleteKorisnikById(int id)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@idKorisnika", id);
                cmd.ExecuteNonQuery();
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
