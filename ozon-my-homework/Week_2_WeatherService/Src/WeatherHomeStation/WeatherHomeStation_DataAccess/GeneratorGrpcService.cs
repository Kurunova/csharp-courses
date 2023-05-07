using Grpc.Net.Client;
using WeatherStationEmulator.GrpcServices;

namespace WeatherHomeStation_DataAccess;

public class GeneratorGrpcService
{
	public void Get()
	{
		try
		{
			using (var channel = GrpcChannel.ForAddress("https://localhost:50001"
				       //, new GrpcChannelOptions(){ HttpHandler = new GrpsWebHandler( new HttpClientHandler()) }
				       , new GrpcChannelOptions(){ HttpHandler = new HttpClientHandler() }
			       ))
			{
				var client = new Generator.GeneratorClient(channel);
				var response = client.GetState(new GetStateRequest() { Id = { 1 } });
				var result = response.Result;
			
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}

	}

}