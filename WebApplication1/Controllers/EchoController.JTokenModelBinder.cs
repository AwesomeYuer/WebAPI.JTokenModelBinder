namespace WebApplication1.Controllers;

using Microshaoft;
using Microshaoft.Web;
using Microshaoft.WebApi.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

//[ConstrainedRoute("api/[controller]")]
//[ApiController]
//[Route("api/[controller]")]
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
    [Route("Echo/JToken/{* }")]
    public ActionResult EchoJToken
            (
                 [ModelBinder(typeof(JTokenModelBinder))]
                    JToken parameters //= null!
            )
    {
        var (callerMemberName, callerFilePath, callerLineNumber) = CallerHelper.GetCallerInfo();

        return
            Request
                .EchoJsonRequestJsonResult<JToken>
                    (
                        parameters
                        , new
                        {
                            Caller = new
                            {
                                callerMemberName
                                , callerFilePath
                                , callerLineNumber
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
    [Route("Echo/JToken/{* }")]
    public ActionResult EchoJToken()
    {
        var (callerMemberName, callerFilePath, callerLineNumber) = CallerHelper.GetCallerInfo();

        return
            Request
                .EchoJsonRequestJsonResult<JToken>
                    (
                        null!
                        , new
                        {
                            Caller = new
                            {
                                callerMemberName
                                , callerFilePath
                                , callerLineNumber
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
    [Route("Echo/JToken/{* }")]
    public async Task<ActionResult> EchoJTokenAsync
            (
                 [ModelBinder(typeof(JTokenModelBinder))]
                    JToken parameters //= null!
            )
    {
        var (callerMemberName, callerFilePath, callerLineNumber) = CallerHelper.GetCallerInfo();

        return
            await
                Task
                    .FromResult
                        (
                            Request
                                .EchoJsonRequestJsonResult<JToken>
                                    (
                                        parameters
                                        , new
                                        {
                                            Caller = new
                                            {
                                                callerMemberName
                                                , callerFilePath
                                                , callerLineNumber
                                            }
                                        }
                                    )
                        );

    }

    [HttpDelete]
    [HttpGet]
    [HttpHead]
    [HttpOptions]
    [HttpPatch]
    [HttpPost]
    [HttpPut]
    [Route("Echo/JToken/{* }")]
    public async Task<ActionResult> EchoJTokenAsync()
    {
        var (callerMemberName, callerFilePath, callerLineNumber) = CallerHelper.GetCallerInfo();

        return
            await
                Task
                    .FromResult
                        (
                            Request
                                .EchoJsonRequestJsonResult<JToken>
                                    (
                                        null!
                                        , new
                                        {
                                            Caller = new
                                            {
                                                callerMemberName
                                                , callerFilePath
                                                , callerLineNumber
                                            }
                                        }
                                    )
                        );

    }
}