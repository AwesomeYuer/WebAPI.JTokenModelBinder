﻿namespace Microshaoft
{
    //using Microshaoft.WebApi.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System;
    using System.Reflection;
    using WebApplication1.Controllers;

    public class ConfigurableActionConstraint<TRouteAttribute>
                                : 
                                    IActionConstraint
                                    , IActionConstraintMetadata
                                    , IServiceProvider
                    where
                        TRouteAttribute
                                : RouteAttribute , IConfigurable
    {
        private readonly TRouteAttribute _routeAttribute;
        public ConfigurableActionConstraint
                    (
                        TRouteAttribute
                            routeAttribute
                        , Func
                                <
                                    ConfigurableActionConstraint<TRouteAttribute>
                                    , ActionConstraintContext
                                    , TRouteAttribute
                                    , bool
                                >
                            onAcceptCandidateActionProcessFunc = null!
                    )
        {
            _routeAttribute = routeAttribute;
            _onAcceptCandidateActionProcessFunc
                        = onAcceptCandidateActionProcessFunc;
        }
        public virtual bool OnAcceptAsyncOrSyncCandidateActionSelectorProcessFunc
                                    (
                                        ConfigurableActionConstraint<TRouteAttribute> configurableActionConstraint
                                        , ActionConstraintContext actionConstraintContext
                                        , TRouteAttribute routeAttribute
                                    )
        {
            return
                OnAcceptAsyncOrSyncCandidateActionSelectorProcessFunc
                        <AdminController>
                            (
                                configurableActionConstraint
                                , actionConstraintContext
                                , routeAttribute
                            );
        }
        protected virtual bool OnAcceptAsyncOrSyncCandidateActionSelectorProcessFunc<TControllerType>
                                    (
                                        ConfigurableActionConstraint<TRouteAttribute> configurableActionConstraint
                                        , ActionConstraintContext actionConstraintContext
                                        , TRouteAttribute routeAttribute
                                    )
        {
            var r = false;
            MethodInfo currentMethodInfo = null!;
            ControllerActionDescriptor currentControllerActionDescriptor = null!;
            Type currentControllerType = null!;
            if (actionConstraintContext.Candidates.Count > 1)
            {
                var httpContext = actionConstraintContext.RouteContext.HttpContext;
                var request = httpContext.Request;
                var currentCandidateAction = actionConstraintContext
                                                            .CurrentCandidate
                                                            .Action;
                currentControllerActionDescriptor = ((ControllerActionDescriptor) currentCandidateAction);
                var methodParamsLength = currentControllerActionDescriptor
                                                    .MethodInfo
                                                    .GetParameters()
                                                    .Length;
                currentControllerType = currentControllerActionDescriptor.ControllerTypeInfo.AsType();
                var routeContext = actionConstraintContext.RouteContext;

                var actionRoutePath = string.Empty;
                if
                    (
                        routeContext
                                .RouteData
                                .Values
                                .TryGetValue
                                        (
                                            " "
                                            , out var @value
                                        )
                    )
                {
                    if (@value != null)
                    {
                        actionRoutePath = @value.ToString()!.ToLower();
                    }
                }
                var urlPath = request.Path.ToString();
                //=============================================================================
                #region isAsyncExecutingOnDemand
                var isAsyncExecutingOnDemand
                                    = urlPath.Contains("/async/", StringComparison.OrdinalIgnoreCase);
                #endregion
                //=============================================================================
                currentMethodInfo = currentControllerActionDescriptor.MethodInfo;
                var isAsyncMethod = currentMethodInfo.IsAsync();
                if (typeof(TControllerType).IsAssignableFrom(currentControllerType))
                {
                    if (request.Method == "GET")
                    {
                        var hasQueryString = request.QueryString.HasValue;
                        if
                            (
                                hasQueryString
                                && methodParamsLength > 0
                                && isAsyncExecutingOnDemand == isAsyncMethod
                            )
                        {
                            if
                                (
                                    !actionRoutePath!.IsNullOrEmptyOrWhiteSpace()
                                    &&
                                    urlPath.EndsWith(actionRoutePath, StringComparison.OrdinalIgnoreCase)
                                )
                            {
                                r = true;
                            }
                            else if (actionRoutePath!.IsNullOrEmptyOrWhiteSpace())
                            {
                                r = true;
                            }

                        }
                        else if
                            (
                                !hasQueryString
                                && methodParamsLength == 0
                                && isAsyncExecutingOnDemand == isAsyncMethod
                            )
                        {
                            r = true;
                        }
                    }
                    else
                    {
                        var hasContentType = 
                                            (
                                                request.HasFormContentType
                                                ||
                                                request.HasJsonContentType()
                                            );
                        var hasContentLength = request.ContentLength > 0;
                        if
                            (
                                hasContentType
                                && hasContentLength
                                && methodParamsLength > 0
                                && isAsyncExecutingOnDemand == isAsyncMethod
                            )
                        {
                            if
                                (
                                    !actionRoutePath!.IsNullOrEmptyOrWhiteSpace()
                                    &&
                                    urlPath.EndsWith(actionRoutePath)
                                )
                            {
                                r = true;
                            }
                            else if (actionRoutePath!.IsNullOrEmptyOrWhiteSpace())
                            {
                                r = true;
                            }
                        }
                        else if
                            (
                                hasContentType
                                && !hasContentLength
                                && methodParamsLength == 0
                                && isAsyncExecutingOnDemand == isAsyncMethod
                            )
                        {
                            r = true;
                        }
                    }

                }
            }
            else
            {
                r = true;
            }
            if (r)
            {
                Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                Console.WriteLine("OnAcceptAsyncOrSyncCandidateActionSelectorProcessFunc");
                Console.WriteLine($"{nameof(currentControllerActionDescriptor.DisplayName)}\t\t\t:{currentControllerActionDescriptor.DisplayName}");
                Console.WriteLine($"ParametersCount\t\t\t:{currentControllerActionDescriptor.Parameters.Count}");
                Console.WriteLine($"{nameof(currentMethodInfo.ReturnType)}\t\t\t:{currentMethodInfo!.ReturnType!.Name}");
                Console.WriteLine($"{nameof(currentMethodInfo)}\t\t:{currentMethodInfo!.Name}");
                Console.WriteLine($"IsAsync\t\t\t\t:{currentMethodInfo!.IsAsync()}");
                Console.WriteLine($"ParametersLength\t\t:{currentMethodInfo.GetParameters().Length}");
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            }
            return
                    r;
        }

        private readonly
                        Func
                            <
                                ConfigurableActionConstraint<TRouteAttribute>
                                , ActionConstraintContext
                                , TRouteAttribute
                                , bool
                            >
                                _onAcceptCandidateActionProcessFunc = null!;
        public int Order
        {
            get
            {
                return 1;
            }
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
            //return
            //    this;
        }

        public bool Accept(ActionConstraintContext context)
        {
            bool r;
            if (_onAcceptCandidateActionProcessFunc != null)
            {
                r = _onAcceptCandidateActionProcessFunc
                            (
                                this
                                , context
                                , _routeAttribute
                            );
            }
            else
            {
                r = OnAcceptAsyncOrSyncCandidateActionSelectorProcessFunc
                            (
                                this
                                , context
                                , _routeAttribute
                            );
            }
            return
                    r;
        }
    }
}
