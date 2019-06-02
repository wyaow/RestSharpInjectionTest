using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharp.Injection.Test
{
	public interface ITestClass
	{
		void Hello();
		void TetsBegin();
	}

	public class TestClass : ITestClass
	{
		/// <summary>
		/// 这个IContainer不在里面,Autofac中有个LiftSpanm的估计可以取得IContainer
		/// </summary>
		public IContainer _container { get; private set; }

		// 容器中有ILifetimeScope + 其余我们注入的
		public readonly ILifetimeScope _scope;

		//这个在容器中有就会自动注入
		private readonly IRestSharpFactory _c;


		/// <summary>
		/// 构造注入: ContainerBuilder
		/// </summary>
		/// <param name="builder"></param>
		public TestClass(IRestSharpFactory c, ILifetimeScope scope)
		{
			_c = c;
			_scope = scope;
		}

		public void Hello()
		{
			Console.WriteLine("Hello TestClass");
		}

		public void SelfRegHello()
		{
			Console.WriteLine("SelfRegHello TestClass");
		}

		public void TetsBegin()
		{
			//这样也可以, _scope自动注入的
			var t =_scope.Resolve<IRestSharpFactory>();
			//_scope.TryResolve();

			_container = Program.Container;
			var restSharpFactry = _container.Resolve<IRestSharpFactory>();
			var client = restSharpFactry.CreateClient("http://localhost:44382/api/ToDo");   //IRestClient, 接受url参数,返回一个委托类型, 委托类型返回值是IRestClient
			var request = restSharpFactry.CreateRequest("", Method.GET);                    //IRestRequest

			//可以restSharpFactry.CreateRequest(1, 2) 可以重载成无参数的, 就可以指定
			//request.Method = Method.GET;
			request.RequestFormat = DataFormat.Json;

			//测试同步
			var response = client.GetAsync<TodoClass>(request);

			//测试异步
			var asyncHandle = client.GetAsync<TodoClass>(request, (resp, callback) =>
			{
				Console.WriteLine(resp.Content);
				//最好还是这样,不然会端口占用过高,长久不释放 netstat -an|findstr 44382
				callback.Abort();
			});
		}
	}
	public class TodoClass
	{
		public int id { get; set; }
		public string name { get; set; }
		public bool isComplete { get; set; }
	}

}
