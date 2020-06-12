using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkDayDotNetGrpc.Protos;

namespace WorkDayDotNetGrpc.Services
{
    public class WorkDayService : WorkDay.WorkDayBase
    {
        private readonly ILogger<WorkDayService> _logger;
        public WorkDayService(ILogger<WorkDayService> logger)
        {
            _logger = logger;
        }

        public override Task<DateTimeReply> GetCurrWorkDay(EmptyRequest request, ServerCallContext context)
        {
            return Task.FromResult(new DateTimeReply
            {
                Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTimeHelper.GetCurrWorkDay().ToUniversalTime())
            });
        }
        public override Task<DateTimeReply> GetWorkDay(DateTimeRequest request, ServerCallContext context)
        {
            return Task.FromResult(new DateTimeReply
            {
                Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTimeHelper.GetWorkDay(request.Date.ToDateTime().ToUniversalTime()))
            });
        }
    }
}
