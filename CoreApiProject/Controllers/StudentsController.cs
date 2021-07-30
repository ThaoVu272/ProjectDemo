using CoreApiProject.Models;
using Dapper.Oracle;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApiProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        List<Student> _oStudent = new List<Student>();
        public StudentsController()
        {
            //_oStudent = new List<Student>();
            //for (int i = 1; i <= 9; i++)
            //{
            //    _oStudent.Add(new Student()
            //    {
            //        StudentId = i,
            //        Name = "Student" + i,
            //        Roll = "100" + i
            //    });
            //}
            _oStudent = GetStudents();
        }
        // GET: api/<Students>
        [HttpGet]
        //[Route("GetStudents")]
        public List<Student> GetStudents()
       {
            //DataTable dtEmp = new DataTable();
            //using (OracleConnection objConn = new OracleConnection("Data Source=THAODB; User ID=student; Password=1234"))
            //{
            //    OracleDataAdapter objAdapter = new OracleDataAdapter();
            //    OracleCommand objCmd = new OracleCommand();
            //    objCmd.Connection = objConn;
            //    objCmd.CommandText = "students_get";
            //    objCmd.CommandType = CommandType.StoredProcedure;
            //    objCmd.Parameters.Add("studentid", OracleDbType.Int32).Value = -1;
            //    objCmd.Parameters.Add("rs", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            //    objAdapter.SelectCommand = objCmd;
            //    try
            //    {
            //        objConn.Open();
            //        objAdapter.Fill(dtEmp);
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Console.WriteLine("Exception: {0}", ex.ToString());
            //    }
            //    objConn.Close();
            //}
            //List<Student> studentList = new List<Student>();
            //for (int i = 0; i < dtEmp.Rows.Count; i++)
            //{
            //    Student student = new Student();
            //    student.StudentId = Convert.ToInt32(dtEmp.Rows[i]["studentId"]);
            //    student.Name = dtEmp.Rows[i]["name"].ToString();
            //    student.Roll = dtEmp.Rows[i]["roll"].ToString();
            //    studentList.Add(student);
            //}
            //return studentList;
            return _oStudent;
        }
        public List<Student> GetStudents2(int id)
        {
            DataTable dtEmp = new DataTable();
            using (OracleConnection objConn = new OracleConnection("Data Source=THAODB; User ID=student; Password=1234"))
            {
                OracleDataAdapter objAdapter = new OracleDataAdapter();
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "students_get";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("studentid", OracleDbType.Int32).Value = id;
                objCmd.Parameters.Add("rs", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                objAdapter.SelectCommand = objCmd;
                try
                {
                    objConn.Open();
                    objAdapter.Fill(dtEmp);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Exception: {0}", ex.ToString());
                }
                objConn.Close();
            }
            List<Student> studentList = new List<Student>();
            for (int i = 0; i < dtEmp.Rows.Count; i++)
            {
                Student student = new Student();
                student.StudentId = Convert.ToInt32(dtEmp.Rows[i]["studentId"]);
                student.Name = dtEmp.Rows[i]["name"].ToString();
                student.Roll = dtEmp.Rows[i]["roll"].ToString();
                studentList.Add(student);
            }
            return studentList;
        }



        // POST api/<Student>
        [HttpPost]
        public List<Student> Post([FromBody] Student oStudent)
        {
            using (OracleConnection objConn = new OracleConnection("Data Source=THAODB; User ID=student; Password=1234"))
            {
                OracleDataAdapter objAdapter = new OracleDataAdapter();
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "students_i";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_id", OracleDbType.Int32).Value = 1;
                objCmd.Parameters.Add("p_name", OracleDbType.NVarchar2,255).Value = "Studentx";
                objCmd.Parameters.Add("p_roll", OracleDbType.NVarchar2,255).Value = "10092";
                objCmd.Parameters.Add("p_code", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_mess", OracleDbType.NVarchar2,255).Direction = ParameterDirection.Output;
                objAdapter.InsertCommand = objCmd;
                try
                {
                    objConn.Open();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Exception: {0}", ex.ToString());
                }
                objConn.Close();
            }

            return GetStudents2(1);
        }

        // PUT api/<Student>/5
        [HttpPut("{id}")]
        public List<Student> Put(int id, [FromBody] Student oStudent)
        {
            _oStudent.RemoveAll(x => x.StudentId == oStudent.StudentId);
            _oStudent.Add(oStudent);
            return _oStudent;
        }

        // DELETE api/<Student>/5
        [HttpDelete("{id}")]
        public List<Student> Delete(int id)
        {
            _oStudent.RemoveAll(x => x.StudentId == id);
            return _oStudent;
        }
    }
}
