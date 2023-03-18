﻿namespace Microshaoft.Web;

using Microshaoft.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;

public static partial class HttpRequestHelper
{
    public static JsonResult EchoJsonRequestJsonResult<T>
                    (
                       this HttpRequest @this
                        , T requestJsonParameters
                    )
    {
        return
            new JsonResult
            (
                new
                {
                    WebApiRequestJsonParameters = requestJsonParameters
                    ,
                    Request = new
                    {
                        @this.ContentLength
                            ,
                        @this.ContentType
                            ,
                        @this.Cookies
                            ,
                        @this.HasFormContentType
                            ,
                        HasJsonContentType = @this.HasJsonContentType()
                            ,
                        @this.Headers
                            ,
                        @this.Host
                            ,
                        @this.IsHttps
                            ,
                        @this.Method
                            ,
                        @this.Path
                            ,
                        @this.PathBase
                            ,
                        @this.Protocol
                            ,
                        @this.Query
                            ,
                        @this.QueryString
                            ,
                        HasQueryStringValue = @this.QueryString.HasValue
                            ,
                        @this.RouteValues
                            ,
                        @this.Scheme
                    }
                    ,
                    HttpContext = new
                    {
                        Connection = new
                        {
                            RemoteIpAddress = @this
                                                    .HttpContext
                                                    .Connection
                                                    .RemoteIpAddress!
                                                    .ToString()
                        }
                            //, HttpContext.Items
                            ,
                        User = new
                        {
                            Claims = @this
                                        .HttpContext
                                        .User
                                        .Claims
                                        .ToArray()
                                ,
                            Identity = new
                            {
                                @this
                                    .HttpContext
                                    .User
                                    .Identity!
                                    .Name
                            }
                        }
                    }
                }
            );
    }



    public static JsonResult NewJsonResult
                            (
                                int statusCode
                                , int resultCode
                                , string message
                            )
    {
        return
            new JsonResult
                    (
                        new
                        {
                            statusCode
                            , resultCode
                            , message
                        }
                    )
                {
                    StatusCode = statusCode
                    , ContentType = "application/json"
                };
    }

    public static void SetJsonResult
                            (
                                this ActionExecutingContext @this
                                , int statusCode
                                , int resultCode
                                , string message
                            )
    {
        @this
            .Result = NewJsonResult
                        (
                            statusCode
                            , resultCode
                            , message
                        );
    }
    public static void SetJsonResult
                    (
                        this ActionExecutedContext @this
                        , int statusCode
                        , int resultCode
                        , string message
                    )
    {
        @this
            .Result = NewJsonResult
                        (
                            statusCode
                            , resultCode
                            , message
                        );
    }
    public static void SetNotFoundJsonResult
                            (
                                this ActionExecutedContext @this
                                , int? resultCode = null
                                , string message = null!
                            )
    {
        
        var statusCode = 404;
        if (resultCode == null || !resultCode.HasValue)
        {
            resultCode = -1 * statusCode;
        }
        if (message.IsNullOrEmptyOrWhiteSpace())
        {
            var request = @this.HttpContext.Request;
            message = $"{request.Path.Value} not found!";
        }
        @this
            .Result = NewJsonResult
                        (
                            statusCode
                            , resultCode.Value
                            , message
                        );
    }
    public static void SetNotFoundJsonResult
                    (
                        this ActionExecutingContext @this
                        , int? resultCode = null
                        , string message = null!
                    )
    {
        var statusCode = 404;
        if (resultCode == null || !resultCode.HasValue)
        {
            resultCode = -1 * statusCode;
        }
        if (message.IsNullOrEmptyOrWhiteSpace())
        {
            var request = @this.HttpContext.Request;
            message = $"{request.Path.Value} not found!";
        }
        @this
            .Result = NewJsonResult
                        (
                            statusCode
                            , resultCode.Value
                            , message
                        );
    }

    public static string GetActionRoutePath
                (
                    this HttpRequest @this
                    , string key = " "
                )
    {
        return
            @this
                .RouteValues[key]!
                .ToString()!;
    }

    public static string GetActionRoutePathOrDefault
        (
            this HttpRequest @this
            , string defaultValue = default!
            , string key = " "
        )
    {
        string? r;
        if
            (
                TryGetActionRoutePath
                    (
                        @this
                        , out var @value
                        , key
                    )
            )
        {
            r = @value;
        }
        else
        {
            r = defaultValue;
        }
        return
            r;
    }
    public static bool TryGetActionRoutePath
        (
            this HttpRequest @this
            , out string @value
            , string key = " "
        )
    {
        @value = string.Empty;
        var r = false;
        if
            (
                @this
                    .RouteValues
                    .TryGetValue
                        (
                            key
                            , out var @object
                        )
                &&
                @object != null
            )
        {
            if (@object is string s)
            {
                if (!s.IsNullOrEmptyOrWhiteSpace())
                {
                    @value = s;
                    r = true;
                }
            }
        }
        return
            r;
    }

