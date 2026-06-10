# 🎓 Školní portál - Rychlý Referenční Průvodce

## 🚀 Rychlý Start (2 minuty)

### 1. Otevřete projekt
```
C:\Users\nemecekf24\Desktop\skolni portal\Skolni portal
```

### 2. Spusťte migrace (Package Manager Console)
```powershell
Add-Migration InitialCreate
Update-Database
```

### 3. Stiskněte F5
- Aplikace se spustí na `https://localhost:7018`

---

## 🗺️ Navigace Projektu

```
📁 Projekt Root
├── 📁 Controllers/
│   ├── HomeController.cs → Index, About, Programs, News, Contact
│   └── AccountController.cs → Login, Logout
├── 📁 Models/
│   ├── LoginViewModel.cs → Login model
│   └── ErrorViewModel.cs → Error model
├── 📁 Views/
│   ├── 📁 Home/
│   │   ├── Index.cshtml → Úvodní stránka
│   │   ├── About.cshtml → O škole
│   │   ├── Programs.cshtml → Obory
│   │   ├── News.cshtml → Aktuality
│   │   ├── Contact.cshtml → Kontakt
│   │   └── Privacy.cshtml → GDPR
│   ├── 📁 Account/
│   │   └── Login.cshtml → Přihlášení
│   └── 📁 Shared/
│       ├── _Layout.cshtml → Master layout
│       └── Error.cshtml → Chyby
├── 📁 Data/
│   └── ApplicationDbContext.cs → Databáze
├── 📁 wwwroot/
│   └── css/site.css → Custom CSS
├── Program.cs → Konfigurace
└── appsettings.json → Nastavení
```

---

## 📍 Všechny Stránky

| Stránka | URL | Controller | Action |
|---------|-----|-----------|--------|
| 🏠 Úvod | `/` | Home | Index |
| 🏫 O škole | `/Home/About` | Home | About |
| 🎓 Obory | `/Home/Programs` | Home | Programs |
| 📰 Aktuality | `/Home/News` | Home | News |
| 📞 Kontakt | `/Home/Contact` | Home | Contact |
| 🔐 Přihlášení | `/Account/Login` | Account | Login |
| 🔒 Ochrana dat | `/Home/Privacy` | Home | Privacy |
| ⚠️ Chyba | `/Home/Error` | Home | Error |

---

## 🎨 Přidání Nové Stránky (Základní Postup)

### Krok 1: Vytvoření Action v Controller
```csharp
// Controllers/HomeController.cs
public IActionResult MyNewPage()
{
    return View();
}
```

### Krok 2: Vytvoření View souboru
```
Views/Home/MyNewPage.cshtml
```

### Krok 3: Přidání do Navigace
```html
<!-- Views/Shared/_Layout.cshtml -->
<li class="nav-item">
    <a class="nav-link text-dark" asp-controller="Home" asp-action="MyNewPage">
        Nová stránka
    </a>
</li>
```

---

## 🔐 Přihlášení & Logout

### Login Controller
```csharp
// Controllers/AccountController.cs
[HttpGet]
public IActionResult Login(string? returnUrl = null)
{
    return View();
}

[HttpPost]
public async Task<IActionResult> Login(LoginViewModel model)
{
    var result = await _signInManager.PasswordSignInAsync(...);
    // ...
}
```

### Logout Tlačítko
```html
<form method="post" asp-controller="Account" asp-action="Logout">
    <button type="submit">Odhlásit se</button>
</form>
```

---

## 📦 Databáze

### Connection String
```json
// appsettings.json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SkolniPortalDb;Trusted_Connection=true;"
}
```

### Migrace
```powershell
# Vytvoření migrace
Add-Migration MojeZmena

# Aplikování migrace
Update-Database

# Rollback
Remove-Migration
```

---

## 🎨 CSS Třídas

