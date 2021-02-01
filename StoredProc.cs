using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AdoDotNetConnectedArch
{
    class StoredProcEx
    {
        SqlConnection cn = null; 
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        public int ShowData()
        {
            try
            {

                cn = new SqlConnection(@"Data Source=DESKTOP-I148QJP\MSSQLSERVER01;
                   Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("select * from EmpTab", cn);
                cn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["empid"]}\t {dr["empname"]}\t {dr["salary"]}\t {dr["DeptNo"]}");
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return 0;
            }
            finally
            {
                cn.Close();
                dr.Close();
            }
        }
        public int InsertWithSp()
        {
            try
            {

                Console.WriteLine("enter employee name");
                var ename = Console.ReadLine();

                Console.WriteLine("enter employee salary");
                var esal = Convert.ToSingle(Console.ReadLine());

                Console.WriteLine("enter employee dept id");
                var did = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection(@"Data Source=DESKTOP-I148QJP\MSSQLSERVER01;
                Initial Catalog=WFA3DotNet;Integrated Security=True");

                cmd = new SqlCommand("sp_InsertEmpTab", cn);
                cmd.CommandType = CommandType.StoredProcedure; 
                               
                cmd.Parameters.Add("@ename", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptId", SqlDbType.Int).Value = did;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("one row is inserted ");
                ShowData();
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return 1;
            }
            finally
            {
                cn.Close();
            }

        }

       
        public int DeleteWithSp()
        {
            try
            {

                Console.WriteLine("enter employee id");
                var eid = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection(@"Data Source=DESKTOP-I148QJP\MSSQLSERVER01;
                Initial Catalog=WFA3DotNet;Integrated Security=True");

                cmd = new SqlCommand("sp_DeleteEmpTab", cn);
                cmd.CommandType = CommandType.StoredProcedure; 

                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;
                
                cn.Open();
                int i = cmd.ExecuteNonQuery();

                Console.WriteLine("one row is deleted ");
                ShowData();
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return 1;
            }
            finally
            {
                cn.Close();
            }

        }
        public int UpdateWithSp()
        {
            try
            {

                Console.WriteLine("enter an existing employee id to update the records");
                var eid = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("enter employee name");
                var ename = Console.ReadLine();

                Console.WriteLine("enter employee salary");
                var esal = Convert.ToSingle(Console.ReadLine());

                Console.WriteLine("enter employee dept id");
                var did = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection(@"Data Source=DESKTOP-I148QJP\MSSQLSERVER01;
                Initial Catalog=WFA3DotNet;Integrated Security=True");

                cmd = new SqlCommand("sp_UpdateEmpTab", cn);
                cmd.CommandType = CommandType.StoredProcedure; //this tells that the above 160line first param is a stored proc


                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid; //eid from line 146
                cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptId", SqlDbType.Int).Value = did;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("one row is updated");
                ShowData();
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return 1;
            }
            finally
            {
                cn.Close();
            }

        }

    }

    class StoredProc
    {
        static void Main()
        {
            int choice;
            StoredProcEx spe = new StoredProcEx();
            //ir.ShowData();
            do
            {

                Console.WriteLine("enter choice");

                Console.WriteLine("1.insert data");
                Console.WriteLine("2.delete data");
                Console.WriteLine("3.update data");
                
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        spe.InsertWithSp();
                        break;
                    case 2:
                        spe.DeleteWithSp();
                        break;
                    case 3:
                        spe.UpdateWithSp();
                        break;
                    default:
                        Console.WriteLine("exit...");
                        break;
                }

            }
            while (choice >= 1 && choice <= 3);
        }
    }
}
