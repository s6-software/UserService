using API.Models.User;
using API.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text.Json;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(JsonSerializer.Serialize(
                    new
                    {
                        type = builder.Configuration["type"],
                        project_id = builder.Configuration["projectId"],
                        private_key_id = builder.Configuration["private_key_id"],
                        private_key = builder.Configuration["private_key"],
                        client_email = builder.Configuration["client_email"],
                        client_id = builder.Configuration["client_id"],
                        auth_uri = builder.Configuration["auth_uri"],
                        token_uri = builder.Configuration["token_uri"],
                        auth_provider_x509_cert_url = builder.Configuration["auth_provider_x509_cert_url"],
                        client_x509_cert_url = builder.Configuration["client_x509_cert_url"],
                        universe_domain = builder.Configuration["universe_domain"]
                    }
                )
                )
            });

            bool useInMemoryDatabase =  builder.Configuration.GetValue<bool>("In_Memory");

            if (useInMemoryDatabase)
            {
                builder.Services.AddDbContext<UserContext>(opt =>
               opt.UseInMemoryDatabase(databaseName: "memoryDB"));
            }
            else
            {
                builder.Services.AddDbContext<UserContext>(opt =>
               opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); ;
            }
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddHttpClient<IJwtProvider, JwtProvider>((sp, httpClient ) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                httpClient.BaseAddress = new Uri(config["GOOGLE_APIS_JWT"]);
            });
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}