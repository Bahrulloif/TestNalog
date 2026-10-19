using System.ComponentModel.DataAnnotations;

namespace TestNalog.Enums;

public enum RequestStatus
{

    [Display(Name = "Новая")]
    New,

    [Display(Name = "В работе")]
    InProgress, // Переименовано из In_Progress

    [Display(Name = "Завершена")]
    Completed,

    [Display(Name = "Отклонена")]
    Cancelled
}
