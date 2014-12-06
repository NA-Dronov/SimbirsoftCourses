using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DictionaryDAL
{
    public class Word
    {
        public string Vocab { get; set; }
        public string Baseform { get; set; }
        public string Phongl { get; set; }
        public string Grclassgl { get; set; }
        public string Stylgl { get; set; }
        public string Def { get; set; }
        public string Anti { get; set; }
        public string Leglexam { get; set; }
    }

    public class DictionaryWord
    {
        private SqlConnection sqlCn = null;

        public void OpenConnection(string connectionString)
        {
            sqlCn = new SqlConnection();
            sqlCn.ConnectionString = connectionString;
            sqlCn.Open();
        }

        public void CloseConnection()
        {
            sqlCn.Close();
        }

        public void InsertWord(string vocab, string baseform, string phongl, string grclassgl, string stylgl, string def, string anti, string leglexam)
        {
            string sql = string.Format("Insert Into dbo.Dict_OZHEGOV" +
                "(VOCAB, BASEFORM, PHONGL, GRCLASSGL, STYLGL, DEF, ANTI, LEGLEXAM) Values" +
                "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                vocab, baseform, phongl, grclassgl, stylgl, def, anti, leglexam);

            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertWord(Word word)
        {
            string sql = string.Format("Insert Into dbo.Dict_OZHEGOV" + 
                "(VOCAB, BASEFORM, PHONGL, GRCLASSGL, STYLGL, DEF, ANTI, LEGLEXAM) Values" +
                "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                word.Vocab, word.Baseform, word.Phongl, word.Grclassgl, word.Stylgl, word.Def, word.Anti, word.Leglexam);

            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteWord(string vocab)
        {
            string sql = string.Format("Delete from dbo.Dict_OZHEGOV where vocab = '{0}'", vocab);
            using(SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Exception error = new Exception("Error during delete operation. ", ex);
                    throw error;
                }
            }
        }

        public List<Word> GetAllWordsAsList()
        {
            List<Word> wrd = new List<Word>();

            string sql = "Select * From dbo.Dict_OZHEGOV";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    wrd.Add(new Word
                        {
                            Vocab = (string)dr["VOCAB"],
                            Baseform = (string)dr["BASEFORM"],
                            Phongl = (string)dr["PHONGL"],
                            Grclassgl = (string)dr["GRCLASSGL"],
                            Stylgl = (string)dr["STYLGL"],
                            Def = (string)dr["DEF"],
                            Anti = (string)dr["ANTI"],
                            Leglexam = (string)dr["LEGLEXAM"]
                        });
                }
                dr.Close();
            }
            return wrd;
        }

        public List<string> GetAllWordsAsStringList()
        {
            List<string> wrd = new List<string>();

            string sql = "Select [VOCAB] From dbo.Dict_OZHEGOV";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    wrd.Add((string)dr["VOCAB"]);
                }
                dr.Close();
            }
            return wrd;
        }

        public DataTable GetAllWordsAsDataTable()
        {
            DataTable wrd = new DataTable();

            string sql = "Select * From dbo.Dict_OZHEGOV";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();

                wrd.Load(dr);
                dr.Close();
            }
            return wrd;
        }
    }
}
