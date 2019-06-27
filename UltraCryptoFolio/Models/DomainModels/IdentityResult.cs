using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;

namespace UltraCryptoFolio.Models.DomainModels
{
    public class IdentityResult
    {
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
        public AuthenticationProperties AuthenticationProperties { get; set; }
    }
}
