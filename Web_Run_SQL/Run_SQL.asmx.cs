using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Common;
using Newtonsoft.Json;
using System.Data.SqlClient;
using static Common.SQLParameter;
using System.Data;

namespace Web_Run_SQL
{
    /// <summary>
    ///Run_SQL 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下列一行。
    // [System.Web.Script.Services.ScriptService]
    public class Run_SQL : System.Web.Services.WebService
    {

        [WebMethod]
        public string FormDataTable(string SQL, string parameter)
        {
            return RunSQLRealDo(SQL, parameter, true);
        }

        [WebMethod]
        public void Execute(string SQL, string parameter)
        {
            RunSQLRealDo(SQL, parameter, false);
        }

        private string RunSQLRealDo(string SQL, string parameter, bool need_dt)
        {
            Trace.SetLogFile(@"C:\\log\", "runSQL");
            try
            {
                //"server=EUE440-PC;User ID=jimny_test;Password=1qaz@WSX;database=Billhunter_Ultimate_test;Connection Reset=FALSE;Connect Timeout=250"
                string Connect_string = "server=EUE440-PC;User ID=jimny_test;Password=1qaz@WSX;database=Billhunter_Ultimate_test;Connection Reset=FALSE;Connect Timeout=250";
                string JsonParameter = Encryption.DecryptBase64(parameter);
                Enum_Type_Transfer ETT = new Enum_Type_Transfer();
                List<ListSQLParameter> LSQLP = (List<ListSQLParameter>)JsonConvert.DeserializeObject(JsonParameter, (typeof(List<ListSQLParameter>)));
                SqlParameter[] param = new SqlParameter[LSQLP.Count()];
                int nowparam = 0;
                foreach (ListSQLParameter SQLP in LSQLP)
                {
                    switch (ETT.Enum_Type[(int)SQLP.type])
                    {
                        case "NVarChar":
                            param[nowparam] = MakeInParam("@" + SQLP.name, SqlDbType.NVarChar, SQLP.size, SQLP.value);
                            break;
                        case "Int":
                            param[nowparam] = MakeInParam("@" + SQLP.name, SqlDbType.Int, SQLP.size, SQLP.value);
                            break;
                        case "DateTime":
                            param[nowparam] = MakeInParam("@" + SQLP.name, SqlDbType.DateTime, SQLP.size, SQLP.value);
                            break;
                        default:
                            throw new Exception("傳入的Parameter TYPE不合法。");
                            break;
                    }
                    nowparam++;
                }
                if (need_dt)
                    return Encryption.EncryptBase64(JsonConvert.SerializeObject(Link_DB.ExecuteDataTable(Connect_string, CommandType.Text, Encryption.DecryptBase64(SQL), param)));
                else
                {
                    Link_DB.ExecuteNonQuery(Connect_string, CommandType.Text, Encryption.DecryptBase64(SQL), param);
                    return "";
                }
            }
            catch (Exception ex)
            {
                Trace.DebugWrite("RunSQLRealDo", ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
    }
}
