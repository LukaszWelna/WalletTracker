﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.ApplicationUser
{
    public interface IUserContextService
    {
        CurrentUser GetCurrentUser();
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public CurrentUser GetCurrentUser()
        {
            var user = _httpContextAccessor?.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("User context is not present");
            }

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                throw new InvalidOperationException("User not logged in");
            }

            var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;

            return new CurrentUser(id, email);
        }
    }
}