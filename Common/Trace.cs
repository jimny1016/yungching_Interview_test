using System;
using System.Diagnostics;
using System.Threading;

namespace Common
{
    public class Trace
    {

        /// <summary>
        /// 
        /// </summary>
        public Trace()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        /// <summary>
        /// LogFilePath
        /// </summary>
        static private string LogFilePath = "";
        private static Mutex mut;
        private static DateTime errorDateTime = System.DateTime.Now.AddDays(-1);

        /// <summary>
        /// 設定 Log 檔名與路徑
        /// </summary>
        /// <param name="File_Path"></param>
        /// <param name="ApName"></param>
        static public void SetLogFile(string File_Path, string ApName)
        {

            try
            {
                mut = new Mutex(false, ApName);
                LogFilePath = File_Path + ApName;
            }
            catch (Exception ee)
            {
                string none = ee.Source;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Class_Name">類別名稱</param>
        /// <param name="Message">內文</param>
        static public void DebugWrite(string Class_Name, string Message)
        {
            // ***Release Mode 時不顯示
            // [DEBUG] 進出function: 以方便了解程式執行流程
            // [DEBUG] function parameters和return value
            // [DEBUG] 程式流程重點: 如重要判斷             
            try
            {
                mut.WaitOne(5 * 1000, true);

                // 設定 Track
                SetTrackFile();

                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " [" + Class_Name.PadRight(10) + "][DEBUG] " + Message);

                // 清除 Track
                ClearTrack();
            }
            catch (Exception ee)
            {
                WriteEventLog(ee);
            }
            finally
            {
                try
                {
                    mut.ReleaseMutex();
                }
                catch { }
            }
        }        

        static public void WriteEventLog(Exception ee)
        {
            try
            {
                if (errorDateTime.AddMinutes(30) < System.DateTime.Now)
                {
                    errorDateTime = System.DateTime.Now;
                    EventLog.WriteEntry("Billhunter", "來源應用程式:" + AppDomain.CurrentDomain.FriendlyName + " " + ee.ToString(), EventLogEntryType.Error);
                }
            }
            catch { }
        }
        /// <summary>
        /// 
        /// </summary>
        static public void SetTrackFile()
        {
            try
            {
                //**************************************************
                // 設定 Trace 增加 File 輸出
                //************************************************** 
                string path = LogFilePath + "." + DateTime.Now.ToString("yyyy-MM-dd") + ".log";

                System.IO.StreamWriter write = new System.IO.StreamWriter(path, true, System.Text.Encoding.GetEncoding("big5"));
                System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.TextWriterTraceListener(write, "log"));

                //**************************************************
                // 設定 Trace 增加 Console 輸出
                //************************************************** 
                //System.Diagnostics.Trace.Listeners.Add(
                //	new System.Diagnostics.TextWriterTraceListener(Console.Out,"console"));

                System.Diagnostics.Trace.AutoFlush = true;
                System.Diagnostics.Debug.AutoFlush = true;

            }
            catch (Exception ee)
            {
                string none = ee.Source;
                //Console.WriteLine(ee.ToString());
                //using(System.IO.StreamWriter write = new System.IO.StreamWriter(@"c:\commom.log",true,System.Text.Encoding.GetEncoding("big5")))
                //{
                //	write.WriteLine("SetTrackFile:" + ee.ToString());
                //	write.Close();
                //}
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static public void ClearTrack()
        {
            try
            {
                //**************************************************
                // 清除 Trace 
                //************************************************** 
                System.Diagnostics.Trace.Listeners["log"].Flush();
                System.Diagnostics.Trace.Listeners["log"].Close();
                System.Diagnostics.Trace.Listeners.Remove("log");
            }
            catch (Exception ee)
            {
                string none = ee.Source;
            }
        }
    }
}
