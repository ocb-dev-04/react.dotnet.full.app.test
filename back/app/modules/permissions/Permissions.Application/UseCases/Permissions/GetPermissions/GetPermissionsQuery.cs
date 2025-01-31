using FluentValidation;
using Permissions.Domain.Constants;
using CQRS.MediatR.Helper.Abstractions.Messaging;
using Permissions.Application.UseCases.CommonResponses;

namespace Permissions.Application.UseCases.Permissions;

public sealed record GetPermissionsQuery(int PageNumber) 
    : IQuery<PaginatedCollection<PermissionResponse>>;

internal sealed class GetPermissionsQueryValidator
    : AbstractValidator<GetPermissionsQuery>
{
    public GetPermissionsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .Cascade(CascadeMode.Continue)
            .GreaterThan(0)
                .WithMessage(ValidationsConstants.CantBeNegativeOrZero);
    }
}