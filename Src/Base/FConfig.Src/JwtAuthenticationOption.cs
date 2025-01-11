using System.Collections.Generic;

namespace FConfig.Src;

public sealed class JwtAuthenticationOption
{
    public bool ValidateIssuer { get; set; }

    public bool ValidateAudience { get; set; }

    public bool ValidateLifetime { get; set; }

    public bool ValidateIssuerSigningKey { get; set; }

    public bool RequireExpirationTime { get; set; }

    public string ValidIssuer { get; set; }

    public string ValidAudience { get; set; }

    public string IssuerSigningKey { get; set; }

    public IEnumerable<string> ValidTypes { get; set; } = [];
}
