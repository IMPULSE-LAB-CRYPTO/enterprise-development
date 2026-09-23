using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Enums;

namespace MuseumSystem.Domain.Data;

/// <summary>
/// генератор тестовых данных музея
/// </summary>
public static class MuseumDataSeeder
{
    /// <summary>
    /// возвращает список выставок
    /// </summary>
    public static List<Exhibition> GetExhibitions()
    {
        return
        [
            new() { Id = 1,  Name = "Древний Египет",         Theme = ExhibitionTheme.Historical,   HallNumber = 1, StartDate = new DateOnly(2026, 1, 10), EndDate = new DateOnly(2026, 3, 10) },
            new() { Id = 2,  Name = "Русский авангард",       Theme = ExhibitionTheme.Art,          HallNumber = 2, StartDate = new DateOnly(2026, 2, 1),  EndDate = new DateOnly(2026, 4, 1)  },
            new() { Id = 3,  Name = "Космос",                 Theme = ExhibitionTheme.Science,      HallNumber = 3, StartDate = new DateOnly(2026, 1, 15), EndDate = new DateOnly(2026, 5, 1)  },
            new() { Id = 4,  Name = "Народы Севера",          Theme = ExhibitionTheme.Ethnographic, HallNumber = 4, StartDate = new DateOnly(2026, 3, 1),  EndDate = new DateOnly(2026, 6, 1)  },
            new() { Id = 5,  Name = "Робототехника",          Theme = ExhibitionTheme.Technical,    HallNumber = 5, StartDate = new DateOnly(2026, 2, 15), EndDate = new DateOnly(2026, 7, 1)  },
            new() { Id = 6,  Name = "Средневековье",          Theme = ExhibitionTheme.Historical,   HallNumber = 1, StartDate = new DateOnly(2026, 1, 20), EndDate = new DateOnly(2026, 3, 20) },
            new() { Id = 7,  Name = "Импрессионизм",          Theme = ExhibitionTheme.Art,          HallNumber = 2, StartDate = new DateOnly(2026, 2, 10), EndDate = new DateOnly(2026, 4, 10) },
            new() { Id = 8,  Name = "Физика частиц",          Theme = ExhibitionTheme.Science,      HallNumber = 3, StartDate = new DateOnly(2026, 1, 5),  EndDate = new DateOnly(2026, 4, 5)  },
            new() { Id = 9,  Name = "Традиции Востока",       Theme = ExhibitionTheme.Ethnographic, HallNumber = 4, StartDate = new DateOnly(2026, 3, 5),  EndDate = new DateOnly(2026, 6, 5)  },
            new() { Id = 10, Name = "Авиация",                Theme = ExhibitionTheme.Technical,    HallNumber = 5, StartDate = new DateOnly(2026, 2, 20), EndDate = new DateOnly(2026, 7, 20) },
            new() { Id = 11, Name = "Ренессанс",              Theme = ExhibitionTheme.Art,          HallNumber = 2, StartDate = new DateOnly(2026, 3, 1),  EndDate = new DateOnly(2026, 5, 1)  },
            new() { Id = 12, Name = "Археология",             Theme = ExhibitionTheme.Historical,   HallNumber = 1, StartDate = new DateOnly(2026, 2, 1),  EndDate = new DateOnly(2026, 4, 1)  },
        ];
    }

    /// <summary>
    /// возвращает список посетителей
    /// </summary>
    public static List<Visitor> GetVisitors()
    {
        return
        [
            new() { Id = 1,  FullName = "Андреев Алексей Сергеевич",       Phone = "+7 (911) 100-00-01", BirthDate = new DateOnly(1990, 4, 12)  },
            new() { Id = 2,  FullName = "Борисова Марина Владимировна",    Phone = "+7 (911) 100-00-02", BirthDate = new DateOnly(1985, 11, 25) },
            new() { Id = 3,  FullName = "Васильев Дмитрий Олегович",       Phone = "+7 (911) 100-00-03", BirthDate = new DateOnly(1978, 2, 18)  },
            new() { Id = 4,  FullName = "Гаврилова Елена Николаевна",      Phone = "+7 (911) 100-00-04", BirthDate = new DateOnly(1992, 6, 30)  },
            new() { Id = 5,  FullName = "Дмитриев Павел Андреевич",        Phone = "+7 (911) 100-00-05", BirthDate = new DateOnly(1996, 9, 5)   },
            new() { Id = 6,  FullName = "Егорова Ольга Ивановна",          Phone = "+7 (911) 100-00-06", BirthDate = new DateOnly(1983, 12, 1)  },
            new() { Id = 7,  FullName = "Жуков Игорь Валерьевич",          Phone = "+7 (911) 100-00-07", BirthDate = new DateOnly(1994, 5, 22)  },
            new() { Id = 8,  FullName = "Зотова Анна Викторовна",          Phone = "+7 (911) 100-00-08", BirthDate = new DateOnly(1999, 8, 17)  },
            new() { Id = 9,  FullName = "Иванов Сергей Петрович",          Phone = "+7 (911) 100-00-09", BirthDate = new DateOnly(1975, 1, 9)   },
            new() { Id = 10, FullName = "Козлова Мария Дмитриевна",        Phone = "+7 (911) 100-00-10", BirthDate = new DateOnly(2001, 3, 28)  },
            new() { Id = 11, FullName = "Лебедев Артём Игоревич",          Phone = "+7 (911) 100-00-11", BirthDate = new DateOnly(2003, 7, 7)   },
            new() { Id = 12, FullName = "Морозова Татьяна Львовна",        Phone = "+7 (911) 100-00-12", BirthDate = new DateOnly(1988, 10, 10) },
        ];
    }
}