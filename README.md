# WebAPI JsonModelBinders

## JTokenModelBinders
```
Newtonsoft.Json.Linq.JToken
```

## JsonNodeModelBinders
```
System.Text.Json.Nodes.JsonNode
```

## Usage Sample:

```csharp
namespace WebApplication1.Controllers;

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
}
```


## For Testing use `.http` file as below: 
```
WebAPI.JTokenModelBinder\VSCode.Rest.Client.Test\RestClientTest.http
```

[![Build Status](https://microshaoft.visualstudio.com/sample-project-001/_apis/build/status/Microshaoft.WebAPI.JTokenModelBinder?branchName=master)](https://microshaoft.visualstudio.com/sample-project-001/_build/latest?definitionId=17&branchName=master)
[![Build Status](https://microshaoft.visualstudio.com/sample-project-001/_apis/build/status/Microshaoft.WebAPI.JTokenModelBinder?branchName=master)](https://microshaoft.visualstudio.com/sample-project-001/_build/latest?definitionId=17&branchName=master)