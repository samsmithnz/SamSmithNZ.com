using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.DataAccess.FooFighters.Interfaces;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.DataAccess.WorldCup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SamSmithNZ.Service
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
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
            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null); //Force PascalCase (and disable camelCase)          

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SamSmithNZ.Service", Version = "v1" });
            });

            //Foo Fighters
            services.AddScoped<SamSmithNZ.Service.DataAccess.FooFighters.Interfaces.IAlbumDataAccess, SamSmithNZ.Service.DataAccess.FooFighters.AlbumDataAccess>();
            services.AddScoped<IAverageSetlistDataAccess, AverageSetlistDataAccess>();
            services.AddScoped<IShowDataAccess, ShowDataAccess>();
            services.AddScoped<ISongDataAccess, SongDataAccess>();
            services.AddScoped<IYearDataAccess, YearDataAccess>();

            //Guitar Tab
            services.AddScoped<SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces.IAlbumDataAccess, SamSmithNZ.Service.DataAccess.GuitarTab.AlbumDataAccess>();
            services.AddScoped<IArtistDataAccess, ArtistDataAccess>();
            services.AddScoped<IRatingDataAccess, RatingDataAccess>();
            services.AddScoped<ISearchDataAccess, SearchDataAccess>();
            services.AddScoped<ITabDataAccess, TabDataAccess>();
            services.AddScoped<ITrackOrderDataAccess, TrackOrderDataAccess>();
            services.AddScoped<ITuningDataAccess, TuningDataAccess>();

            //Int Football
            services.AddScoped<IEloRatingDataAccess, EloRatingDataAccess>();
            services.AddScoped<IGameDataAccess, GameDataAccess>();
            services.AddScoped<IGameGoalAssignmentDataAccess, GameGoalAssignmentDataAccess>();
            services.AddScoped<IGoalDataAccess, GoalDataAccess>();
            services.AddScoped<IGroupCodeDataAccess, GroupCodeDataAccess>();
            services.AddScoped<IGroupDataAccess, GroupDataAccess>();
            services.AddScoped<IPenaltyShootoutGoalDataAccess, PenaltyShootoutGoalDataAccess>();
            services.AddScoped<IPlayerDataAccess, PlayerDataAccess>();
            services.AddScoped<ITeamDataAccess, TeamDataAccess>();
            services.AddScoped<ITournamentDataAccess, TournamentDataAccess>();
            services.AddScoped<ITournamentTeamDataAccess, TournamentTeamDataAccess>();
            services.AddScoped<ITournamentTopGoalScorerDataAccess, TournamentTopGoalScorerDataAccess>();


            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SamSmithNZ.Service v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
