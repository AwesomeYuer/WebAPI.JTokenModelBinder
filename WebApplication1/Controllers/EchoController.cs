namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;
    using Microshaoft.WebApi.ModelBinders;
    using Microshaoft;
    using Microshaoft.Web;

    [ConstrainedRoute("api/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        [HttpDelete]
        [HttpGet]
        [HttpHead]
        [HttpOptions]
        [HttpPatch]
        [HttpPost]
        [HttpPut]
        [Route("Echo/{* }")]
        public ActionResult Echo
                (
                     [ModelBinder(typeof(JTokenModelBinder))]
                        JToken parameters //= null!
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
                    .EchoJTokenRequestJsonResult
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
        public ActionResult Echo()
        {
            //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            //MethodInfo currentMethod = (MethodInfo)MethodBase.GetCurrentMethod()!;
            //Console.WriteLine($"{nameof(currentMethod.ReturnType)}:{currentMethod!.ReturnType!.Name}");
            //Console.WriteLine($"{nameof(currentMethod)}:{currentMethod!.Name}");
            //Console.WriteLine($"IsAsync:{currentMethod!.IsAsync()}");
            //Console.WriteLine($"ParametersLength:{currentMethod.GetParameters().Length}");
            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

            return
                Echo
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
        [Route("Echo/{* }")]
        public async Task<ActionResult> EchoAsync
                (
                     [ModelBinder(typeof(JTokenModelBinder))]
                        JToken parameters //= null!
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
                                    .EchoJTokenRequestJsonResult
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
        [Route("Echo/{* }")]
        public async Task<ActionResult> EchoAsync()
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
                    EchoAsync(null!);

        }

    }
}