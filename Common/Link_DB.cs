using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Link_DB
    {

        public static DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable mydt = new DataTable();
                SqlTransaction myTrans = null;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        try
                        {
                            PrepareCommand(cmd, connection, out myTrans, cmdType, cmdText, commandParameters);
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(mydt);
                            myTrans.Commit();
                            cmd.Parameters.Clear();
                            return mydt;
                        }
                        catch (SqlException e)
                        {
                            #region =========== Transaction Rollback
                            if (myTrans != null)
                            {
                                try
                                {
                                    Trace.DebugWrite("EU2010.Common.SQLHelperConsole", "Start Rollback()");
                                    if (myTrans.Connection != null)
                                        myTrans.Rollback();
                                }
                                catch (Exception ex)
                                {
                                    Trace.DebugWrite("EU2010.Common.SQLHelperConsole", "Rollback Exception");
                                    if (myTrans.Connection != null)
                                    {
                                        Trace.DebugWrite("EU2010.Common.SQLHelperConsole", "Rollback Error:" + ex.ToString());
                                    }
                                }
                            }
                            #endregion
                            #region ========== 檢查可否 retry
                            if (CheckErrorCanRetry(e))
                            {
                                Trace.DebugWrite("EU2010.Common.SQLHelperConsole", "Execute SqlCommand Error(網路連線問題)Retry(" + i + "):" + " ErrorNumber=" + e.Number + "  " + e.ToString() + "\r\n SQL = " + cmdText);

                                int TimeWait = 0; // 秒
                                if (i == 0)
                                    TimeWait = 10;
                                else if (i == 1)
                                    TimeWait = 60;
                                else if (i == 2)
                                    TimeWait = 120;
                                else
                                { // 若 超過 3 次皆失敗 則 直接送出錯誤訊息 結束程式
                                    Trace.DebugWrite("EU2010.Common.SQLHelperConsole", "Execute SqlCommand Error(網路連線問題)Retry(" + i + "):" + " ErrorNumber=" + e.Number + "  " + e.ToString() + "\r\n SQL = " + cmdText);
                                    throw e;
                                }

                                System.Threading.Thread.Sleep(1000 * TimeWait);
                            }
                            else
                            {
                                // 其它問題
                                Trace.DebugWrite("EU2010.Common.SQLHelperConsole", "Execute SqlCommand Error:" + e.ToString() + "\r\n SQL = " + cmdText);
                                throw e;
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return new DataTable();
        }
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlTransaction myTrans = null;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        try
                        {
                            PrepareCommand(cmd, conn, out myTrans, cmdType, cmdText, commandParameters);
                            int val = cmd.ExecuteNonQuery();
                            myTrans.Commit();
                            cmd.Parameters.Clear();
                            return val;
                        }
                        catch (SqlException e)
                        {
                            #region =========== Transaction Rollback
                            if (myTrans != null)
                            {
                                try
                                {
                                    Trace.DebugWrite("EU2012.Common.SQLHelperConsole", "Start Rollback()");
                                    if (myTrans.Connection != null)
                                        myTrans.Rollback();
                                }
                                catch (Exception ex)
                                {
                                    Trace.DebugWrite("EU2012.Common.SQLHelperConsole", "Rollback Exception");
                                    if (myTrans.Connection != null)
                                    {
                                        Trace.DebugWrite("EU2012.Common.SQLHelperConsole", "Rollback Error:" + ex.ToString());
                                    }
                                }
                            }
                            #endregion
                            #region ========== 檢查可否 retry
                            if (CheckErrorCanRetry(e))
                            {
                                Trace.DebugWrite("EU2012.Common.SQLHelperConsole", "Execute SqlCommand Error(網路連線問題)Retry(" + i + "):" + " ErrorNumber=" + e.Number + "  " + e.ToString() + "\r\n SQL = " + cmdText);

                                int TimeWait = 0; // 秒
                                if (i == 0)
                                    TimeWait = 10;
                                else if (i == 1)
                                    TimeWait = 60;
                                else if (i == 2)
                                    TimeWait = 120;
                                else
                                { // 若 超過 3 次皆失敗 則 直接送出錯誤訊息 結束程式
                                    Trace.DebugWrite("EU2010.Common.SQLHelperConsole", "Execute SqlCommand Error(網路連線問題)Retry(" + i + "):" + " ErrorNumber=" + e.Number + "  " + e.ToString() + "\r\n SQL = " + cmdText);
                                    throw e;
                                }

                                System.Threading.Thread.Sleep(1000 * TimeWait);
                            }
                            else
                            {
                                // 其它問題
                                Trace.DebugWrite("EU2010.Common.SQLHelperConsole", "Execute SqlCommand Error:" + e.ToString() + "\r\n SQL = " + cmdText);
                                throw e;
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return 0;
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, out SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 0; //

            trans = conn.BeginTransaction();
            cmd.Transaction = trans;

            cmd.CommandType = cmdType;
            cmd.Parameters.Clear();
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    if (parm != null)
                        cmd.Parameters.Add(parm);
                }
            }
        }
        private static bool CheckErrorCanRetry(System.Data.SqlClient.SqlException e)
        {
            bool CanRetry = false;
            string TempMsg = e.ToString().ToLower();
            if (e.Number == 11) //general network error
                CanRetry = true;
            else if (e.Number == 17) //sql server does not exist or access denied
                CanRetry = true;
            else if (e.Number == -2) //timeout expired
                CanRetry = true;
            else if (e.Number == 1205) //deadlocked on lock
                CanRetry = true;
            else if (TempMsg.IndexOf("逾時") > 0)
                CanRetry = true;
            else if (TempMsg.IndexOf("timeout expired") > 0)
                CanRetry = true;
            else if (TempMsg.IndexOf("一般性網路") > 0)
                CanRetry = true;
            else if (TempMsg.IndexOf("一般網路") > 0)
                CanRetry = true;
            else if (TempMsg.IndexOf("general network error") > 0)
                CanRetry = true;
            else if (TempMsg.IndexOf("不存在或拒絕存取") > 0)
                CanRetry = true;
            else if (TempMsg.IndexOf("sql server does not exist") > 0)
                CanRetry = true;
            return CanRetry;
        }
    }
}
