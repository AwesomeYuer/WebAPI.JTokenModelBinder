namespace WebApplication1.Controllers;

using Microshaoft;
using Microshaoft.Web;
using Microshaoft.WebApi.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

public class Foo
{
    public object[]? a { set; get; }
    public string? b { set; get; }

    public int c { set; get; }

    public JsonNode? d { set; get; }

    public JsonNode? e { set; get; }

    public JsonArray? f { set; get; }

}

public partial class AdminController : ControllerBase
{
    //private readonly ILogger<AdminController> _logger;

    //public AdminController(ILogger<AdminController> logger)
    //{
    //    _logger = logger;
    //}
    [HttpDelete]
    [HttpGet]
    [HttpHead]
    [HttpOptions]
    [HttpPatch]
    [HttpPost]
    [HttpPut]
    [Route("StrongType")]
    public ActionResult StrongTypeParametersModelBinder
                            (
                                [ModelBinder(typeof(StrongTypeModelBinder<Foo>))]
                                Foo
                                    parameters
                            )
    {

        var (callerMemberName, callerFilePath, callerLineNumber) = CallerHelper.GetCallerInfo();

        return
            Request
                .EchoJsonRequestJsonResult
                    (
                        parameters
                        , new
                        {
                            Caller = new
                            {
                                callerMemberName
                                ,
                                callerFilePath
                                ,
                                callerLineNumber
                            }
                        }
                    );

    }

}
