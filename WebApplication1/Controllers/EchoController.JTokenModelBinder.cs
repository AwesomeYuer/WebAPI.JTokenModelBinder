namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;
    using Microshaoft.WebApi.ModelBinders;
    using Microshaoft;
    using Microshaoft.Web;

    //[ConstrainedRoute("api/[controller]")]
    //[ApiController]
    //[Route("api/[controller]")]
    public partial class AdminController : ControllerBase
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
        [Route("Echo/JToken/{* }")]
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
        [Route("Echo/JToken/{* }")]
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
        [Route("Echo/JToken/{* }")]
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
        [Route("Echo/JToken/{* }")]
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