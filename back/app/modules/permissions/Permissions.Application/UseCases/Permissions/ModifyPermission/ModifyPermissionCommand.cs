using FluentValidation;
using Permissions.Domain.Constants;
using CQRS.MediatR.Helper.Abstractions.Messaging;

namespace Permissions.Application.UseCases.Permissions;

public sealed record ModifyPermissionRequest(
    string EmployeeName,
    string EmployeeLastName,
    string Description);

public sealed record ModifyPermissionCommand(
    int Id,
    ModifyPermissionRequest Body) : ICommand;

internal sealed class ModifyPermissionComandValidator
    : AbstractValidator<ModifyPermissionCommand>
{
    public ModifyPermissionComandValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
                .WithMessage(ValidationsConstants.FieldCantBeEmpty)
            .NotNull()
                .WithMessage(ValidationsConstants.RequiredField)
            .GreaterThan(0)
                .WithMessage(ValidationsConstants.CantBeNegativeOrZero);

        RuleFor(x => x.Body.EmployeeName)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
                .WithMessage(ValidationsConstants.FieldCantBeEmpty)
            .NotNull()
                .WithMessage(ValidationsConstants.RequiredField)
            .MaximumLength(150)
                .WithMessage(ValidationsConstants.LongField);

        RuleFor(x => x.Body.EmployeeLastName)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
                .WithMessage(ValidationsConstants.FieldCantBeEmpty)
            .NotNull()
                .WithMessage(ValidationsConstants.RequiredField)
            .MaximumLength(150)
                .WithMessage(ValidationsConstants.LongField);

        RuleFor(x => x.Body.Description)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
                .WithMessage(ValidationsConstants.FieldCantBeEmpty)
            .NotNull()
                .WithMessage(ValidationsConstants.RequiredField)
            .MaximumLength(250)
                .WithMessage(ValidationsConstants.LongField);
    }
}