    public static bool TryParseJsonParameters<T>
        (
            this HttpRequest @this
            , Func<string, T> onParseProcessFunc
            , Func
                <
                    IEnumerable
                            <KeyValuePair<string, StringValues>>
                    , T
                >
                    onReturnProcessFunc
            , out T parameters
            , out string secretJwtToken
            , Func<Task<T>> onFormProcessFuncAsync = null!
            , string jwtTokenName = "xJwtToken"
            , Func<T, string, string> onExtractJwtTokenProcessFuncAsync = null!
            
        )
        where T : class
    {
        bool r;
        T jsonNode = default!;
        void requestFormBodyProcess()
        {
            var hasContentLength = @this.ContentLength > 0;
            if
                (
                    !@this.IsJsonRequest()
                    &&
                    @this.HasFormContentType
                    &&
                    hasContentLength
                )
            {
                if (onFormProcessFuncAsync != null)
                {
                    jsonNode = onFormProcessFuncAsync().Result;
                }
            }
            else if (hasContentLength)
            {
                using var streamReader = new StreamReader(@this.Body);
                var json = streamReader.ReadToEnd();
                if (!json.IsNullOrEmptyOrWhiteSpace())
                {
                    json
                        .IsJson<T>
                            (
                                onParseProcessFunc
                                , out jsonNode
                                , true
                            );
                }
            }
        }
        void requestQueryStringHeaderProcess()
        {
            var queryString = @this.QueryString.Value;
            if (queryString!.IsNullOrEmptyOrWhiteSpace())
            {
                return;
            }
            queryString = HttpUtility
                                .UrlDecode
                                    (
                                        queryString
                                    );
            if (queryString!.IsNullOrEmptyOrWhiteSpace())
            {
                return;
            }
            queryString = queryString!.TrimStart('?');
            if (queryString.IsNullOrEmptyOrWhiteSpace())
            {
                return;
            }
            var isJson = false;
            try
            {
                if (queryString.IsJson(onParseProcessFunc ,out jsonNode, true))
                {
                    isJson = jsonNode is T;
                }
            }
            catch
            {

            }
            if (!isJson)
            {
                jsonNode = @this.Query.ToTJson<T>(onReturnProcessFunc);

                //Console.WriteLine("@this.Query.ToJToken()");
            }
        }
        // 取 jwtToken 优先级顺序：Header → QueryString → Body
        StringValues jwtToken = string.Empty;
        var needExtractJwtToken = !jwtTokenName.IsNullOrEmptyOrWhiteSpace();
        void extractJwtToken()
        {
            if (needExtractJwtToken)
            {
                if (jsonNode != null)
                {
                    if (StringValues.IsNullOrEmpty(jwtToken))
                    {
                        //var j = jsonNode[jwtTokenName];
                        //if (j != null)
                        //{
                        //    jwtToken = j.GetValue<string>();
                        //}
                        if (needExtractJwtToken)
                        {
                            if (onExtractJwtTokenProcessFuncAsync != null)
                            {
                                jwtToken = onExtractJwtTokenProcessFuncAsync(jsonNode, jwtTokenName);
                            }
                        }
                    }
                }
            }
        }
        if (needExtractJwtToken)
        {
            @this
                .Headers
                .TryGetValue
                        (
                           jwtTokenName
                           , out jwtToken
                        );
        }
        if
            (
                string.Compare(@this.Method, "get", true) != 0
                &&
                string.Compare(@this.Method, "head", true) != 0
            )
        {
            requestFormBodyProcess();
        }
        else
        {
            requestQueryStringHeaderProcess();
        }
        extractJwtToken();
        parameters = jsonNode;
        secretJwtToken = jwtToken;
        r = true;
        return r;
    }


    public static async
                Task<T> GetFormTJsonAsync<T>
                    (
                        this ModelBindingContext @this
                        , Func
                                    <
                                        IEnumerable
                                                <KeyValuePair<string, StringValues>>
                                        , T
                                    >
                                onReturnProcessFunc
                    )
    {
        T r = default!;
        var formCollectionModelBinder =
                            new FormCollectionModelBinder
                                    (
                                        NullLoggerFactory
                                                    .Instance
                                    );
        await
            formCollectionModelBinder
                        .BindModelAsync(@this);
        if
            (
                @this
                    .Result
                    .IsModelSet
            )
        {
            r = JsonWebHelper
                            .ToTJson<T>
                                (
                                    (IFormCollection)
                                        @this
                                            .Result
                                            .Model!
                                    , onReturnProcessFunc
                                );
        }
        return r;
    }


    //public static async 
    //    Task<JToken> GetFormJTokenAsync
    //                        (
    //                            this ModelBindingContext @this
    //                        )
    //{
    //    JToken r = null!;
    //    var formCollectionModelBinder =
    //                        new FormCollectionModelBinder
    //                                (
    //                                    NullLoggerFactory
    //                                                .Instance
    //                                );
    //    await
    //        formCollectionModelBinder
    //                    .BindModelAsync(@this);
    //    if 
    //        (
    //            @this
    //                .Result
    //                .IsModelSet
    //        )
    //    {
    //        r = JTokenWebHelper
    //                        .ToJToken
    //                            (
    //                                (IFormCollection)
    //                                    @this
    //                                        .Result
    //                                        .Model!
    //                            );
    //    }
    //    return r;
    //}


    //public static async Task<JsonNode> GetFormJsonNodeAsync
    //                (
    //                    this ModelBindingContext @this
    //                )
    //{
    //    JsonNode r = null!;
    //    var formCollectionModelBinder =
    //                        new FormCollectionModelBinder
    //                                (
    //                                    NullLoggerFactory
    //                                                .Instance
    //                                );
    //    await
    //        formCollectionModelBinder
    //                    .BindModelAsync(@this);
    //    if
    //        (
    //            @this
    //                .Result
    //                .IsModelSet
    //        )
    //    {
    //        r = JsonNodeWebHelper
    //                        .ToJsonNode
    //                            (
    //                                (IFormCollection)
    //                                    @this
    //                                        .Result
    //                                        .Model!
    //                            );
    //    }
    //    return r;
    //}
}
