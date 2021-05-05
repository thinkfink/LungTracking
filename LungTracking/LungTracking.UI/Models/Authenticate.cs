using LungTracking.BL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LungTracking.UI.Models
{
    public static class Authenticate
    {
        public static bool IsAuthenticated(HttpContext context)
        {
            if (context.Session.GetObject<User>("user") != null)
            {
                User user = context.Session.GetObject<User>("user");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
