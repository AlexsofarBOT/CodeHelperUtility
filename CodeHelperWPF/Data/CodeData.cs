﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;

namespace CodeHelperWPF.Data
{
    class CodeData
    {
        public object Language { get; set; }

        private static SQLiteConnection _db = new SQLiteConnection("Data Source=codelsm.db3; version=3");

        /// <summary>Метод GetLanguages() открывает соединения с БД. Посредством команд <see cref="F:System.Data.SQLite"/> формируется строка запроса.
        /// Если в таблице имеются записи то создаются экземпляры объекта CodeData, в авто-свойтва экземпляров присваиваются получаемые данные</summary>
        /// <returns>Возращает коллекцию объектов пользовательского типа CodeData</returns>
        public CodeData[] GetLanguages()
        {
            OpenSQLite();

            var result = new CodeData[GetEntriesCount()];

            SQLiteCommand CMD = _db.CreateCommand();
            CMD.CommandText = "select Language from Languages";
            SQLiteDataReader SQL = CMD.ExecuteReader();

            if (SQL.HasRows)
            {
                int count = 0;

                while (SQL.Read())
                {
                    result[count] = new CodeData() { Language = SQL["Language"] };
                    count++;
                }
            }

            CloseSQLite();

            return result;
        }

        private static int GetEntriesCount()
        {
            SQLiteCommand CMD = _db.CreateCommand();
            CMD.CommandText = "select count(*) from Languages";
            return Convert.ToInt32(CMD.ExecuteScalar());
        }

        private static void OpenSQLite()
        {
            _db.Open();
        }

        private static void CloseSQLite()
        {
            _db.Close();
        }

        private void Add()
        {
            //if (tbName.Text != "" && tbLastName.Text != "")
            //{
            //    SQLiteCommand CMD = _db.CreateCommand();
            //    CMD.CommandText = "insert into Users(Firstname, Lastname) values(@fname, @lname)";
            //    CMD.Parameters.Add("@fname", System.CodeData.DbType.String).Value = tbName.Text.ToUpper();
            //    CMD.Parameters.Add("@lname", System.Data.DbType.String).Value = tbLastName.Text.ToUpper();
            //    CMD.ExecuteNonQuery();
            //}
        }
    }
}
