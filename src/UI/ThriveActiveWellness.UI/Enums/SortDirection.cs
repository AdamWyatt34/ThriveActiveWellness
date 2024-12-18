using System.ComponentModel.DataAnnotations;

namespace ThriveActiveWellness.UI.Enums;

public enum SortDirection 
{
    None = 0,
    [Display(Name = "Ascending", ShortName = "ASC")]
    Asc = 1,
    [Display(Name = "Descending", ShortName = "DESC")]
    Desc = 2
}

