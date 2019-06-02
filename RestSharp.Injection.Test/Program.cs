using System;
using System.Reflection;
using Autofac;
using Autofac.Core;
using RestSharp.Injection.Test;

namespace RestSharp.Injection.Test
{
	/// <summary>
	/// 此处测试的是Autofac注入 using Autofac;
	/// Ninject的未做测试
	/// </summary>
	public class Program
	{
		public static IContainer Container { get; private set; }
		public static IContainer ConfigureIoCc()
		{
			var builder = new ContainerBuilder();
			builder.RegisterInstance<Func<string, IRestClient>>(url => new RestClient(url));
			builder.RegisterInstance<Func<string, Method, IRestRequest>>((resource, method) => new RestRequest(resource, method));
			builder.RegisterType<RestSharpFactory>().As<IRestSharpFactory>();
			builder.RegisterType<TestClass>().As<ITestClass>();
			builder.RegisterType<TestClass>().AsSelf();

			#region // 用法很丰富可以看官网
			/*
			//builder.RegisterType<TestClass>().As(..).As(..).As<ITestClass>();
			//触发事件
			_builder.RegisterType<TestClass>()
				.OnRegistered(e =>
				{
					Console.WriteLine(e);
				});
			//_builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
			//	.Where(t => t.Name.StartsWith("Test"))
			//	.AsImplementedInterfaces();

			static void Main(string[] args)
			{
				var services = new ServiceCollection();
				services.AddHttpClient();
				var builder = new ContainerBuilder();
				// exclicit resolving client for constructor
				builder.RegisterType<MyHttpClient>().As<IMyHttpClient>().WithParameter(
					(p, ctx) => p.ParameterType == typeof(HttpClient),
					(p, ctx) => ctx.Resolve<IHttpClientFactory>().CreateClient());

				builder.Populate(services);
				var provider = builder.Build();
				// ============== This works
				provider.Resolve<IMyHttpClient>();

				// ============== This works too
				provider.Resolve<IEnumerable<IMyHttpClient>>();
			}
			*/
			#endregion
			Container = builder.Build();
			return Container;
		}

		static void Main(string[] args)
		{

			Console.WriteLine("开始调用类库测试:TestEntry.Main");
			var container = ConfigureIoCc();
			var testService = container.Resolve<ITestClass>();
			testService.Hello();

			var testSelfRegHello = container.Resolve<TestClass>();
			testSelfRegHello.SelfRegHello();

			for(int i =0; i <= 10; i++)
			{
				testService.TetsBegin();
			}
			Console.ReadLine();
		}
	}
}
