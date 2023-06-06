using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Domain;

public class UserSettings:BaseEntity
{
    public ConfigTheme ConfigTheme { get; set; }
    public int ConfigThemeId { get; set; }
    public string UserId { get; set; }
}
