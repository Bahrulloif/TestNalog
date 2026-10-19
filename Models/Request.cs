using System.ComponentModel.DataAnnotations;
using TestNalog.Enums;

namespace TestNalog.Models;

public class Request
{

    public int Id { get; set; }                 //int	Первичный ключ, формируется автоматически

    public string Title { get; set; } = string.Empty;      //string	Название, обязательно, до 200 символов

    public string Description { get; set; } = string.Empty;      //string	Описание, от 10 до 2000 символов
    public DateTime CreatedDate { get; set; }   //DateTime	Дата и время создания, устанавливаются сервером
    public RequestStatus Status { get; set; }   //Статус заявки, enum
    public RequestPriority Priority { get; set; }   //Приоритет заявки, enum


    public int EmployeeId { get; set; }         //int	Обязательный внешний ключ на Employee
    public Employee Employee { get; set; } = null!; //Навигационное свойство для связи с Employee
}
