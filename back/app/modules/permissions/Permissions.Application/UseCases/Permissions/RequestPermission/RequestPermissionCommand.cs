using FluentValidation;
using Permissions.Domain.Constants;
using CQRS.MediatR.Helper.Abstractions.Messaging;

namespace Permissions.Application.UseCases.Permissions;

public sealed record RequestPermissionCommand(
    string EmployeeName,
    string EmployeeLastName,
    string Description) : ICommand<PermissionResponse>;

internal sealed class RequestPermissionCommandValidator
    : AbstractValidator<RequestPermissionCommand>
{
    public RequestPermissionCommandValidator()
    {
        RuleFor(x => x.EmployeeName)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
                .WithMessage(ValidationsConstants.FieldCantBeEmpty)
            .NotNull()
                .WithMessage(ValidationsConstants.RequiredField)
            .MaximumLength(150)
                .WithMessage(ValidationsConstants.LongField);

        RuleFor(x => x.EmployeeLastName)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
                .WithMessage(ValidationsConstants.FieldCantBeEmpty)
            .NotNull()
                .WithMessage(ValidationsConstants.RequiredField)
            .MaximumLength(150)
                .WithMessage(ValidationsConstants.LongField);

        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
                .WithMessage(ValidationsConstants.FieldCantBeEmpty)
            .NotNull()
                .WithMessage(ValidationsConstants.RequiredField)
            .MaximumLength(250)
                .WithMessage(ValidationsConstants.LongField);
    }
}