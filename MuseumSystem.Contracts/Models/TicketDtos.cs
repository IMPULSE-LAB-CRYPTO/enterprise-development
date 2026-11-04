using System.ComponentModel.DataAnnotations;
using MuseumSystem.Domain.Enums;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Данные билета для ответа
/// </summary>
public class TicketResponse
{
    /// <summary>
    /// Уникальный идентификатор билета
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    public int ExcursionId { get; set; }

    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    public int VisitorId { get; set; }

    /// <summary>
    /// Тип билета (взрослый/льготный)
    /// </summary>
    public TicketType Type { get; set; }

    /// <summary>
    /// Цена билета
    /// </summary>
    public decimal Price { get; set; }
}

/// <summary>
/// Данные для создания билета
/// </summary>
public class TicketCreateRequest
{
    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    [Required]
    public int ExcursionId { get; set; }

    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    [Required]
    public int VisitorId { get; set; }

    /// <summary>
    /// Тип билета (взрослый/льготный)
    /// </summary>
    [Required]
    public TicketType Type { get; set; }

    /// <summary>
    /// Цена билета
    /// </summary>
    [Range(0, 1000000)]
    public decimal Price { get; set; }
}

/// <summary>
/// Данные для обновления билета
/// </summary>
public class TicketUpdateRequest
{
    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    [Required]
    public int ExcursionId { get; set; }

    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    [Required]
    public int VisitorId { get; set; }

    /// <summary>
    /// Тип билета (взрослый/льготный)
    /// </summary>
    [Required]
    public TicketType Type { get; set; }

    /// <summary>
    /// Цена билета
    /// </summary>
    [Range(0, 1000000)]
    public decimal Price { get; set; }
}