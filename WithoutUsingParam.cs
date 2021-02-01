using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AdoDotNetConnectedArch
{
    class InsertRow
    {
        SqlConnection cn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;

        public int ShowData() 
        {
            try
            {
                Console.WriteLine("data from the table after the DML Command\n");
                Console.WriteLine("-------------------------------");
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


        public int InsertOneRow()
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
                cmd = new SqlCommand("insert into EmpTab values('" + ename + "'," + esal + "," + did + ")", cn); //here as shown string val are written in '"'" bcz string and varchar in sql are written in ''  (single quotes) int and etc, in normal double quotes
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("one row is added to the table");
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


        public int DelOneRow()
        {
            try
            {
                Console.WriteLine("delete employee details");
                var empid = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection(@"Data Source=DESKTOP-I148QJP\MSSQLSERVER01;
                   Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("delete from EmpTab where empid=" + empid + "", cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("deleted one row");
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

        public int UpdateOneRow()
        {
            try
            {
                Console.WriteLine("enter employee id");
                var empid = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("enter dept no");
                var deptno = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection(@"Data Source=DESKTOP-I148QJP\MSSQLSERVER01;
                   Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("update emptab set deptno=" + deptno + "where empid=" + empid + "", cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("one row updated to emptab");
                //ShowData();
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
    
        public int SearchOneRow()
        {
            try
            {
                Console.WriteLine("enter employee id");
                var empid = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection(@"Data Source=DESKTOP-I148QJP\MSSQLSERVER01;
                   Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("select *from EmpTab where EmpId=" + empid + "", cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine($"Employee Name: {dr["EmpName"].ToString()}");
                    Console.WriteLine($"Salary: {dr["Salary"].ToString()}");
                    Console.WriteLine($"DeptNo: {dr["DeptNo"].ToString()}");

                }

                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return 1;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }
        }
    }

    /*
     public int SelectWithSp()
     {

         try
         {
             Console.WriteLine("enter an existing employeeid");
             var eid = Convert.ToInt32(Console.ReadLine());
             cn = new SqlConnection(@"Data Source=DESKTOP-I148QJP\MSSQLSERVER01;
                Initial Catalog=WFA3DotNet;Integrated Security=True");

             cmd = new SqlCommand("sp_SelectId", cn);
             cmd.CommandType = CommandType.StoredProcedure;

             cmd.Parameters.Add("@empId", SqlDbType.Int).Value = eid;
             cn.Open();
             dr = cmd.ExecuteReader();
             while (dr.Read()) 
             {
                 Console.WriteLine($"emp name: {dr["empname"].ToString()}");
                 Console.WriteLine($"emp salary: {dr["salary"].ToString()}");
                 Console.WriteLine($"dept name: {dr["deptName"].ToString()}");

                 //Console.WriteLine($"{dr["empid"]}\t {dr["empname"]}\t{dr["salary"]}\t {dr["deptName"]}");
             }
             return 0;
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
    
     }*/

    class WithoutUsingParam
    {
        static void Main()
        {
            int choice;
            InsertRow ir = new InsertRow();
            //ir.ShowData();
            do
            {
                
                Console.WriteLine("enter choice");
                Console.WriteLine("1.insert data");
                Console.WriteLine("2.delete data");
                Console.WriteLine("3.update data");
                Console.WriteLine("4.Search data");


                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        ir.InsertOneRow();
                        break;
                    case 2:
                        ir.DelOneRow();
                        break;
                    case 3:
                        ir.UpdateOneRow();
                        break;
                    case 4:
                        ir.SearchOneRow();
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




