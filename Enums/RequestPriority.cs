using System.ComponentModel.DataAnnotations;

namespace TestNalog.Enums;

public enum RequestPriority
{
    [Display(Name = "Низкий")]
    Low,

    [Display(Name = "Средний")]
    Medium,

    [Display(Name = "Высокий")]
    High
}
