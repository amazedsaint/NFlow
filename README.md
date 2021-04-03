# NFlow
This is a simple GRPC library generated from Protobuf definitions to interact with Flow Blockchain (https://www.onflow.org) from C# & Unity, with some basic examples and extensions methods.


## Quick Start

To test the code, please make sure your local flow emulator is running. Please see here (https://docs.onflow.org/emulator) for more on how to install the Flow CLI. Once you've Flow CLI, just start the emulator as below


```
 flow init
 flow emulator start
```

See the NFlow Example project for some basic usecases. You can create a client by providing the URL to create a Channel 

```
 //Please make sure your local flow emulator is running

  var url = "127.0.0.1:3569";

  Channel channel = new Channel(url, ChannelCredentials.Insecure);
  client = new AccessAPIClient(channel);


```

Once you initiate the client, you can invoke pretty much any method using the client object as below.

## Ping

Try doing a ping 

```
//Let us do a ping and hopefully with out errors

   //Invoking GRPC Method 
   public static PingResponse Ping()
    {
        var req = new PingRequest();
        return client.Ping(req);  
    }
    
        
   // Somewhere in Main
    var pingResponse = Ping();
    Console.WriteLine(pingResponse.ToString());
    
    
```


## Fetching account details

We've to properly convert the Flow address to a protobuf ByteString, so I wrote a handy FromHexToByteString extension method for the same. This is same as Pack(*H) in Python, Ruby etc. Likewise, we've to Unpack the protobuf byte string back to the Hex address as well

```
 
 //Shows how to fetch an account
 

    //Invoking GRPC method
    public static GetAccountResponse GetAccount(string address)
    {
        var req = new GetAccountRequest() { Address = "f8d6e0586b0a20c7".FromHexToByteString() };
        return client.GetAccount(req);
    }
    
     //Somewhere in Main
    var accountResponse = GetAccount(account);
    var accountId = accountResponse.Account.Address.FromByteStringToHex();
    Console.Write(accountId); 
    //writes f8d6e0586b0a20c7 to the console
  

```

## Executing a Cadence script

Now let us execute a Cadence script using C#. It is pretty straight forward. Again, I added a handy extension to convert the script string to protobuf Bytestring. 


```
//Executing a script
    
    
    //Invoking GRPC method
    
    public static ExecuteScriptResponse ExecuteScript(string script, string[] arguments = null)
        {
            var req = new ExecuteScriptAtLatestBlockRequest() { Script = script.FromStringToByteSring() };
            return client.ExecuteScriptAtLatestBlock(req);
        }

    //Somewhere in main
     
        var result=ExecuteScript("pub fun main(): Int { return 1 }");
        var payload = result.Value.FromByteStringToString();
        Console.Write(payload);

```

This will output the JSON payload to the console, you can easily parse the result using a JSON parser.

```
{"type":"Int","value":"1"}
```

 
