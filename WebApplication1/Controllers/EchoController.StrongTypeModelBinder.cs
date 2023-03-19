namespace WebApplication1.Controllers;

using Microshaoft;
using Microshaoft.Web;
using Microshaoft.WebApi.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

public class Foo
{
    public object[]? a { set; get; }
    public string? b { set; get; }

    public int c { set; get; }

    public JsonObject? d { set; get; } //= new JsonObject();

    public JObject? e { set; get; } //= new JObject();

    public JsonArray? f { set; get; }

    public JArray? g { set; get; }

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
    [Route("StrongType/JTokenParse")]
    public ActionResult JTokenParseStrongTypeParameters
                            (
                                [ModelBinder(typeof(JTokenStrongTypeModelBinder<Foo>))]
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
    [HttpDelete]
    [HttpGet]
    [HttpHead]
    [HttpOptions]
    [HttpPatch]
    [HttpPost]
    [HttpPut]
    [Route("StrongType/JsonNodeParse")]
    public ActionResult JsonNodeParseStrongTypeParameters
                            (
                                [ModelBinder(typeof(JsonNodeStrongTypeModelBinder<Foo>))]
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
