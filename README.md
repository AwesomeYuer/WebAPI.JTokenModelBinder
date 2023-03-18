# WebAPI JsonModelBinders

## 以下说明由 `ChatGPT` 生成

WebAPI JsonModelBinders可以使开发人员在将JSON作为参数或结果传递时轻松实现。可用的JsonModelBinders类型有：JTokenModelBinders，Newtonsoft.Json.Linq.JToken 和 System.Text.Json.Nodes.JsonNode。它们可以帮助开发人员在API控制器中使用JsonNode，JToken或JObject作为参数。实例如下：在API控制器的EchoJsonNode方法中使用ModelBinder特性（接受JsonNode类型的参数）来获取JsonNode参数，然后将其返回以显示结果。

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