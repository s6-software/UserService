using API.Models.User;
using API.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Configuration;
using System.Text.Json;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            
            
            
            var firebaseSettings = builder.Configuration.GetSection("Firebase");
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(JsonSerializer.Serialize(
                    new
                    {
                        type = firebaseSettings["type"],
                        project_id = firebaseSettings["projectId"],
                        private_key_id = firebaseSettings["private_key_id"],
                        private_key = firebaseSettings["private_key"],
                        client_email = firebaseSettings["client_email"],
                        client_id = firebaseSettings["client_id"],
                        auth_uri = firebaseSettings["auth_uri"],
                        token_uri = firebaseSettings["token_uri"],
                        auth_provider_x509_cert_url = firebaseSettings["auth_provider_x509_cert_url"],
                        client_x509_cert_url = firebaseSettings["client_x509_cert_url"],
                        universe_domain = firebaseSettings["universe_domain"]
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
                httpClient.BaseAddress = new Uri(firebaseSettings["GOOGLE_APIS_JWT"]);
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