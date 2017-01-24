using Finisar.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DatabaseProcess
{
    public class DbAccess : IDisposable
    {
        private SQLiteConnection conn;
        private SQLiteCommand command;
        private SQLiteTransaction trans;

        public void Dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }

        public DbAccess()
        {
            if (File.Exists(@"Kanzii.db"))
            {
                conn = new SQLiteConnection("Data Source=Kanzii.db;Version=3;UTF8Encoding=True;New=False;Compress=True;");
            }
            else
            {
                conn = new SQLiteConnection("Data Source=Kanzii.db;Version=3;UTF8Encoding=True;New=True;Compress=True;");
            }
        }

        public void OpenConnection()
        {
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
            {
                conn.Open();
                trans = conn.BeginTransaction();
            }
        }

        public void CloseCommitConnection()
        {
            if (conn.State != ConnectionState.Broken && conn.State != ConnectionState.Closed)
            {
                trans.Commit();
                trans.Dispose();
                command.Dispose();
                conn.Close();
            }
        }

        public void CloseRollbackConnection()
        {
            if (conn.State != ConnectionState.Broken && conn.State != ConnectionState.Closed)
            {
                trans.Rollback();
                trans.Dispose();
                command.Dispose();
                conn.Close();
            }
        }

        private void InitDb()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("CREATE TABLE kanzii (");
            queryString.Append("id INTEGER, mazii_id TEXT, word TEXT, lesson TEXT, vi_mean TEXT, ");
            queryString.Append("uvi_mean TEXT, cn_mean TEXT, ucn_mean TEXT, image TEXT, remember TEXT, ");
            queryString.Append("write TEXT, onjomi TEXT, ronjomi TEXT, kunjomi TEXT, rkunjomi TEXT, numstroke TEXT, favorite TEXT, note TEXT, tag TEXT)");
            command = new SQLiteCommand(queryString.ToString(), conn);
            command.ExecuteNonQuery();
        }

        public KanziiObject GetKanzii(KanziiObject kanzii)
        {
            KanziiObject result = new KanziiObject();

            StringBuilder queryString = new StringBuilder();
            queryString.Append("SELECT * FROM kanzii WHERE ");
            if (kanzii.id > 0)
            {
                queryString.Append("id = ").Append(kanzii.id).Append(" AND ");
            }
            if (kanzii.mazii_id != null)
            {
                queryString.Append("mazii_id = ").Append(kanzii.mazii_id).Append(" AND ");
            }
            if (kanzii.word != null)
            {
                queryString.Append("word = ").Append(kanzii.word).Append(" AND ");
            }
            if (kanzii.lesson != null)
            {
                queryString.Append("lesson = ").Append(kanzii.lesson).Append(" AND ");
            }
            if (kanzii.vi_mean != null)
            {
                queryString.Append("vi_mean = ").Append(kanzii.vi_mean).Append(" AND ");
            }
            if (kanzii.uvi_mean != null)
            {
                queryString.Append("uvi_mean = ").Append(kanzii.uvi_mean).Append(" AND ");
            }
            if (kanzii.cn_mean != null)
            {
                queryString.Append("cn_mean = ").Append(kanzii.cn_mean).Append(" AND ");
            }
            if (kanzii.ucn_mean != null)
            {
                queryString.Append("ucn_mean = ").Append(kanzii.ucn_mean).Append(" AND ");
            }
            if (kanzii.image != null)
            {
                queryString.Append("image = ").Append(kanzii.image).Append(" AND ");
            }
            if (kanzii.remember != null)
            {
                queryString.Append("remember = ").Append(kanzii.remember).Append(" AND ");
            }
            if (kanzii.write != null)
            {
                queryString.Append("write = ").Append(kanzii.write).Append(" AND ");
            }
            if (kanzii.onjomi != null)
            {
                queryString.Append("onjomi = ").Append(kanzii.onjomi).Append(" AND ");
            }
            if (kanzii.ronjomi != null)
            {
                queryString.Append("ronjomi = ").Append(kanzii.ronjomi).Append(" AND ");
            }
            if (kanzii.kunjomi != null)
            {
                queryString.Append("kunjomi = ").Append(kanzii.kunjomi).Append(" AND ");
            }
            if (kanzii.rkunjomi != null)
            {
                queryString.Append("rkunjomi = ").Append(kanzii.rkunjomi).Append(" AND ");
            }
            if (kanzii.numstroke != null)
            {
                queryString.Append("numstroke = ").Append(kanzii.numstroke).Append(" AND ");
            }
            if (kanzii.favorite != null)
            {
                queryString.Append("favorite = ").Append(kanzii.favorite).Append(" AND ");
            }
            if (kanzii.note != null)
            {
                queryString.Append("note = ").Append(kanzii.note).Append(" AND ");
            }
            if (kanzii.tag != null)
            {
                queryString.Append("tag = ").Append(kanzii.tag).Append(" AND ");
            }
            if (queryString.ToString().IndexOf("AND") > 0)
            {
                queryString.Remove(queryString.Length - 5, 5);
            }
            command = new SQLiteCommand(queryString.ToString(), conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.id = int.Parse(reader["id"].ToString());
                result.mazii_id = reader["mazii_id"].ToString();
                result.word = reader["word"].ToString();
                result.lesson = reader["lesson"].ToString();
                result.vi_mean = reader["vi_mean"].ToString();
                result.uvi_mean = reader["uvi_mean"].ToString();
                result.cn_mean = reader["cn_mean"].ToString();
                result.ucn_mean = reader["ucn_mean"].ToString();
                result.image = reader["image"].ToString();
                result.remember = reader["remember"].ToString();
                result.write = reader["write"].ToString();
                result.onjomi = reader["onjomi"].ToString();
                result.ronjomi = reader["ronjomi"].ToString();
                result.kunjomi = reader["kunjomi"].ToString();
                result.rkunjomi = reader["rkunjomi"].ToString();
                result.numstroke = reader["numstroke"].ToString();
                result.favorite = reader["favorite"].ToString();
                result.note = reader["note"].ToString();
                result.tag = reader["tag"].ToString();
            }

            return result;
        }

        public Boolean InsertKanzii(KanziiObject kanzii)
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("INSERT INTO kanzii VALUES(");
            queryString.Append(kanzii.id).Append(", ");
            queryString.Append(kanzii.mazii_id).Append(", ");
            queryString.Append(kanzii.word).Append(", ");
            queryString.Append(kanzii.lesson).Append(", ");
            queryString.Append(kanzii.vi_mean).Append(", ");
            queryString.Append(kanzii.uvi_mean).Append(", ");
            queryString.Append(kanzii.cn_mean).Append(", ");
            queryString.Append(kanzii.ucn_mean).Append(", ");
            queryString.Append(kanzii.image).Append(", ");
            queryString.Append(kanzii.remember).Append(", ");
            queryString.Append(kanzii.write).Append(", ");
            queryString.Append(kanzii.onjomi).Append(", ");
            queryString.Append(kanzii.ronjomi).Append(", ");
            queryString.Append(kanzii.kunjomi).Append(", ");
            queryString.Append(kanzii.rkunjomi).Append(", ");
            queryString.Append(kanzii.numstroke).Append(", ");
            queryString.Append(kanzii.favorite).Append(", ");
            queryString.Append(kanzii.note).Append(", ");
            queryString.Append(kanzii.tag).Append(")");
            command = new SQLiteCommand(queryString.ToString(), conn);
            if (command.ExecuteNonQuery() > 0)
            {
                return true;
            }
            return false;
        }

        public void Truncate()
        {
            StringBuilder queryString = new StringBuilder("TRUNCATE TABLE kanzii");
            command = new SQLiteCommand(queryString.ToString(), conn);
            command.ExecuteNonQuery();
        }
    }
}
