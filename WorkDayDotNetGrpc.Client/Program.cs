using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using WorkDayDotNetGrpc.Protos;

namespace WorkDayDotNetGrpc.Client
{
    class Program
    {
        static ILogger logger;
        static ILoggerFactory loggerFactory;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Default", LogLevel.Debug)
                    .AddConsole();
            });
            logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("info");

            //Http2Call();
            GrpcCall();

            Console.ReadLine();
        }

        public static void Http2Call()
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            var httpClientHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://10.228.130.225:9030");
            requestMessage.Version = Version.Parse("2.0");
            try
            {
                var responseTask = httpClient.SendAsync(requestMessage);
                responseTask.Wait();
                var readTask = responseTask.Result.Content.ReadAsStringAsync();
                readTask.Wait();
                Console.WriteLine(readTask.Result);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
            }
        }

        public static void GrpcCall()
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            var httpClientHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);

            //var channel = GrpcChannel.ForAddress("https://10.228.130.225:9030", new GrpcChannelOptions() { HttpClient = httpClient, Credentials = ChannelCredentials.Insecure, LoggerFactory = loggerFactory });
            //var channel = GrpcChannel.ForAddress("https://10.228.130.225:9030", new GrpcChannelOptions() { HttpClient = httpClient, LoggerFactory = loggerFactory });
            //var channel = GrpcChannel.ForAddress("http://10.228.130.225:9030", new GrpcChannelOptions() { Credentials = ChannelCredentials.Insecure, LoggerFactory = loggerFactory });
            var channel = GrpcChannel.ForAddress("http://10.228.130.225:9030", new GrpcChannelOptions() { LoggerFactory = loggerFactory });

            var workDayClient = new WorkDay.WorkDayClient(channel);
            var helloClient = new Greeter.GreeterClient(channel);

            try
            {
                //var helloTask = helloClient.SayHelloAsync(new HelloRequest() { Name = "HD" });
                //helloTask.ResponseAsync.Wait();
                //Console.WriteLine(helloTask.ResponseAsync.Result.Message);
                CallOptions callOptions = new CallOptions()
                {
                };

                int i = 0;
                while (i < 3)
                {
                    var replayTask = workDayClient.GetCurrWorkDayAsync(new EmptyRequest(), callOptions);

                    replayTask.ResponseAsync.Wait();

                    Console.WriteLine(replayTask.ResponseAsync.Result.Date.ToDateTime().ToString());

                    i++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
