
# FixIt — Платформа за заявки за домашни ремонти

FixIt е уеб приложение, разработено с ASP.NET Core MVC и Entity Framework, което позволява на потребители да подават заявки за ремонтни дейности, а администратори — да ги управляват и следят.

---

## Основни функционалности

### За потребители (`User`)
- Регистрация и вход в системата
- Подаване на заявка за ремонт с прикачена снимка (по избор)
- Преглед и редакция на собствените заявки
- Визуализация на статуса на всяка заявка

### За администратори (`Admin`)
- Достъп до всички подадени заявки
- Промяна на статусите: `Waiting`, `Assigned`, `Completed`
- Изтриване на заявки и свързани изображения

---

## Структура на проекта

```
FixIt/
├── Controllers/
│   ├── AdminController.cs
│   ├── HomeController.cs
│   ├── LoginController.cs
│   ├── RegisterController.cs
│   └── RequestsController.cs
├── Models/
│   ├── User.cs
│   ├── AdminUser.cs
│   ├── RepairRequest.cs
│   ├── ErrorViewModel.cs
│   └── RequestStatus (enum)
├── Data/
│   └── ApplicationDbContext.cs
├── Views/
│   └── (индивидуални папки за всеки контролер)
```

---

## ⚙️ Инсталация и стартиране

### 1. Клониране на репото

```bash
git clone https://github.com/KonstantinNatev/FixIt.git
cd FixIt
```

### 2. Конфигуриране на `appsettings.json`

Добави connection string към локалната база:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\mssqllocaldb;Database=FixItDb;Trusted_Connection=True;"
}
```

### 3. Създаване на база данни и миграции

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Стартиране на приложението

```bash
dotnet run
```

След това отворете `https://localhost:5432` в браузъра.

---

## 🖼️ Качване на изображения

- Потребителите могат да прикачват снимки към заявката
- Всички снимки се запазват в `wwwroot/uploads`
- При редакция или изтриване на заявка, старите изображения се изтриват от файловата система

---

## 🧑‍💻 Разработка

- ASP.NET Core MVC
- Entity Framework Core
- Bootstrap за интерфейс (по избор)
- Razor Pages за визуализация
- Session-базирана аутентикация

---

## 📜 Лиценз

Проектът е създаден с учебна цел. Не е лицензиран за комерсиална употреба без изрично разрешение.

---

## 📧 Контакт

Проект от [Konstantin Natev](https://github.com/KonstantinNatev)