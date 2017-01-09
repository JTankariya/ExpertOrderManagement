using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Admin.BusinessLogic
{
    public class ExceptionLogger
    {
        private ExceptionParameters _exceptionParamaters;

        public void LogException()
        {

            SqlParameter[] parameters = BuildSqlParameters(_exceptionParamaters);
            //DBHelper.ExecuteNonQuery("LogException", parameters);
        }

        private SqlParameter[] BuildSqlParameters(ExceptionParameters exParameters)
        {
            SqlParameter[] _params = new SqlParameter[10];

            _params[0] = new SqlParameter("@ErrorId", exParameters.ErrorId);
            _params[1] = new SqlParameter("@Application", exParameters.Application);
            _params[2] = new SqlParameter("@Host", exParameters.Host);
            _params[3] = new SqlParameter("@Type", exParameters.Type);
            _params[4] = new SqlParameter("@Source", exParameters.Source);
            _params[5] = new SqlParameter("@Message", exParameters.Message);
            _params[6] = new SqlParameter("@User", exParameters.User);
            _params[7] = new SqlParameter("@StatusCode", exParameters.StatusCode);
            _params[8] = new SqlParameter("@TimeUTC", exParameters.TimeUtc);
            _params[9] = new SqlParameter("@AllXml", exParameters.AllXml);
   
            return _params;
        }

        public ExceptionLogger(Exception ex)
        {
            _exceptionParamaters = new ExceptionParameters(ex);
        }
    }

    public class ExceptionParameters
    {
        public string ErrorId { get; set; }
        public string Application { get; set; }
        public string Host { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public int StatusCode { get; set; }
        public DateTime TimeUtc { get; set; }
        public string AllXml { get; set; }

        public ExceptionParameters(Exception e)
        {
            ErrorId = Guid.NewGuid().ToString();
            Application = System.AppDomain.CurrentDomain.FriendlyName;
            Host = System.Environment.MachineName.ToString();
            Type = e.GetType().ToString();
            Source = e.Source.ToString();
            Message = e.Message.ToString();
            User = "";
            StatusCode = 500;
            TimeUtc = DateTime.Now;
            AllXml = e.StackTrace;
        }
    }
}
