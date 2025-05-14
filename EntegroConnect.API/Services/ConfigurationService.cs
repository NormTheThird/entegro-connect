namespace EntegroConnect.API.Services;

public static class ConfigurationService
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Configure named HttpClient with basic authentication for NarVar
        builder.Services.AddHttpClient("NarVarClient", client =>
        {
            var authenticationString = $"{builder.Configuration["NarVarClientUsername"]}:{builder.Configuration["NarVarClientPassword"]}";
            client.BaseAddress = new Uri(builder.Configuration["NarVarClientUrl"]);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString))
            );
        });

        // Configure named HttpClient with basic authentication for ShipStation
        builder.Services.AddHttpClient("ShipStationClient", client =>
        {
            var authenticationString = $"{builder.Configuration["ShipStationClientUsername"]}:{builder.Configuration["ShipStationClientPassword"]}";
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString))
            );
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "CORSPolicy",
                policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
        });

        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        // Add Service Dependencies
        builder.Services.AddScoped<ApiKeyFilter>();
        builder.Services.AddScoped<IFlightService, FlightService>();
        builder.Services.AddScoped<IMessageService, MessageService>();
        builder.Services.AddScoped<INarVarService, NarVarService>();
        builder.Services.AddScoped<IShipStationService, ShipStationService>();
    }

    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, LoggerConfiguration) => LoggerConfiguration
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .MinimumLevel.Override("System", LogEventLevel.Error)
            .WriteTo.Console());
    }

    public static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Entegro Connect", Version = "1.0" });
            setup.IncludeXmlComments(AppDomain.CurrentDomain.BaseDirectory + @"EntegroConnect.API.xml");
            setup.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
            {
                Name = "api-key",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "apikey",
                In = ParameterLocation.Header,
                Description = "Input apikey to access this API",
            });
            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
                    },
                    new string[] {}
                }
            });
        });
    }
}