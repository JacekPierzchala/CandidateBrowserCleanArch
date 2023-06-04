using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class UpdateUserDtoValidator:AbstractValidator<UpdateUserDto>
{
    private readonly IRoleRepository _roleRepository;

    public UpdateUserDtoValidator(IRoleRepository roleRepository)
	{
        _roleRepository = roleRepository;

        RuleFor(p => p.FirstName).NotEmpty().NotNull();
		RuleFor(p => p.LastName).NotEmpty().NotNull();
		RuleFor(p => p.Id).NotEmpty().NotNull();
		RuleFor(p => p.Roles).NotEmpty().NotNull()
            .WithMessage("No role provided"); 
		RuleFor(p => p.Roles).Must(c => c.Count() > 0)
             .WithMessage("No role provided");
        RuleFor(p => p.Roles).ForEach(c => c.MustAsync(async(role, token) =>            
        {
            var result=await _roleRepository.GetRoleByIdAsync(role.Id);
            return result is not null;
        })).WithMessage("Provided role does not exist");



    }
}
