namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;
    using Microshaoft.WebApi.ModelBinders;
    using Microshaoft;
    using Microshaoft.Web;
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
        [Route("EchoJsonNode/{* }")]
        public ActionResult EchoJsonNode
                (
                     [ModelBinder(typeof(JsonNodeModelBinder))]
                        JsonNode parameters //= null!
                )
        {
            //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            //MethodInfo currentMethod = (MethodInfo)MethodBase.GetCurrentMethod()!;
            //Console.WriteLine($"{nameof(currentMethod.ReturnType)}:{currentMethod!.ReturnType!.Name}");
            //Console.WriteLine($"{nameof(currentMethod)}:{currentMethod!.Name}");
            //Console.WriteLine($"IsAsync:{currentMethod!.IsAsync()}");
            //Console.WriteLine($"ParametersLength:{currentMethod.GetParameters().Length}");
            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

            return
                Request
                    .EchoJsonRequestJsonResult
                        (parameters);
        }


        [HttpDelete]
        [HttpGet]
        [HttpHead]
        [HttpOptions]
        [HttpPatch]
        [HttpPost]
        [HttpPut]
        [Route("Echo/{* }")]
        public ActionResult EchoJsonNode()
        {
            //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            //MethodInfo currentMethod = (MethodInfo)MethodBase.GetCurrentMethod()!;
            //Console.WriteLine($"{nameof(currentMethod.ReturnType)}:{currentMethod!.ReturnType!.Name}");
            //Console.WriteLine($"{nameof(currentMethod)}:{currentMethod!.Name}");
            //Console.WriteLine($"IsAsync:{currentMethod!.IsAsync()}");
            //Console.WriteLine($"ParametersLength:{currentMethod.GetParameters().Length}");
            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

            return
                EchoJsonNode
                    (
                        null!
                    );
        }


        [HttpDelete]
        [HttpGet]
        [HttpHead]
        [HttpOptions]
        [HttpPatch]
        [HttpPost]
        [HttpPut]
        [Route("EchoJsonNode/{* }")]
        public async Task<ActionResult> EchoJsonNodeAsync
                (
                     [ModelBinder(typeof(JsonNodeModelBinder))]
                        JsonNode parameters //= null!
                )
        {
            //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            //MethodInfo currentMethod = (MethodInfo) MethodBase.GetCurrentMethod()!;
            //Console.WriteLine($"{nameof(currentMethod.ReturnType)}:{currentMethod!.ReturnType!.Name}");
            //Console.WriteLine($"{nameof(currentMethod)}:{currentMethod!.Name}");
            //Console.WriteLine($"IsAsync:{currentMethod!.IsAsync()}");
            //Console.WriteLine($"ParametersLength:{currentMethod.GetParameters().Length}");
            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

            return
                await
                    Task
                        .FromResult
                            (
                                Request
                                    .EchoJsonRequestJsonResult
                                        (parameters)
                            );
                
        }
 
        [HttpDelete]
        [HttpGet]
        [HttpHead]
        [HttpOptions]
        [HttpPatch]
        [HttpPost]
        [HttpPut]
        [Route("EchoJsonNode/{* }")]
        public async Task<ActionResult> EchoJsonNodeAsync()
        {
            //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            //MethodInfo currentMethod = (MethodInfo)MethodBase.GetCurrentMethod()!;
            //Console.WriteLine($"{nameof(currentMethod.ReturnType)}:{currentMethod!.ReturnType!.Name}");
            //Console.WriteLine($"{nameof(currentMethod)}:{currentMethod!.Name}");
            //Console.WriteLine($"IsAsync:{currentMethod!.IsAsync()}");
            //Console.WriteLine($"ParametersLength:{currentMethod.GetParameters().Length}");
            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

            return
                await
                    EchoJsonNodeAsync(null!);

        }

    }
}