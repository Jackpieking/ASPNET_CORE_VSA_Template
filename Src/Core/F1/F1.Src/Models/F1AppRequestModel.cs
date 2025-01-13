using FCommon.Src.FeatureService;

namespace F1.Src.Models;

public sealed class F1AppRequestModel : IServiceRequest<F1AppResponseModel>
{
    public string Email { get; set; }

    public string Password { get; set; }

    public bool RememberMe { get; set; }
}
