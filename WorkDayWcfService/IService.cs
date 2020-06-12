using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WorkDayWcfService
{
    [ServiceContract(SessionMode = SessionMode.NotAllowed)]
    interface IService
    {
        [OperationContract]
        [WebGet(UriTemplate = "DateTime", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetDateTime();

        //[OperationContract]
        //[WebGet(UriTemplate = "CurrWorkDay", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //string GetCurrWorkDay();

        [OperationContract]
        [WebGet(UriTemplate = "CurrWorkDayAsync", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<string> GetCurrWorkDayAsync();

        [OperationContract]
        [WebGet(UriTemplate = "WorkDay?date={date}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetWorkDay(DateTime date);

        [OperationContract]
        [WebGet(UriTemplate = "LastWorkDay?date={date}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetLastWorkday(DateTime date);

        [OperationContract]
        [WebGet(UriTemplate = "NextWorkDay?date={date}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetNextWorkday(DateTime date);

        [OperationContract]
        [WebGet(UriTemplate = "PointWorkDay?date={date}&day={day}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetPointWorkday(DateTime date, int day);

        [OperationContract]
        [WebGet(UriTemplate = "TestForThrottle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<string> TestForThrottle();
    }
}
