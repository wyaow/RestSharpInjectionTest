using System;

using Autofac;

namespace RestSharp.Injection.Autofac
{
	/// <summary>
	/// Provides Autofac registrations for the public types in RestSharp.Injection.
	/// </summary>
	public class RestSharpModule : Module
	{
		/// <summary>
		///    Adds RestSharp.Injection-based registrations to the container.
		/// </summary>
		/// <param name="builder">
		///    The builder through which components can be registered.
		/// </param>
		/// <remarks>
		///    Note that the ContainerBuilder parameter is unique to this module.
		/// </remarks>
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterInstance<Func<string, IRestClient>>(url => new RestClient(url));
			builder.RegisterInstance<Func<string, Method, IRestRequest>>((resource, method) => new RestRequest(resource, method));
			builder.RegisterType<RestSharpFactory>().As<IRestSharpFactory>();
		}
	}
}