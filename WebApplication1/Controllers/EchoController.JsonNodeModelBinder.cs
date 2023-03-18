namespace WebApplication1.Controllers
{
    using Microshaoft;
    using Microshaoft.Web;
    using Microshaoft.WebApi.ModelBinders;
    using Microsoft.AspNetCore.Mvc;
    using System.Text.Json.Nodes;

    [ConstrainedRoute("api/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
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
        [Route("Echo/JsonNode/{* }")]
        public ActionResult EchoJsonNode
                (
                     [ModelBinder(typeof(JsonNodeModelBinder))]
                        JsonNode parameters //= null!
                )
        {
            var (callerMemberName, callerFilePath, callerLineNumber) = CallerHelper.GetCallerInfo();

            return
                Request
                    .EchoJsonRequestJsonResult<JsonNode>
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
        [Route("Echo/JsonNode/{* }")]
        public ActionResult EchoJsonNode()
        {
            var (callerMemberName, callerFilePath, callerLineNumber) = CallerHelper.GetCallerInfo();

            return
                Request
                    .EchoJsonRequestJsonResult<JsonNode>
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
        [Route("Echo/JsonNode/{* }")]
        public async Task<ActionResult> EchoJsonNodeAsync
                (
                     [ModelBinder(typeof(JsonNodeModelBinder))]
                        JsonNode parameters //= null!
                )
        {
            var (callerMemberName, callerFilePath, callerLineNumber) = CallerHelper.GetCallerInfo();

            return
                await
                    Task
                        .FromResult
                            (
                                Request
                                    .EchoJsonRequestJsonResult<JsonNode>
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
        [Route("Echo/JsonNode/{* }")]
        public async Task<ActionResult> EchoJsonNodeAsync()
        {
            var (callerMemberName, callerFilePath, callerLineNumber) = CallerHelper.GetCallerInfo();

            return
                await
                    Task
                        .FromResult
                            (
                                Request
                                    .EchoJsonRequestJsonResult<JsonNode>
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
}