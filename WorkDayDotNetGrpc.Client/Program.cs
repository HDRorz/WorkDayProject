using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Net.Http;
using WorkDayDotNetGrpc.Protos;

namespace WorkDayDotNetGrpc.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            GrpcCall();
        }

        public static void GrpcCall()
        {
            var httpClientHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpClientHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);

            var channel = GrpcChannel.ForAddress("https://10.228.130.225:9007", new GrpcChannelOptions() { HttpClient = httpClient });
            //var channel = GrpcChannel.ForAddress("https://10.228.130.225:9007");


            var workDayClient = new WorkDay.WorkDayClient(channel);

            try
            {
                var replayTask = workDayClient.GetCurrWorkDayAsync(new EmptyRequest());

                replayTask.ResponseAsync.Wait();

                Console.WriteLine(replayTask.ResponseAsync.Result.Date.ToDateTime().ToString());
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
