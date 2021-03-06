﻿using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.GraprhQlTypes.Customer;
using Orders.GraprhQlTypes.Order;
using Orders.Schema;
using Orders.Services;

namespace GRAPHQL_.NET_API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddCors(options =>
			{
				options.AddPolicy("AllowMyOrigin",
					builder => builder.WithOrigins("http://localhost").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddSingleton<IOrderService, OrderService>();
			services.AddSingleton<ICustomerService, CustomerService>();
			services.AddSingleton<IOrderEventService, OrderEventService>();
			services.AddSingleton<ISchema, Scheme>();
			services.AddSingleton<OrderType>();
			services.AddSingleton<CustomerType>();
			services.AddSingleton<OrderStatusesEnum>();
			services.AddSingleton<OrdersQuery>();
			services.AddSingleton<Scheme>();
			services.AddSingleton<OrderCreateInputType>();
			services.AddSingleton<OrdersMutation>();
			services.AddSingleton<OrdersSubscription>();
			services.AddSingleton<OrderEventType>();
			services.AddSingleton<IDependencyResolver>(c => new FuncDependencyResolver(type => c.GetRequiredService(type)));
			services.AddGraphQL().AddWebSockets();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseWebSockets();
			app.UseGraphQLWebSockets<Scheme>();
			app.UseGraphQL<Scheme>();
			app.UseMvc();
			app.UseCors("AllowMyOrigin");
		}
	}
}
