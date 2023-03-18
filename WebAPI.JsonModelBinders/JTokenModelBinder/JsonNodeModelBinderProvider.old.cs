#if NETCOREAPP
namespace Microshaoft.WebApi.ModelBinders
{
    using Microshaoft.Web;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json.Linq;
    using System.Text.Json.Nodes;
    using System.Threading.Tasks;
    public class JsonNodeModelBinder : IModelBinder
    {
        private const string _itemKeyOfRequestJTokenParameters = "requestJTokenParameters";

        public virtual JsonNode GetJsonNodeModelBindingResult(ModelBindingContext bindingContext)
        {
            var httpContext = bindingContext
                                        .HttpContext;
            var request = httpContext
                                    .Request;
            var jwtTokenName = "xJwtToken";
            var ok = false;
            ok = request
                    .TryParseJsonNodeParameters
                        (
                            out JsonNode parameters
                            , out var secretJwtToken
                            , async () =>
                            {
                                return
                                    await
                                        bindingContext
                                            .GetFormJsonNodeAsync();
                            }
                            , jwtTokenName
                        );
            if (ok)
            {
                if
                    (
                        !httpContext
                                .Items
                                .ContainsKey
                                    (
                                        _itemKeyOfRequestJTokenParameters
                                    )
                    )
                {
                    httpContext
                            .Items
                            .Add
                                (
                                    _itemKeyOfRequestJTokenParameters
                                    , parameters
                                );
                }
            }
            if (!StringValues.IsNullOrEmpty(secretJwtToken))
            {
                httpContext
                        .Items
                        .Add
                            (
                                jwtTokenName
                                , secretJwtToken
                            );
            }
            return parameters;
        }

        public virtual Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var parameters = GetJsonNodeModelBindingResult(bindingContext);
            bindingContext
                        .Result = ModelBindingResult
                                                .Success
                                                    (
                                                        parameters
                                                    );
            return
                Task
                    .CompletedTask;
        }
    }
}
#endif
