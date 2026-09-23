namespace MuseumSystem.Domain.Entities;

/// <summary>
/// посетитель музея
/// </summary>
public class Visitor
{
    /// <summary>
    /// идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ФИО посетителя
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// номер телефона
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// дата рождения
    /// </summary>
    public required DateOnly BirthDate { get; set; }
}