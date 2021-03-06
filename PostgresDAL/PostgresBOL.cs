﻿using System.Collections.Generic;

namespace PostgresDAL
{
    using System;
    using System.Data;
    using Npgsql;
    public static class PostgresBOL
    {
        public static NpgsqlConnection dbcon;
        public static IDbCommand dbcmd;
        public static IDataReader reader;

        public static void Open()
        {
            string connectionString =
            "Server=localhost;" +
            "Database=test;" +
            "User ID=postgres;" +
            "Password=postgres;";

            dbcon = new NpgsqlConnection(connectionString);
            dbcon.Open();
            dbcmd = dbcon.CreateCommand();
        }

        public static void Close()
        {
            dbcon.Close();
        }

        public static DataSet GetVietlottVNs()
        {
            NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter("SELECT * FROM public.\"VietlottVN\";", dbcon);
            DataSet ds = new DataSet();
            myDataAdapter.Fill(ds, "VietlottVNs");
            return ds;
        }

        public static DataSet GetTop10VietlottVNs()
        {
            NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter("SELECT * FROM public.\"VietlottVN\" ORDER BY DayPrize ASC;", dbcon);
            DataSet ds = new DataSet();
            myDataAdapter.Fill(ds, "VietlottVNs");
            return ds;
        }

        public static void AddVietlottVN(int drawid, DateTime dayPrize, string fullBlockNumber, int numberOne, int numberTwo,
            int numberThree, int numberFour, int numberFive, int numberSix, DateTime importDate)
        {

            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO public.\"VietlottVN\" ( \"DrawId\", \"DayPrize\", \"FullBlockNumber\", \"NumberOne\","
                + " \"NumberTwo\", \"NumberThree\", \"NumberFour\", \"NumberFive\", \"NumberSix\", \"ImportDate\")"
                + " VALUES(:drawid, :dayPrize, :fullBlockNumber, :numberOne, :numberTwo, :numberThree, :numberFour, :numberFive, :numberSix, :importDate)", dbcon);
            command.Parameters.Add(new NpgsqlParameter("Drawid", DbType.Int32));
            command.Parameters.Add(new NpgsqlParameter("DayPrize", DbType.Date));
            command.Parameters.Add(new NpgsqlParameter("FullBlockNumber", DbType.String));
            command.Parameters.Add(new NpgsqlParameter("NumberOne", DbType.Int32));
            command.Parameters.Add(new NpgsqlParameter("NumberTwo", DbType.Int32));
            command.Parameters.Add(new NpgsqlParameter("NumberThree", DbType.Int32));
            command.Parameters.Add(new NpgsqlParameter("NumberFour", DbType.Int32));
            command.Parameters.Add(new NpgsqlParameter("NumberFive", DbType.Int32));
            command.Parameters.Add(new NpgsqlParameter("NumberSix", DbType.Int32));
            command.Parameters.Add(new NpgsqlParameter("ImportDate", DbType.Date));

            command.Parameters[0].Value = drawid;
            command.Parameters[1].Value = dayPrize;
            command.Parameters[2].Value = fullBlockNumber;
            command.Parameters[3].Value = numberOne;
            command.Parameters[4].Value = numberTwo;
            command.Parameters[5].Value = numberThree;
            command.Parameters[6].Value = numberFour;
            command.Parameters[7].Value = numberFive;
            command.Parameters[8].Value = numberSix;
            command.Parameters[9].Value = importDate;
            dbcmd = dbcon.CreateCommand();
            reader = command.ExecuteReader();
            reader.Close();
        }

        public static List<PGVietLottVN> GetVietLottVNs()
        {

            return new List<PGVietLottVN>();
        }

        public static void InsertVietLottVN(PGVietLottVN obj)
        {
            if (!CheckExists(obj.DayPrize, obj.FullBlockNumber))
            {
                AddVietlottVN(obj.Drawid, obj.DayPrize, obj.FullBlockNumber, obj.NumberOne, obj.NumberTwo, obj.NumberThree, obj.NumberFour, obj.NumberFive, obj.NumberSix, DateTime.Now);
            }
        }

        public static bool CheckExists(DateTime dt, string fullNumber)
        {
            NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter("SELECT * FROM public.\"VietlottVN\" where public.\"VietlottVN\".\"FullBlockNumber\" = '" + fullNumber + "' and public.\"VietlottVN\".\"DayPrize\" = '" + dt + "';", dbcon);
            DataSet ds = new DataSet();
            myDataAdapter.Fill(ds, "VietlottVNs");
            return ds.Tables["VietlottVNs"].Rows.Count > 0;
        }

        public static int TotalRows()
        {
            NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter("SELECT * FROM public.\"VietlottVN\";", dbcon);
            DataSet ds = new DataSet();
            myDataAdapter.Fill(ds, "VietlottVNs");
            return ds.Tables["VietlottVNs"].Rows.Count;
        }

        public static int CalculateNumber(int number)
        {
            NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter("SELECT * FROM public.\"VietlottVN\" " +
                                                                    "where public.\"VietlottVN\".\"NumberOne\" = '" + number + "'" +
                                                                    " OR public.\"VietlottVN\".\"NumberTwo\" = '" + number + "'" +
                                                                    " OR public.\"VietlottVN\".\"NumberThree\" = '" + number + "'" +
                                                                    " OR public.\"VietlottVN\".\"NumberFour\" = '" + number + "'" +
                                                                    " OR public.\"VietlottVN\".\"NumberFive\" = '" + number + "'" +
                                                                    " OR public.\"VietlottVN\".\"NumberSix\" = '" + number + "';", dbcon);
            DataSet ds = new DataSet();
            myDataAdapter.Fill(ds, "VietlottVNs");
            int totalRowns = ds.Tables["VietlottVNs"].Rows.Count;
            return totalRowns;
        }

        public static List<VietlottVNDto> GetTop10()
        {
            var lst = new List<VietlottVNDto>();
            NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter("SELECT * FROM public.\"VietlottVN\" order by public.\"VietlottVN\".\"DayPrize\" desc LIMIT 10;", dbcon);
            DataSet ds = new DataSet();
            myDataAdapter.Fill(ds, "VietlottVNs");
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow row = (DataRow) ds.Tables[0].Rows[i];
                 
                    //((DataRow)row).ItemArray[0]
                    lst.Add(new VietlottVNDto()
                    {
                        VietLottID = (int)row.ItemArray[0],
                        DrawId = (int)row.ItemArray[1],
                        DayPrize = (DateTime)row.ItemArray[2],
                        FullBlockNumber = (string)row.ItemArray[3],
                        NumberOne = (int)row.ItemArray[4],
                        NumberTwo = (int)row.ItemArray[5],
                        NumberThree = (int)row.ItemArray[6],
                        NumberFour = (int)row.ItemArray[7],
                        NumberFive = (int)row.ItemArray[8],
                        NumberSix = (int)row.ItemArray[9],
                        ImportDate = (DateTime)row.ItemArray[10]
                    });
                }
            }
            return lst;
        }

    }
}
