using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfDateServiceLib
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IDateService
    {
        [OperationContract]
        DateTime FetchDateTime();

        [OperationContract]
        DateTime FetchCurrWorkDay(DateTime date);

        [OperationContract]
        DateTime FetchLastWorkday(DateTime date);

        [OperationContract]
        DateTime FetchLast2Workday(DateTime date);

        [OperationContract]
        DateTime FetchLast3Workday(DateTime date);

        [OperationContract]
        DateTime FetchNextWorkday(DateTime date);

        [OperationContract]
        DateTime FetchNext2Workday(DateTime date);

        [OperationContract]
        DateTime FetchNext3Workday(DateTime date);

        [OperationContract]
        DateTime FetchPointWorkday(DateTime date, int day);

        [OperationContract]
        DateTime FetchPreCfmDate(DateTime appTime);
    }
}
