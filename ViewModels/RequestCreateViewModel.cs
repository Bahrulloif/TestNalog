using System.ComponentModel.DataAnnotations;
using TestNalog.Data;
using TestNalog.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestNalog.ViewModels;

public class RequestCreateViewModel
{
    private string _title = string.Empty;
    private string _description = string.Empty;
    // private RequestPriority _priority = RequestPriority.Medium;
    // private RequestStatus _status = RequestStatus.New;



    [Display(Name = "Cотрудник")]
    [Range(1, int.MaxValue, ErrorMessage = "Прошу выбрать сотрудника")]
    public int EmployeeId { get; set; }         //int	Обязательный внешний ключ на Employee

    [Display(Name = "Название")]
    [Required(ErrorMessage = "Обязательное поле")]
    [StringLength(200, ErrorMessage = "Название должно быть до 200 символов")]
    public string Title
    {
        get => _title;
        set => _title = value?.Trim() ?? string.Empty;
    }     //string	Название, обязательно, до 200 символов

    [Display(Name = "Описание")]
    [Required(ErrorMessage = "Обязательное поле")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Описание должно быть  от 10 до 2000 символов")]
    public string Description
    {
        get => _description;
        set => _description = value?.Trim() ?? string.Empty;
    }       //string	Описание, от 10 до 2000 символов

    // [Display(Name = "Дата и время создания")]
    // public DateTime CreatedDate { get; set; }   //DateTime	Дата и время создания, устанавливаются сервером

    // [Display(Name = "Статус")]
    // [Required(ErrorMessage = "Статус")]
    // [EnumDataType(typeof(RequestStatus))]
    // public RequestStatus Status
    // {
    //     get => _status;
    //     set => _status = value?.Trim() ?? RequestStatus.New;
    // }   //Статус заявки, enum

    [Display(Name = "Приоритет")]
    // [Required(ErrorMessage = "Приоритет")]
    [EnumDataType(typeof(RequestPriority))]
    public RequestPriority Priority { get; set; } = RequestPriority.Medium; //Приоритет заявки, enum

    public IEnumerable<SelectListItem> Employees = new List<SelectListItem>();
}
