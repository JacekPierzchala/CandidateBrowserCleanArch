﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Domain;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public bool Deleted { get; set; }
}
