﻿using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
