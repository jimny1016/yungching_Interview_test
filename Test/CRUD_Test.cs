using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.SQLParameter;

namespace Test
{
    public class CRUD_Test
    {
        public static string _URL = @"http://localhost:1454/Run_SQL.asmx";
        public static void Insert_web_login_check(string idkey, string go_page, string url_request, string idno, string cust_code)
        {
            string SQL = "INSERT INTO web_login_check (idkey,go_page,url_request,idno) VALUES (@idkey,@go_page,@url_request,@idno)";
            WebReference.Run_SQL RunSQL = new WebReference.Run_SQL();
            RunSQL.Url = _URL;
            List<ListSQLParameter> LSQLP = new List<ListSQLParameter>();
            ListSQLParameter SQLP = new ListSQLParameter();
            SQLP.name = "idkey";
            SQLP.size = 10;
            SQLP.value = idkey;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            SQLP = new ListSQLParameter();
            SQLP.name = "go_page";
            SQLP.size = 500;
            SQLP.value = go_page;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            SQLP = new ListSQLParameter();
            SQLP.name = "url_request";
            SQLP.size = 500;
            SQLP.value = url_request;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            SQLP = new ListSQLParameter();
            SQLP.name = "idno";
            SQLP.size = 20;
            SQLP.value = idno;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            RunSQL.Execute(Encryption.EncryptBase64(SQL), Encryption.EncryptBase64(JsonConvert.SerializeObject(LSQLP)));
        }

        public static DataTable Select_web_login_check(string idkey, string go_page, string url_request, string idno, string cust_code)
        {
            string SQL = "SELECT * FROM web_login_check WHERE idkey=@idkey AND go_page=@go_page AND url_request=@url_request AND idno=@idno";
            WebReference.Run_SQL RunSQL = new WebReference.Run_SQL();
            RunSQL.Url = _URL;
            List<ListSQLParameter> LSQLP = new List<ListSQLParameter>();
            ListSQLParameter SQLP = new ListSQLParameter();
            SQLP.name = "idkey";
            SQLP.size = 10;
            SQLP.value = idkey;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            SQLP = new ListSQLParameter();
            SQLP.name = "go_page";
            SQLP.size = 500;
            SQLP.value = go_page;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            SQLP = new ListSQLParameter();
            SQLP.name = "url_request";
            SQLP.size = 500;
            SQLP.value = url_request;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            SQLP = new ListSQLParameter();
            SQLP.name = "idno";
            SQLP.size = 20;
            SQLP.value = idno;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            return
                (DataTable)JsonConvert.DeserializeObject(
                    Encryption.DecryptBase64(
                        RunSQL.FormDataTable(Encryption.EncryptBase64(SQL), Encryption.EncryptBase64(JsonConvert.SerializeObject(LSQLP)))
                    ), (typeof(DataTable))
                );
        }

        public static void Update_web_login_check(string idkey, string go_page, string url_request, string idno, string cust_code)
        {
            string SQL = "UPDATE web_login_check SET url_request=@url_request WHERE idkey=@idkey AND go_page=@go_page AND idno=@idno";
            WebReference.Run_SQL RunSQL = new WebReference.Run_SQL();
            RunSQL.Url = _URL;
            List<ListSQLParameter> LSQLP = new List<ListSQLParameter>();
            ListSQLParameter SQLP = new ListSQLParameter();
            SQLP = new ListSQLParameter();
            SQLP.name = "idkey";
            SQLP.size = 10;
            SQLP.value = idkey;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            SQLP = new ListSQLParameter();
            SQLP.name = "go_page";
            SQLP.size = 500;
            SQLP.value = go_page;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            SQLP = new ListSQLParameter();
            SQLP.name = "url_request";
            SQLP.size = 500;
            SQLP.value = url_request;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            SQLP = new ListSQLParameter();
            SQLP.name = "idno";
            SQLP.size = 20;
            SQLP.value = idno;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            RunSQL.Execute(Encryption.EncryptBase64(SQL), Encryption.EncryptBase64(JsonConvert.SerializeObject(LSQLP)));
        }

        public static void Delete_web_login_check(string idkey, string go_page, string url_request, string idno, string cust_code)
        {
            string SQL = "DELETE FROM web_login_check WHERE idkey=@idkey AND go_page=@go_page AND url_request=@url_request AND idno=@idno";
            WebReference.Run_SQL RunSQL = new WebReference.Run_SQL();
            RunSQL.Url = _URL;
            List<ListSQLParameter> LSQLP = new List<ListSQLParameter>();
            ListSQLParameter SQLP = new ListSQLParameter();
            SQLP = new ListSQLParameter();
            SQLP.name = "idkey";
            SQLP.size = 10;
            SQLP.value = idkey;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            SQLP = new ListSQLParameter();
            SQLP.name = "go_page";
            SQLP.size = 500;
            SQLP.value = go_page;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            SQLP = new ListSQLParameter();
            SQLP.name = "url_request";
            SQLP.size = 500;
            SQLP.value = url_request;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            SQLP = new ListSQLParameter();
            SQLP.name = "idno";
            SQLP.size = 20;
            SQLP.value = idno;
            SQLP.type = ListSQLParameter.Enum_Type_code.NVarChar;
            LSQLP.Add(SQLP);
            RunSQL.Execute(Encryption.EncryptBase64(SQL), Encryption.EncryptBase64(JsonConvert.SerializeObject(LSQLP)));
        }
    }
}
