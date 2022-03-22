using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Server.Logic;
using Server.Communication.Hubs;
using Server.Logic.Masters.Match;
using Server.Logic.Masters.Room;
using Server.Logic.Services;
using Server.Persistence.DatabaseSettings;
using Server.Persistence.Models;
using Server.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SignalR;

namespace Server
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDbSettings>(Configuration.GetSection("TrashQuizDatabase"));
            services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            services.AddSingleton<IMatchMaster, MatchMaster>();
            services.AddSingleton<IRoomMaster, RoomMaster>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ILobbyService, LobbyService>();
            services.AddScoped<IRoomService, RoomService>();

            services.AddHttpContextAccessor();

            services.AddCors();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.Cookie.Name = "TrashKviz";
                        options.Cookie.HttpOnly = true;
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                        options.SlidingExpiration = true;
                        
                    });

            services.AddAuthorization();
            services.AddSignalR().AddNewtonsoftJsonProtocol();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.WithOrigins("http://localhost:4200", "http://192.168.100.126:4200");
                builder.AllowCredentials();
            });
            app.UseWebSockets();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/InitQuestions", async context =>
                //{

                //    var repository = new BaseRepository<Question>(new MongoDbSettings() { ConnectionString = Configuration.GetSection("TrashQuizDatabase")["ConnectionString"], DatabaseName = "TrashQuizDatabase" });
                //    var user = new User { Email = "pera@gmail.com", PassHash = "veomasiguranhesiranpassword", Stats = new Stats(), Username = "Pera" };
                //   // repository.InsertOne(user);
                //   // await context.Response.WriteAsync(JsonConvert.SerializeObject(repository.FilterBy(user => user.Email == "pera@gmail.com")));

                //    var closestNum = new ClosestNumber { Text = "Koji je tačan broj muževa (bivših i sadašnjih) Nataše Bekvalac? ", Answer = 3, Points = 10 };
                //    repository.InsertOne(closestNum);
                //    closestNum = new ClosestNumber { Text = "U koliko rijaliti programa je učestvovao Miki Džuričić iz Kupinova? ", Answer = 11, Points = 10 };
                //    repository.InsertOne(closestNum);
                //    closestNum = new ClosestNumber { Text = "Kolko procenata alkohola je u rakiji od 22 grada? ", Answer = 55, Points = 10 };
                //    repository.InsertOne(closestNum);
                //    closestNum = new ClosestNumber { Text = "Stan od koliko kvadrata je, po Đaniju Ćurčiću, dobio Sergej Trifunović od Danice Drašković? ", Answer = 70, Points = 10 };
                //    repository.InsertOne(closestNum);
                //    closestNum = new ClosestNumber { Text = "Koje godine je Marko Kon sa pesmom \"Cipela\" predstavljao Srbiju u polufinalu Pesme Evrovizije? ", Answer = 2009, Points = 10 };
                //    repository.InsertOne(closestNum);
                //    closestNum = new ClosestNumber { Text = "Koliko je NAJMANJE poena osvojio legendarni Slavoljub u kvizu Slagalica? ", Answer = -5, Points = 10 };
                //    repository.InsertOne(closestNum);
                //    closestNum = new ClosestNumber { Text = "Koliko puta je u numeri \"Jače manijače\" Dina Dvornika izgovoreno \"COME ON\"? ", Answer = 93, Points = 10 };
                //    repository.InsertOne(closestNum);

                //    var choice = new MultipleChoice { Text = "Pravo ime folk izvođača Baje Malog Knindže je:", CorrectAnswer = "Mirko Pajčin", WrongAnswer1 = "Stanko Nedeljković", WrongAnswer2 = "Radiša Trajković", WrongAnswer3 = "Nedeljko Bajić", Points = 10 };
                //    repository.InsertOne(choice);
                //    choice = new MultipleChoice { Text = "Koji građevinski materijal je završio u ustima Ere Ojdanića na jednom od nastupa u Velikom Bratu?", CorrectAnswer = "stiropol", WrongAnswer1 = "šoder", WrongAnswer2 = "staklena vuna", WrongAnswer3 = "pesak", Points = 10 };
                //    repository.InsertOne(choice);
                //    choice = new MultipleChoice { Text = "Po Radiši, učesniku Velikog Brata, gde se kod žena sakuplja histerija?", CorrectAnswer = "u trepavicama", WrongAnswer1 = "u očima", WrongAnswer2 = "na noktima", WrongAnswer3 = "na usnama", Points = 10 };
                //    repository.InsertOne(choice);
                //    choice = new MultipleChoice { Text = "Kako se zove radnik Pošte kog Stojan Vasić zove da se žali na telefonsko uznemiravanje koje traje godinama?", CorrectAnswer = "Žuća", WrongAnswer1 = "Dobrivoje Savić", WrongAnswer2 = "Milivoje Jović", WrongAnswer3 = "Dobrosav", Points = 10 };
                //    repository.InsertOne(choice);
                //    choice = new MultipleChoice { Text = "Šta je jela žena izvesnog gospodina iz klipa \"Šta sam jeo\"?", CorrectAnswer = "prebranac", WrongAnswer1 = "boraniju", WrongAnswer2 = "grašak", WrongAnswer3 = "pilav", Points = 10 };
                //    repository.InsertOne(choice);
                //    choice = new MultipleChoice { Text = "Čije patrole se pominju u pesmi \"Od Topole, od Topole\"?", CorrectAnswer = "Nikole Kalabića", WrongAnswer1 = "Draže Mihajlovića", WrongAnswer2 = "Momčila Đujića", WrongAnswer3 = "Narodne patrole", Points = 10 };
                //    repository.InsertOne(choice);
                //    choice = new MultipleChoice { Text = "Kako glasi ime izvesnog gospodina Šešelja, prvog na spisku koji u skupštini čita ministar Vladan Batić dokazujući Vojislavu Šešelju da je Hrvat?", CorrectAnswer = "Karmelo", WrongAnswer1 = "Hrvoje", WrongAnswer2 = "Ante", WrongAnswer3 = "Stipe", Points = 10 };
                //    repository.InsertOne(choice);

                //    string[] a = { "S.A.R.S", "Barbi", "Action-Man", "igračka" };
                //    string[] b = { "televizija", "Željko Mitrović", "Music", "Film" };
                //    string[] c = { "Predsednik", "5. oktobar", "Premijer", "Nisam obavešten" };
                //    string[] d = { "Bolji život", "Bolji život (repriza)", "Porodično blago", "Tv program" };
                //    AssociationColumn A = new AssociationColumn { Answer = "Lutka", Fields = a };
                //    AssociationColumn B = new AssociationColumn { Answer = "Pink", Fields = b };
                //    AssociationColumn C = new AssociationColumn { Answer = "Koštunica", Fields = c };
                //    AssociationColumn D = new AssociationColumn { Answer = "serija", Fields = d };
                //    AssociationColumn[] cols = { A, B, C, D };
                //    var asoc = new Association { Answer = "Nikad izvini", Columns = cols, Points = 30 };
                //    repository.InsertOne(asoc);

                //    string[] a2 = { "mali", "čovek", "džentlmen", "muškarac" };
                //    string[] b2 = { "Sima/Živko", "Aranđel", "Serija", "Ristana" };
                //    string[] c2 = { "Sunce", "Laušević", "Lazetić", "Obradović" };
                //    string[] d2 = { "kuvano", "glavno", "posno", "narodno" };
                //    AssociationColumn A2 = new AssociationColumn { Answer = "gospodin", Fields = a };
                //    AssociationColumn B2 = new AssociationColumn { Answer = "Srećni ljudi", Fields = b };
                //    AssociationColumn C2 = new AssociationColumn { Answer = "Žarko", Fields = c };
                //    AssociationColumn D2 = new AssociationColumn { Answer = "jelo", Fields = d };
                //    AssociationColumn[] cols2 = { A, B, C, D };
                //    var asoc2 = new Association { Answer = "Popara", Columns = cols, Points = 30 };
                //    repository.InsertOne(asoc2);

                //    string[] steps = { "Sve je pare popio", "Takođe i otkida", "Lira šoumen", "Narod ga pita", "ZAM", "Uvek ide dalje", "Slatki greh", "Grand" };
                //    var step = new StepByStep { Answer = "Saša Popović", Steps = steps, Points = 30 };
                //    repository.InsertOne(step);

                //    string[] steps2 = { "Autor Dece komunizma", "Počeo je sa Mimarom", "A kroz dalji rad se provlači Arkan", "Isti mu je pretio ubistvom", "Goli, ali ne pištolj", "Mada je poštoljem mahao na nacionalnoj frekfenciji", "Ćiriličar", "Urednik TV Hepi" };
                //    step = new StepByStep { Answer = "Milomir Marić", Steps = steps2, Points = 30 };
                //    repository.InsertOne(step);


                //});
                endpoints.MapHub<LobbyHub>("/lobby");
                endpoints.MapHub<MatchHub>("/match");
                endpoints.MapHub<RoomHub>("/room");
                endpoints.MapControllers();
        });
            
        }
    }
}
