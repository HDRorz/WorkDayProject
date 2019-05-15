using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WorkDayWcfService
{
    [ServiceContract]
    interface IService
    {
        [OperationContract]
        [WebGet(UriTemplate = "DateTime", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DateTime GetDateTime();

        [OperationContract]
        [WebGet(UriTemplate = "CurrWorkDay", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DateTime GetCurrWorkDay();

        [OperationContract]
        [WebGet(UriTemplate = "WorkDay?date={date}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DateTime GetWorkDay(DateTime date);

        [OperationContract]
        [WebGet(UriTemplate = "LastWorkDay?date={date}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DateTime GetLastWorkday(DateTime date);

        [OperationContract]
        [WebGet(UriTemplate = "Last2WorkDay?date={date}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DateTime GetLast2Workday(DateTime date);

        [OperationContract]
        [WebGet(UriTemplate = "Last3WorkDay?date={date}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DateTime GetLast3Workday(DateTime date);

        [OperationContract]
        [WebGet(UriTemplate = "NextWorkDay?date={date}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DateTime GetNextWorkday(DateTime date);

        [OperationContract]
        [WebGet(UriTemplate = "Next2WorkDay?date={date}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DateTime GetNext2Workday(DateTime date);

        [OperationContract]
        [WebGet(UriTemplate = "Next3WorkDay?date={date}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DateTime GetNext3Workday(DateTime date);

        [OperationContract]
        [WebGet(UriTemplate = "PointWorkDay?date={date}&day={day}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DateTime GetPointWorkday(DateTime date, int day);
    }
}
