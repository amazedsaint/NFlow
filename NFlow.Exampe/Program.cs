using System;
using System.Linq;
using System.Text;
using Flow.Access;
using Grpc.Core;
using NFlow.Grpc;
using static Flow.Access.AccessAPI;

namespace NFlow.Exampe
{
    class Program
    {

        static AccessAPIClient client;


        static void Main(string[] args)
        {


            var url = "127.0.0.1:3569";
            var account = "f8d6e0586b0a20c7";


            Channel channel = new Channel(url, ChannelCredentials.Insecure);
            client = new AccessAPIClient(channel);
            Console.WriteLine("\n--------------------------");

            //Let us do a ping and hopefully with out errors
            Console.WriteLine("Ping Example");
            var pingResponse = Ping();
            Console.WriteLine(pingResponse.ToString());
            Console.WriteLine("\n--------------------------");


            //Shows how to fetch an account
            Console.WriteLine("Get account response");
            var accountResponse = GetAccount(account);
            var accountId = accountResponse.Account.Address.FromByteStringToHex();
            Console.Write(accountId);
            Console.WriteLine("\n--------------------------");


            //Shows how to execute a script
            Console.WriteLine("Execute a script");
            var result=ExecuteScript("pub fun main(): Int { return 1 }");
            var payload = result.Value.FromByteStringToString();
            Console.Write(payload);
            Console.WriteLine("\n--------------------------");

            Console.Read();

        }


        public static PingResponse Ping()
        {
            var req = new PingRequest();
            return client.Ping(req);
        }


        public static GetAccountResponse GetAccount(string address)
        {

            var req = new GetAccountRequest() { Address = address.FromHexToByteString() };
            return client.GetAccount(req);
        }


        public static ExecuteScriptResponse ExecuteScript(string script, string[] arguments = null)
        {

            var req = new ExecuteScriptAtLatestBlockRequest() { Script = script.FromStringToByteSring() };
            return client.ExecuteScriptAtLatestBlock(req);
        }

    }
}
