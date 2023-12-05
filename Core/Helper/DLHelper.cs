using Core.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Core.Helper
{
    public class DLHelper
    {
        public static object CreatDtoFromDataReader(Type type, IDataReader rd)
        {
            object obj = Activator.CreateInstance(type);
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var dicNameDr = new Dictionary<string, object>();

            for (int i = 0; i <= rd.FieldCount - 1; i++)
            {
                dicNameDr.Add(rd.GetName(i), rd[rd.GetName(i)]);
            }
            foreach (var prop in props)
            {
                if (dicNameDr.ContainsKey(prop.Name))
                {
                    prop.SetValue(obj, dicNameDr[prop.Name], null);
                }
            }
            return obj;
        }

        public static bool Insert(object source)
        {
            // gen sql insert
            var typeSoure = source.GetType();
            string strTableName = GetNameDataTable(typeSoure);
            var lstPropSource = typeSoure.GetProperties();
            var lstFieldName = GetListColumnInDB(strTableName);
            // loại bỏ trường ID
            lstFieldName.RemoveAll(x => x.ToUpper().Equals("ID"));
            string strSqlInser = "INSERT INTO " + strTableName + " (" + String.Join(",", lstFieldName) + ") VALUES ( " + BuilderSqlValue(source, lstFieldName) + ")";

            //return cmd.ExecuteNonQuery() == 1 ? true : false;

            var cmd = new SqlCommand(strSqlInser, Connection.ConnectToSQLDataBase());
            cmd.CommandType = CommandType.Text;
            var bInsert = cmd.ExecuteNonQuery() == 1 ? true : false;


            //var cmdSql = new SqlCommand(strSqlInser, Connection.ConnectToSQLDataBase());
            //cmdSql.CommandType = CommandType.Text;
            //bInsert= cmdSql.ExecuteNonQuery() == 1 ? true : false;

            return bInsert;
        }
        public static string GetNameDataTable(Type typeData)
        {
            return typeData.Name.Replace("Model", "");
        }

        public static List<string> GetListColumnInDB(string tableName)
        {
            var lstResult = new List<string>();
            string query = string.Format("SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}'", tableName);
            var cmd = new SqlCommand(query, Connection.ConnectToSQLDataBase());
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var dtoResult = CreatDtoFromDataReader(typeof(DbColumnModel), dr) as DbColumnModel;
                    lstResult.Add(dtoResult.COLUMN_NAME);
                }
            }
            return lstResult;
        }
    }
}
