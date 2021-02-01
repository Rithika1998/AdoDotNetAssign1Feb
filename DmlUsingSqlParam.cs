using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AdoDotNetConnectedArch
{
    class UsingParameters
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

        public int InsertWithParameters()
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
                cmd = new SqlCommand("insert into EmpTab values(@ename,@esal,@deptid)", cn);
                cmd.Parameters.Add("@ename", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptid", SqlDbType.Int).Value = did;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                //ShowData();
                return i;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

        public int DelWithParameters()
        {
            try
            {
                Console.WriteLine("enter employee id");
                var eid = Convert.ToInt32(Console.ReadLine());
                
                cn = new SqlConnection(@"Data Source=DESKTOP-I148QJP\MSSQLSERVER01;
                   Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("delete from EmpTab where EmpId=@eid", cn);
                cmd.Parameters.Add("@eid", SqlDbType.Int).Value = eid;
                
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                //ShowData();
                Console.WriteLine("using sql parameters deleted on row");
                return i;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

        public int UpdateWithParameters()
        {
            try
            {
                Console.WriteLine("enter Employee id");
                var eid = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("enter employee name");
                var ename = Console.ReadLine();
                Console.WriteLine("enter employee salary");
                var esal = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("enter employee dept id");
                var did = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection(@"Data Source=DESKTOP-I148QJP\MSSQLSERVER01;
                   Initial Catalog=WFA3DotNet;Integrated Security=True");

                cmd = new SqlCommand("update EmpTab set EmpName=@ename,Salary=@esal,DeptNo=@did where EmpId=@eid", cn);
                
                cmd.Parameters.Add("@eid", SqlDbType.Int).Value = eid;
                cmd.Parameters.Add("@ename", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@did", SqlDbType.Int).Value = did;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("using sql parameters one row is updated");
                //ShowData();
                return i;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

    }

    class DmlUsingSqlParam
    {
        static void Main()
        {
            int choice;
            UsingParameters up = new UsingParameters();
            up.ShowData();
            Console.WriteLine("------------------------------");

           do
           {
               // up.ShowData();
                Console.WriteLine("enter choice");
                Console.WriteLine("1.insert data");
                Console.WriteLine("2.delete data");
                Console.WriteLine("3.update data");

                choice = int.Parse(Console.ReadLine());
                switch(choice)
                {
                    case 1: up.InsertWithParameters();
                        break;
                    case 2: up.DelWithParameters();
                        break;
                    case 3: up.UpdateWithParameters();
                        break;
                    default:
                        Console.WriteLine("exit...");
                        break;
                }
               
            }
            while (choice >= 1 && choice <=3);
        }
    }
}
