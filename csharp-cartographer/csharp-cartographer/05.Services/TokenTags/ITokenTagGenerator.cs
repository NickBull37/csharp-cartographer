﻿using csharp_cartographer._03.Models.Tokens;

namespace csharp_cartographer._05.Services.TokenTags
{
    public interface ITokenTagGenerator
    {
        void GenerateTokenTags(List<NavToken> navTokens);
    }
}