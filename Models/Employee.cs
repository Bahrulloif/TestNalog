using System;
using System.ComponentModel.DataAnnotations;

namespace TestNalog.Models;

public class Employee
{

    public int Id { get; set; }                              //int	Первичный ключ, формируется автоматически
    public string FullName { get; set; } = string.Empty;     //string	ФИО, обязательно, до 150 символов
    public string Department { get; set; } = string.Empty;   //string	Подразделение, обязательно, до 100 символов
    public string? Phone { get; set; }                       //string?	Телефон, необязательно, до 30 символов

    public List<Request> Requests { get; set; } = new();

}
