﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class CandidateCreateDto: ICandidateDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Description { get; set; }
    public string? Email { get; set; }
    public string? ProfilePicture { get; set; }
    public string? ProfilePictureData { get; set; }
    public DateTime DateOfBirth { get; set; }
}
