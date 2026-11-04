# MuseumSystem

Корпоративное приложение для управления музеем на платформе .NET 10.

## Задание

**Вариант 20:**
*   **Доменная область:** Музей
*   **База данных:** PostgreSQL
*   **Брокер сообщений:** RabbitMQ

### Предметная область

В системе хранятся сведения об экскурсиях, посетителях и билетах.
*   **Экскурсия** характеризуется датой, временем начала, продолжительностью, посещаемыми выставками.
*   **Выставка** содержит сведения о названии, тематике, номере зала, дате начала и дате окончания.
*   **Посетитель** характеризуется ФИО, телефоном, датой рождения.
*   **Билет** содержит информацию об экскурсии и посетителе, тип билета и цену.

## Структура решения

*   **MuseumSystem.Shared** — общие перечисления (Enums), используемые в Domain и Contracts.
*   **MuseumSystem.Domain** — сущности, датасидер, интерфейсы репозиториев.
*   **MuseumSystem.Contracts** — DTO-модели для запросов/ответов и интерфейсы сервисов.
*   **MuseumSystem.Services** — бизнес-логика, мапперы.
*   **MuseumSystem.Infrastructure.InMemory** — реализация обобщенного репозитория для хранения данных в памяти.
*   **MuseumSystem.Api** — ASP.NET Core Web API (контроллеры, DI, Scalar UI, JWT).
*   **MuseumSystem.Tests** — unit-тесты (xUnit v3).

## Запуск проекта

```bash
# Сборка решения
dotnet build MuseumSystem.slnx
```

```bash
# Запуск тестов
dotnet test MuseumSystem.slnx
```

```bash
# Запуск API
dotnet run --project MuseumSystem.Api
```

## Документация API

Для документации используется нативный `Microsoft.AspNetCore.OpenApi` и современный интерфейс **Scalar**.
После запуска приложения перейдите по адресу: `http://localhost:5099/scalar/v1` *(порт может отличаться, проверьте консоль)*.

## Аутентификация (Факультатив)

Реализована JWT-аутентификация.
*   **Эндпоинт для получения токена:** `POST /api/auth/login`
*   **Тестовые данные:** логин `admin`, пароль `password`.
*   В интерфейсе Scalar нажмите кнопку **Authorize** (замок) и вставьте полученный токен.

## Основные эндпоинты

### Выставки (`/api/exhibitions`)
*   `GET /api/exhibitions` — список всех выставок
*   `GET /api/exhibitions/{id}` — выставка по ID
*   `POST /api/exhibitions` — создать выставку
*   `PUT /api/exhibitions/{id}` — обновить выставку
*   `DELETE /api/exhibitions/{id}` — удалить выставку (идемпотентно)

### Экскурсии (`/api/excursions`)
*   `GET /api/excursions` — список всех экскурсий
*   `GET /api/excursions/{id}` — экскурсия по ID
*   `POST /api/excursions` — создать экскурсию
*   `PUT /api/excursions/{id}` — обновить экскурсию
*   `DELETE /api/excursions/{id}` — удалить экскурсию

### Посетители (`/api/visitors`)
*   `GET /api/visitors` — список всех посетителей
*   `GET /api/visitors/{id}` — посетитель по ID
*   `POST /api/visitors` — создать посетителя
*   `PUT /api/visitors/{id}` — обновить посетителя
*   `DELETE /api/visitors/{id}` — удалить посетителя

### Билеты (`/api/tickets`)
*   `GET /api/tickets` — список всех билетов
*   `GET /api/tickets/{id}` — билет по ID
*   `POST /api/tickets` — создать билет
*   `PUT /api/tickets/{id}` — обновить билет
*   `DELETE /api/tickets/{id}` — удалить билет

### Аналитика (`/api/analytics`)
*   `GET /api/analytics/top-exhibitions` — топ-5 выставок по количеству посетителей
*   `GET /api/analytics/minimal-participants` — экскурсии с минимальным числом участников
*   `GET /api/analytics/theme-summary?from={date}&to={date}` — сводка по тематикам за период
*   `GET /api/analytics/excursions-in-hall?hallNumber={number}&from={date}&to={date}` — экскурсии в зале за период
*   `GET /api/analytics/visitors-of-excursion/{excursionId}` — посетители выбранной экскурсии