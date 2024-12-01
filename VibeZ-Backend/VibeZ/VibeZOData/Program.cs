
using BusinessObjects;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories.IRepository;
using Repositories.Repository;
using System.Text;
using VibeZOData.Models;
using VibeZOData.Services.Blob;
using VibeZOData.Services.ElasticSearch;
using Microsoft.AspNetCore.Authentication.Google;
using VibeZOData.Services.Email;
using VibeZOData.Services.DailyTrackListenerService;
namespace VibeZOData
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;

            builder.Services.AddDbContext<VibeZDbContext>(options =>
                                     options.UseSqlServer(builder.Configuration.GetConnectionString("VibeZDB")));
            builder.Services.AddScoped<ITrackRepository, TrackRepository>();
            builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
            builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
            builder.Services.AddScoped<IBlockedArtistRepository, BlockedArtistRepository>();
            builder.Services.AddScoped<ITracksPlaylistRepository, TrackPlaylistRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();
            builder.Services.AddScoped<ILibrary_PlaylistRepository, Library_PlaylistRepository>();
            builder.Services.AddScoped<ILibrary_AlbumRepository, Library_AlbumRepository>();
            builder.Services.AddScoped<ILibrary_ArtistRepository, Library_ArtistRepository>();
            builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();
            builder.Services.AddScoped<IPackageRepository, PackageRepository>();
            builder.Services.AddScoped<IU_PackageRepository, U_PackageRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddHostedService<DailyService>(); // Đăng ký Background Service
            builder.Services.AddScoped<IArtistDashboarRepository,ArtistDashboardRepository>();
            builder.Services.AddScoped<IFollowRepository, FollowRepository>();
            builder.Services.AddScoped<IArtistPendingRepository, ArtistPendingRepository>();








            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddControllers().AddOData(
                opt => opt.Select().Filter().Count().OrderBy().SetMaxTop(null).Expand().AddRouteComponents("odata", EdmModelBuilder.GetEdmModel()));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.AddStorage();
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.AddElasticSearch();


            // Add services to the container.
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
                };
            })
             .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
              {
                  options.ClientId = configuration["GoogleKeys:ClientId"];
                  options.ClientSecret = configuration["GoogleKeys:ClientSecret"];
              }); 
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173", "https://localhost:7241")
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });
            builder.Services.AddMemoryCache();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseODataBatching();
            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
