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

    /// <summary>
    /// возвращает список экскурсий
    /// </summary>
    public static List<Excursion> GetExcursions(List<Exhibition> exhibitions)
    {
        var byId = exhibitions.ToDictionary(e => e.Id);

        return
        [
            new() { Id = 1,  Date = new DateOnly(2026, 1, 15), StartTime = new TimeOnly(10, 0), DurationMinutes = 60, HallNumber = 1, Exhibitions = [byId[1], byId[6]] },
            new() { Id = 2,  Date = new DateOnly(2026, 1, 20), StartTime = new TimeOnly(12, 0), DurationMinutes = 90, HallNumber = 2, Exhibitions = [byId[2], byId[7]] },
            new() { Id = 3,  Date = new DateOnly(2026, 2, 5),  StartTime = new TimeOnly(14, 0), DurationMinutes = 60, HallNumber = 3, Exhibitions = [byId[3], byId[8]] },
            new() { Id = 4,  Date = new DateOnly(2026, 2, 10), StartTime = new TimeOnly(11, 0), DurationMinutes = 45, HallNumber = 4, Exhibitions = [byId[4]]         },
            new() { Id = 5,  Date = new DateOnly(2026, 2, 15), StartTime = new TimeOnly(16, 0), DurationMinutes = 75, HallNumber = 5, Exhibitions = [byId[5], byId[10]] },
            new() { Id = 6,  Date = new DateOnly(2026, 2, 20), StartTime = new TimeOnly(10, 0), DurationMinutes = 60, HallNumber = 1, Exhibitions = [byId[12]]        },
            new() { Id = 7,  Date = new DateOnly(2026, 3, 1),  StartTime = new TimeOnly(13, 0), DurationMinutes = 90, HallNumber = 2, Exhibitions = [byId[11]]        },
            new() { Id = 8,  Date = new DateOnly(2026, 3, 5),  StartTime = new TimeOnly(15, 0), DurationMinutes = 60, HallNumber = 3, Exhibitions = [byId[8]]         },
            new() { Id = 9,  Date = new DateOnly(2026, 3, 10), StartTime = new TimeOnly(11, 30), DurationMinutes = 45, HallNumber = 4, Exhibitions = [byId[9]]        },
            new() { Id = 10, Date = new DateOnly(2026, 3, 15), StartTime = new TimeOnly(17, 0), DurationMinutes = 75, HallNumber = 5, Exhibitions = [byId[10]]        },
            new() { Id = 11, Date = new DateOnly(2026, 3, 20), StartTime = new TimeOnly(12, 0), DurationMinutes = 60, HallNumber = 1, Exhibitions = [byId[1]]         },
            new() { Id = 12, Date = new DateOnly(2026, 3, 25), StartTime = new TimeOnly(14, 0), DurationMinutes = 90, HallNumber = 2, Exhibitions = [byId[7]]         },
        ];
    }

    /// <summary>
    /// возвращает список билетов
    /// </summary>
    public static List<Ticket> GetTickets(List<Excursion> excursions, List<Visitor> visitors)
    {
        var e = excursions.ToDictionary(x => x.Id);
        var v = visitors.ToDictionary(x => x.Id);

        return
        [
            // Экскурсия 1 — 5 билетов
            new() { Id = 1,  Excursion = e[1],  Visitor = v[1],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 2,  Excursion = e[1],  Visitor = v[2],  Type = TicketType.Discounted, Price = 250m },
            new() { Id = 3,  Excursion = e[1],  Visitor = v[3],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 4,  Excursion = e[1],  Visitor = v[4],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 5,  Excursion = e[1],  Visitor = v[5],  Type = TicketType.Discounted, Price = 250m },

            // Экскурсия 2 — 4 билета
            new() { Id = 6,  Excursion = e[2],  Visitor = v[6],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 7,  Excursion = e[2],  Visitor = v[7],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 8,  Excursion = e[2],  Visitor = v[8],  Type = TicketType.Discounted, Price = 250m },
            new() { Id = 9,  Excursion = e[2],  Visitor = v[9],  Type = TicketType.Adult,      Price = 500m },

            // Экскурсия 3 — 6 билетов
            new() { Id = 10, Excursion = e[3],  Visitor = v[1],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 11, Excursion = e[3],  Visitor = v[2],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 12, Excursion = e[3],  Visitor = v[3],  Type = TicketType.Discounted, Price = 250m },
            new() { Id = 13, Excursion = e[3],  Visitor = v[10], Type = TicketType.Adult,      Price = 500m },
            new() { Id = 14, Excursion = e[3],  Visitor = v[11], Type = TicketType.Adult,      Price = 500m },
            new() { Id = 15, Excursion = e[3],  Visitor = v[12], Type = TicketType.Discounted, Price = 250m },

            // Экскурсия 4 — 2 билета (минимум)
            new() { Id = 16, Excursion = e[4],  Visitor = v[4],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 17, Excursion = e[4],  Visitor = v[5],  Type = TicketType.Adult,      Price = 500m },

            // Экскурсия 5 — 3 билета
            new() { Id = 18, Excursion = e[5],  Visitor = v[6],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 19, Excursion = e[5],  Visitor = v[7],  Type = TicketType.Discounted, Price = 250m },
            new() { Id = 20, Excursion = e[5],  Visitor = v[8],  Type = TicketType.Adult,      Price = 500m },

            // Экскурсия 6 — 1 билет (минимум)
            new() { Id = 21, Excursion = e[6],  Visitor = v[9],  Type = TicketType.Adult,      Price = 500m },

            // Экскурсия 7 — 4 билета
            new() { Id = 22, Excursion = e[7],  Visitor = v[10], Type = TicketType.Adult,      Price = 500m },
            new() { Id = 23, Excursion = e[7],  Visitor = v[11], Type = TicketType.Adult,      Price = 500m },
            new() { Id = 24, Excursion = e[7],  Visitor = v[12], Type = TicketType.Discounted, Price = 250m },
            new() { Id = 25, Excursion = e[7],  Visitor = v[1],  Type = TicketType.Adult,      Price = 500m },

            // Экскурсия 8 — 3 билета
            new() { Id = 26, Excursion = e[8],  Visitor = v[2],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 27, Excursion = e[8],  Visitor = v[3],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 28, Excursion = e[8],  Visitor = v[4],  Type = TicketType.Discounted, Price = 250m },

            // Экскурсия 9 — 2 билета (минимум)
            new() { Id = 29, Excursion = e[9],  Visitor = v[5],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 30, Excursion = e[9],  Visitor = v[6],  Type = TicketType.Adult,      Price = 500m },

            // Экскурсия 10 — 5 билетов
            new() { Id = 31, Excursion = e[10], Visitor = v[7],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 32, Excursion = e[10], Visitor = v[8],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 33, Excursion = e[10], Visitor = v[9],  Type = TicketType.Discounted, Price = 250m },
            new() { Id = 34, Excursion = e[10], Visitor = v[10], Type = TicketType.Adult,      Price = 500m },
            new() { Id = 35, Excursion = e[10], Visitor = v[11], Type = TicketType.Adult,      Price = 500m },

            // Экскурсия 11 — 3 билета
            new() { Id = 36, Excursion = e[11], Visitor = v[12], Type = TicketType.Adult,      Price = 500m },
            new() { Id = 37, Excursion = e[11], Visitor = v[1],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 38, Excursion = e[11], Visitor = v[2],  Type = TicketType.Discounted, Price = 250m },

            // Экскурсия 12 — 4 билета
            new() { Id = 39, Excursion = e[12], Visitor = v[3],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 40, Excursion = e[12], Visitor = v[4],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 41, Excursion = e[12], Visitor = v[5],  Type = TicketType.Adult,      Price = 500m },
            new() { Id = 42, Excursion = e[12], Visitor = v[6],  Type = TicketType.Discounted, Price = 250m },
        ];
    }
}