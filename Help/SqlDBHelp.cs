using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/*********************************
 * 类名：DBHelper
 * 功能描述：提供数据访问基础操作
 * ******************************/

public static class SqlDBHelp
{
    //数据库连接属性
    public static readonly string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;


    /// <summary>
    /// 执行有参SQL语句
    /// </summary>
    public static int ExecuteCommand(string sql, params SqlParameter[] cmdParams)
    {
        SqlCommand cmd = new SqlCommand();
        using (SqlConnection conn = new SqlConnection(connString))
        {
            PrepareCommand(sql, conn, cmd, cmdParams);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            conn.Close();
            return val;
        }

    }

    /// <summary>
    /// 执行有参SQL语句，并返回执行行数
    /// </summary>
    public static int GetScalar(string sql, params SqlParameter[] cmdParams)
    {
        SqlCommand cmd = new SqlCommand();
        using (SqlConnection conn = new SqlConnection(connString))
        {
            PrepareCommand(sql, conn, cmd, cmdParams);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            conn.Close();
            return Convert.ToInt32(val);
        }


    }
    /// <summary>
    /// 执行无参SQL语句，并返SqlDataReader
    /// </summary>

    /// <summary>
    /// 执行有参SQL语句，并返SqlDataReader
    /// </summary>
    public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] cmdParams)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            PrepareCommand(sql, conn, cmd, cmdParams);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return dr;
        }
        catch
        {
            conn.Close();
            throw;
        }
    }
    /// <summary>
    /// 执行有参SQL语句，并返DataTable
    /// </summary>
    public static DataTable GetTable(string sql, params SqlParameter[] cmdParams)
    {
        DataSet ds = new DataSet();
        using (SqlConnection conn = new SqlConnection(connString))
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            PrepareCommand(sql, conn, da.SelectCommand, cmdParams);
            da.Fill(ds, "temp");
            return ds.Tables["temp"];
        }


    }
    /// <summary>
    private static void PrepareCommand(string sql, SqlConnection conn, SqlCommand cmd, params SqlParameter[] cmdParams)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        if (cmdParams != null)
        {
            foreach (SqlParameter param in cmdParams)
                cmd.Parameters.Add(param);
        }
    }

    /// <summary>  
    /// 查询结果集中第一行第一列的值  
    /// </summary>  
    /// <param name="sql">T-Sql语句</param>  
    /// <param name="values">参数数组</param>  
    /// <returns>第一行第一列的值</returns>  
    public static int ExecuteScalar(string sql, SqlParameter[] values)
    {
        using (SqlConnection Connection = new SqlConnection(connString))
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            return result;
        }
    }


}

