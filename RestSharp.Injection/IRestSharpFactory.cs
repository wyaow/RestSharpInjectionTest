namespace RestSharp.Injection
{
	/// <summary>
	///    Provides a factory for RestSharp service creation.
	/// </summary>
	public interface IRestSharpFactory
	{
		/// <summary>
		///    Creates the client.
		/// </summary>
		/// <param name="url">The URL.</param>
		IRestClient CreateClient(string url);

		/// <summary>
		///    Creates the request.
		///    还可以重载其他接口方法
		/// </summary>
		/// <param name="resource">The resource.</param>
		/// <param name="method">The method.</param>
		IRestRequest CreateRequest(string resource, Method method);
	}
}