using System;

using Ninject.Modules;

namespace RestSharp.Injection.Ninject
{
	/// <summary>
	///    Provides Ninject registrations for the public types in RestSharp.Injection.
	/// </summary>
	public class RestSharpModule : NinjectModule
	{
		/// <summary>
		///    Loads the module into the kernel.
		/// </summary>
		public override void Load()
		{
			Bind<Func<string, IRestClient>>()
				.ToConstant(new Func<string, IRestClient>(url => new RestClient(url)));

			Bind<Func<string, Method, IRestRequest>>()
				.ToConstant(new Func<string, Method, IRestRequest>((url, method) => new RestRequest(url, method)));

			Bind<IRestSharpFactory>().To<RestSharpFactory>();
		}
	}
}