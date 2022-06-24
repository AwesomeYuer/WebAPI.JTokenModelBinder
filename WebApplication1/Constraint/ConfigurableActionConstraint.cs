﻿namespace Microshaoft
{
    //using Microshaoft.WebApi.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System;
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
            var r = (actionConstraintContext.Candidates.Count == 1);
            var httpContext = actionConstraintContext.RouteContext.HttpContext;
            var request = httpContext.Request;
            var currentCandidateAction = actionConstraintContext
                                                        .CurrentCandidate
                                                        .Action;
            var currentControllerActionDescriptor = ((ControllerActionDescriptor) currentCandidateAction);
            if (!r)
            {
                var l = currentControllerActionDescriptor
                                                    .MethodInfo
                                                    .GetParameters()
                                                    .Length;
                bool hasValue;
                if (request.Method == "GET")
                {
                    hasValue= request.QueryString.HasValue;
                }
                else
                {
                    hasValue = (request.HasFormContentType);
                }
                r = (hasValue && l > 0) || (!hasValue && l <= 0);
            }
            if (r)
            {
                r = false;
                var currentControllerType = currentControllerActionDescriptor.ControllerTypeInfo.AsType();
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
                        actionRoutePath = @value.ToString();
                    }
                }
                if
                    (
                        typeof(TControllerType)
                                .IsAssignableFrom(currentControllerType)
                        &&
                        !actionRoutePath!
                                .IsNullOrEmptyOrWhiteSpace()
                    )
                {
                    var isAsyncExecuting = currentControllerActionDescriptor
                                                                        .MethodInfo
                                                                        .IsAsync();
                    var url = request.Path.ToString();
                    var isAsyncExecutingByRequest
                            = url.Contains("/async/", StringComparison.OrdinalIgnoreCase);
                    r =
                        (
                            isAsyncExecutingByRequest
                            ==
                            isAsyncExecuting
                        );
                }
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
