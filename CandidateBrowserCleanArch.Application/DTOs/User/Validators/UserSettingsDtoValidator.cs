using FluentValidation;

namespace CandidateBrowserCleanArch.Application;

public class UserSettingsDtoValidator: AbstractValidator<UserSettingsDto>
{
    private readonly IConfigThemeRepository _configThemeRepository;

    public UserSettingsDtoValidator(IConfigThemeRepository configThemeRepository)
	{
        _configThemeRepository = configThemeRepository;
        RuleFor(c=>c.UserId).NotEmpty().NotNull();
        RuleFor(c=>c.ConfigThemeId).NotEmpty().NotNull();
        RuleFor(p => p.ConfigThemeId).MustAsync(async (settings, id,token) =>
        {
            var result = await _configThemeRepository.GetAsync(id);
            return result is not null;
        }).WithMessage("Provided theme does not exist");
    }
}
