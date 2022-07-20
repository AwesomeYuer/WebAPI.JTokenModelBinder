using Microshaoft;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services
    .AddControllers()
//===================================
    .AddNewtonsoftJson();
//===================================

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder
    .Services
    .AddSwaggerGen
        (
            //(c) =>
            //{
            //    c
            //        .ResolveConflictingActions
            //            (
            //                (xApiDescriptions) =>
            //                {
            //                    Console.WriteLine("===============================================");
            //                    foreach (var apiDescription in xApiDescriptions)
            //                    {
            //                        Console.WriteLine($"{apiDescription.ActionDescriptor.DisplayName}:{apiDescription.HttpMethod}");
                                    
            //                    }
            //                    Console.WriteLine("===============================================");
            //                    return
            //                        xApiDescriptions
            //                                    .First();
            //                }
            //            );
            //}
        );

//=================================================================================================
builder
    .Services
    .Configure
        <KestrelServerOptions>
            (
                (options) =>
                {
                    options
                            .AllowSynchronousIO = true;
                }
            );
var configurationBuilder = new ConfigurationBuilder();
var configuration = configurationBuilder.Build();


#region ConfigurableActionConstrainedRouteApplicationModelProvider
// for both NETCOREAPP2_X and NETCOREAPP3_X
// for Sync or Async Action Selector
builder
        .Services
        .TryAddEnumerable
            (
                ServiceDescriptor
                    .Singleton
                        <
                            IApplicationModelProvider
                            , ConfigurableActionConstrainedRouteApplicationModelProvider
                                                                    <ConstrainedRouteAttribute>
                        >
                    (
                        (x) =>
                        {
                            return
                                new
                                    ConfigurableActionConstrainedRouteApplicationModelProvider
                                            <ConstrainedRouteAttribute>
                                        (
                                            configuration
                                            , (attribute) =>
                                            {
                                                return
                                                    new ConfigurableActionConstraint
                                                                <ConstrainedRouteAttribute>
                                                            (
                                                                attribute
                                                            );
                                            }
                                        );
                        }
                    )
            );
#endregion

//==================================================================================================

//==================================================================================================
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
app
    .UseDefaultFiles
        (
            new DefaultFilesOptions()
            {
                DefaultFileNames =
                    {
                        "index.html"
                    }
            }
        );
app.UseStaticFiles();
app.Run();
