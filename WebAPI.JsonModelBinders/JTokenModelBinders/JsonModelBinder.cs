namespace Microshaoft.WebApi.ModelBinders;

using Microshaoft.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

public abstract class JsonModelBinder<T>
                                    : IModelBinder
                                            where T : class
{
    private const string _itemKeyOfRequestJsonParameters = "requestJsonParameters";

    protected virtual string JwtTokenKey { get; set; } = "xJwtToken";

    public abstract string OnExtractJwtTokenProcessFunc(T tJson, string jwtTokenName);

    public abstract T OnParseProcessFunc(string json);

    public abstract T OnReturnProcessFunc
                                (
                                     IEnumerable
                                        <KeyValuePair<string, StringValues>>
                                            keyValuePairs
                                );

    public T GetJsonModelBindingResult
                                (
                                    ModelBindingContext bindingContext
                                )
        
    {
        var httpContext = bindingContext
                                    .HttpContext;
        var request = httpContext
                                .Request;
        var ok = false;
        ok = request
                .TryParseJsonParameters<T>
                    (
                        OnParseProcessFunc
                        , OnReturnProcessFunc   
                        , out T parameters
                        , out var secretJwtToken
                        , async () =>
                        {
                            return
                                await
                                    bindingContext
                                        .GetFormTJsonAsync<T>(OnReturnProcessFunc);
                        }
                        , JwtTokenKey
                        , OnExtractJwtTokenProcessFunc
                    );
        if (ok)
        {
            if
                (
                    !httpContext
                            .Items
                            .ContainsKey
                                (
                                    _itemKeyOfRequestJsonParameters
                                )
                )
            {
                httpContext
                        .Items
                        .Add
                            (
                                _itemKeyOfRequestJsonParameters
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
                            JwtTokenKey
                            , secretJwtToken
                        );
        }
        return parameters;
    }

    public virtual Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var parameters = GetJsonModelBindingResult
                                        (
                                            bindingContext
                                            
                                        );
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

