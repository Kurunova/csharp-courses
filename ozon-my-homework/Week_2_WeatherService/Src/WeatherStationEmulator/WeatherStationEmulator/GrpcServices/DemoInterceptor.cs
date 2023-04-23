namespace WeatherStationEmulator.GrpcInterceptors;

// public class DemoInterceptor : Interceptor
// {
// 	private readonly ILogger<DemoInterceptor> _logger;
//
// 	public DemoInterceptor(ILogger<DemoInterceptor> logger)
// 	{
// 		_logger = logger;
// 	}
//     
// 	public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
// 		TRequest request,
// 		ServerCallContext context,
// 		UnaryServerMethod<TRequest, TResponse> continuation)
// 	{
// 		_logger.LogInformation("call DemoInterceptor");
//         
// 		return base.UnaryServerHandler(request, context, continuation);
// 	}
// }