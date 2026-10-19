using System;
using System.ComponentModel.DataAnnotations;

namespace TestNalog.ViewModels;

public class EmployeeFormViewModel
{
    private string _fullName = string.Empty;
    private string _department = string.Empty;
    private string _phone = string.Empty;

    public int Id { get; set; }

    [Display(Name = "Ф.И.О.")]
    [Required(ErrorMessage = "Поле Ф.И.О. не должно превышать 150 символов")]
    [StringLength(150, ErrorMessage = "ФИО должно быть до 150 символов")]
    public string FullName
    {
        get => _fullName;
        set => _fullName = value?.Trim() ?? string.Empty;
    }

    [Display(Name = "Департамент")]
    [Required(ErrorMessage = "Поле Департамент не должно превышать 100 символов")]
    [StringLength(100, ErrorMessage = "Департамент должно быть до 100 символов")]
    public string Department
    {
        get => _department;
        set => _department = value?.Trim() ?? string.Empty;
    }

    [Display(Name = "Телефон")]
    [StringLength(30, ErrorMessage = "Номер телефона не должен быть больше 30 символов")]
    public string? Phone
    {
        get => _phone;
        set => _phone = value?.Trim();
    }

    
}
