using Bugsnag.AspNet.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RestaurantApp.API.DTO;
using RestaurantApp.API.ErrorLogging;
using RestaurantApp.API.Extensions;
using RestaurantApp.API.Jwt;
using RestaurantApp.API.Jwt.TokenStorage;
using RestaurantApp.API.Middleware;
using RestaurantApp.Application;
using RestaurantApp.Application.Logging;
using RestaurantApp.Application.UseCaseHandling;
using RestaurantApp.Application.UseCases.Commands;
using RestaurantApp.Application.UseCases.Queries;
using RestaurantApp.DataAccess;
using RestaurantApp.Implementation.Logging;
using RestaurantApp.Implementation.UseCases.Commands;
using RestaurantApp.Implementation.UseCases.Queries;
using RestaurantApp.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.API
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
            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);
            /*
                Transient - Svaki put kada se zatrazi objekat, kreira se novi
                Scoped - Ponovna upotreba na nivou 1 http zahteva
                Singleton - jedan objekat od starta do stop-a aplikacije
                
            */
            services.AddTransient<ITokenStorage, InMemoryTokenStorage>();
            services.AddTransient<JwtManager>(x =>
            {
                var context = x.GetService<RestaurantAppContext>();
                var tokenStorage = x.GetService<ITokenStorage>();
                return new JwtManager(context, appSettings.Jwt.Issuer, appSettings.Jwt.SecretKey, appSettings.Jwt.DurationSeconds, tokenStorage);
            });

            services.AddBugsnag(configuration => {
                configuration.ApiKey = appSettings.BugSnagKey;
            });


            services.AddLogger();
            services.AddValidators();

            services.AddTransient<RestaurantAppContext>(x =>
            {
                DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
                builder.UseSqlServer("Data Source=./SQLEXPRESS; Initial Catalog = RestaurantApp; Integrated Security = true");
                return new RestaurantAppContext(builder.Options);
            });

            services.AddTransient<QueryHandler>();

            services.AddHttpContextAccessor();
            services.AddScoped<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                var data = header.ToString().Split("Bearer ");

                if (data.Length < 2)
                {
                    return new UnauthorizedActor();
                }

                var handler = new JwtSecurityTokenHandler();

                var tokenObj = handler.ReadJwtToken(data[1].ToString());

                var claims = tokenObj.Claims;

                var id = claims.First(x => x.Type == "Id").Value;
                var username = claims.First(x => x.Type == "Username").Value;
                var useCases = claims.First(x => x.Type == "UseCases").Value;

                List<int> useCaseIds = JsonConvert.DeserializeObject<List<int>>(useCases);

                return new JwtActor
                {
                    AllowedUseCases = useCaseIds,
                    Id = int.Parse(id),
                    Username = username,
                };
            });

            //Queries and Commands
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<ISearchTablesQuery, EfSearchTablesQuery>();
            services.AddTransient<ISearchReservationsQuery ,EfSearchReservationsQuery>();
            services.AddTransient<ICreateReservationCommand, EfCreateReservationCommand>();
            services.AddTransient<ISearchOrderQuery, EfSearchOrderQuery>();
            services.AddTransient<ICreateOrderCommand, EfNewOrderCommand>();
            services.AddTransient<ICompleteOrderCommand, EfCompleteOrderCommand>();
            services.AddTransient<IServiceOrderCommand, EfCreateServiceOrderCommand>();
            services.AddTransient<ICreateMenuItemCommand, EfCreateMenuItemCommand>();
            services.AddTransient<ISearchMenuItemsQuery, EfSearchMenuItemsQuery>();
            services.AddTransient<IGetMenuItemQuery, EfGetMenuItemQuery>();
            services.AddTransient<IUpdateMenuItemCommand, EfUpdateMenuItemCommand>();
            services.AddTransient<IDeleteMenuItemCommand, EfDeleteMenuItemCommand>();
            services.AddTransient<IGetTableQuery,EfGetTableQuery>();
            services.AddTransient<IDeleteTableCommand, EfDeleteTableCommand>();
            services.AddTransient<IUpdateTableCommand, EfUpdateTableCommand>();
            services.AddTransient<ISearchCategoryQuery, EfSearchCategoryQuery>();
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<IGetCategoryQuery, EfGetCategoryQuery>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
            services.AddTransient<IUpdateCategoryCommand, EfUpdateCategoryCommand>();
            services.AddTransient<ISearchLogQuery, EfSearchLogQuery>();
            services.AddTransient<IDeleteReservationCommand, EfDeleteReservationCommand>();
            services.AddTransient<IGetReservationsQuery, EfGetReservationQuery>();
            services.AddTransient<IDeleteOrderCommand, EfDeleteOrderCommand>();
            services.AddTransient<IUpdateReservationCommand, EfUpdateReservationCommand>();
            services.AddTransient<IGetOrderQuery, EfGetOrderQuery>();
            services.AddTransient<ICreateTableCommand, EfCreateTableCommand>();
            services.AddTransient<IUpdateOrderCommand, EfUpdateOrderCommand>();
            services.AddTransient<ICloseTableCommand, EfCloseTableCommand>();
            services.AddTransient<IRemoveOrderCommand, EfRemoveOrderCommand>();

            services.AddTransient<IRegisterUserCommand>(x =>
            {
                var ctx = x.GetService<RestaurantAppContext>();
                var validator = x.GetService<RegisterUserValidator>();

                return new EfRegisterUserCommand(ctx, validator);
            });

            services.AddTransient<RestaurantAppContext>();
            services.AddTransient<IErrorLogger, ConsoleErrorLogger>();
            services.AddTransient<IUseCaseLogger, EfUseCaseLogger>();


            services.AddTransient<ICommandHandler, CommandHandler>();

            //Validator Registracija
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<CreateTableValidator>();
            services.AddTransient<CreateReservationValidator>();
            services.AddTransient<NewOrderValidator>();
            services.AddTransient<CompleteOrderValidator>();
            services.AddTransient<CreateServiceOrderValidator>();
            services.AddTransient<CreateMenuItemValidator>();
            services.AddTransient<UpdateMenuItemValidator>();
            services.AddTransient<UpdateTableValidator>();
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<UpdateReservationValidator>();
            services.AddTransient<UpdateOrderValidator>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestaurantApp.API", Version = "v1" });
            });

            services.AddJwt(appSettings);


            services.AddTransient<IQueryHandler>(x =>
            {
                var actor = x.GetService<IApplicationActor>();
                var logger = x.GetService<IUseCaseLogger>();
                var queryHandler = new QueryHandler();
                var timeTrackingHandler = new TimeTrackingQueryHandler(queryHandler);
                var loggingHandler = new LoggingQueryHandler(timeTrackingHandler, actor, logger);
                var decoration = new AuthorizationQueryHandler(actor, loggingHandler);

                return decoration;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantApp.API v1"));
            }

            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
