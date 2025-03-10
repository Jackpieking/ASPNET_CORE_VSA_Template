using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace F2.Presentation;

[ValidateNever]
public sealed class Request
{
    [FromRoute]
    public long TodoTaskListId { get; init; }
}
