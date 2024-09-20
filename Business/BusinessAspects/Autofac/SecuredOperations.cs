using Business.Constants;
using Castle.DynamicProxy;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
       
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;  //istek yapılıyor her istek için bir httpcontext oluşturur

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); //Split senin verdiğin o karaktere göre arrayde onları ayırıyor
            _httpContextAccessor =  ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