### Navštívené CSS:
```css
.bg-light-custom      /* Světlé pozadí */
.card-hover           /* Efekt na kartách */
.border-top-oznameni  /* Modrá linka (oznámení) */
.border-top-akce      /* Zelená linka (akce) */
.border-top-prijimacky /* Oranžová linka (přijímací) */
.icon-box             /* Ikony oborů */
.bg-it                /* IT obor - modré */
.bg-stroj             /* Strojírenství - zelené */
.bg-elektro           /* Elektrotechnika - žluté */
.bg-stavba            /* Stavebnictví - fialové */
```

---

## 🔍 Užitečné Příkazy

```powershell
# Spuštění bez prohlížeče
dotnet run --no-launch-profile

# Build
dotnet build

# Čiště
dotnet clean

# Balíčky
dotnet list package
dotnet package update

# HTTPS certifikát
dotnet dev-certs https --trust

# Spuštění testů
dotnet test

# Publikování
dotnet publish -c Release
```

---

## ❌ Běžné Chyby & Řešení

| Chyba | Řešení |
|-------|--------|
| "Connection string not found" | Zkontrolujte `appsettings.json` |
| "DbContext not registered" | Zkontrolujte `Program.cs` |
| Migrace selhání | `Remove-Migration`, pak `Add-Migration` znova |
| HTTPS chyba | `dotnet dev-certs https --trust` |
| Heslo se nepřijímá | Musí obsahovat číslo (1-9) |

---

## 📋 Kontrolní seznam před "Go Live"

- [ ] Databáze je vytvořena
- [ ] Všechny stránky se načítají
- [ ] Přihlášení funguje
- [ ] Všechny odkazy fungují
- [ ] Responsive design je OK
- [ ] Security headers jsou nastaveny
- [ ] HTTPS je nakonfigurován
- [ ] Error handling je aktivní
- [ ] Logging je zapnut
- [ ] Connection string je bezpečný

---

## 🌐 Deployment na Server

```powershell
# 1. Publikování
dotnet publish -c Release -o ./publish

# 2. Zkopírujte publish folder na server

# 3. Na serveru spusťte
dotnet Skolni\ portal.dll

# 4. Nastavte environment proměnné
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=https://0.0.0.0:443;http://0.0.0.0:80
```

---

## 📚 Dokumentace

- `SPUSTENI.md` - Detailní návod
- `AUDIT_REPORT.md` - Co bylo opraveno
- `CHECKLIST.md` - Kontrolní seznam
- `SOUHRN.md` - Celkový přehled

---

## 🆘 Potřebujete pomoc?

1. **Zkontrolujte dokumentaci výše**
2. **Zkontrolujte Output panel** (View → Output)
3. **Zkontrolujte problém v Google**
4. **Zkontrolujte código v problémovém souboru**

---

## 💡 Tipy & Triky

### Debugging
- Stiskněte `F10` pro krokování
- Stiskněte `F5` pro spuštění/pokračování
- Přidejte breakpoint kliknutím na řádek

### View Rendering
- `@` je tag pro C# v HTML
- `asp-controller` generuje správný URL
- `asp-for` binduje model properties

### Model Binding
```csharp
// Automaticky se binduje z formuláře
public IActionResult MyForm(MyModel model)
{
    // model je automaticky naplněn z POST dat
}
```

---

## 🎯 Dalších 10 Minut Setup

Pokud chcete vše:

1. **(2 min)** Spusťte migrace
2. **(3 min)** Spusťte aplikaci
3. **(2 min)** Testujte navigaci
4. **(2 min)** Vytvořte test účet
5. **(1 min)** Zkontrolujte login

**Hotovo! Aplikace je připravena.** ✅

---

## 📊 Verze Información

- **Projekt:** Školní portál
- **Framework:** ASP.NET Core MVC 10.0
- **Bootstrap:** 5.3.2
- **Language:** C# 14.0
- **Database:** SQL Server LocalDB
- **Status:** ✅ Funkční

---

**Všechno je připraveno! Užijte si vývoj! 🚀**
