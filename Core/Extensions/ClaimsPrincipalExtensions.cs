﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        //ClaimsPrincipal bir kişinin o anki claim'lerine ulaşmak için gereken class
        //claimsPrincipal nesnesi null dönebileceği ve bunun sonucunca NullReferenceException almamak için ? yani nullable operatörüyle birlikte kullanıyoruz.
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}