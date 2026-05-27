# 🚀 Školní portál - Návod ke spuštění

## Předpoklady

- ✅ Visual Studio 2026 Community (již máte)
- ✅ .NET 10 SDK (již máte)
- ✅ SQL Server LocalDB (obvykle součástí Visual Studia)
- ✅ Git (pro verzi kontrolu)

## Krok 1: Příprava databáze

Otevřete **Package Manager Console** v Visual Studiu:
- Tools → NuGet Package Manager → Package Manager Console

Spusťte migrace:

```powershell
Add-Migration InitialCreate
```

Poté:

```powershell
Update-Database
```

Pokud by se chyby, zkontrolujte connection string v `appsettings.json`.

## Krok 2: Spuštění aplikace

### Možnost A: Visual Studio
1. Stiskněte `F5` nebo klikněte na tlačítko "Start"
2. Aplikace se otevře v prohlížeči na `https://localhost:7018`

### Možnost B: Příkazový řádek
```powershell
cd "C:\Users\nemecekf24\Desktop\skolni portal\Skolni portal"
dotnet run
```

## Krok 3: Testování

### Navigace
1. Navštivte `https://localhost:7018/`
2. Klikněte na navigační položky (O škole, Obory, Aktuality, Kontakt)
3. Zkontrolujte, že všechny stránky se načítají správně

### Přihlášení
1. Klikněte na "Přihlásit se"
2. Měli byste vidět přihlašovací formulář

### Vytvoření test účtu

#### Přes registraci (pokud máte implementovánu):
1. Na přihlašovací stránce klikněte na "Registrovat se"
2. Vyplňte formulář
3. Vytvořte účet

#### Manuálně (bez registrace):
1. Otevřete SQL Server Management Studio
2. Připojte se na `(localdb)\mssqllocaldb`
3. Najděte databázi `SkolniPortalDb`
4. Přidejte data do tabulky `AspNetUsers` (s hashlovaným heslem)

Nebo jednoduše: přejděte na přihlašovací stránku a zkuste se registrovat pomocí:
- Email: `test@sps-trutnovska.cz`
- Heslo: `Test123` (musí obsahovat číslo)

## Krok 4: Vývoj

### Přidání nových stránek
1. Vytvořte novou akci v `HomeController.cs`
2. Vytvořte odpovídající view soubor v `Views/Home/`
3. Přidejte odkaz v `_Layout.cshtml`

### Přidání funkcionality
1. Vytvořte nový model v `Models/`
2. Vytvořte nový controller v `Controllers/`
3. Vytvořte odpovídající views

### Databáze
Při změně modelů:
```powershell
Add-Migration DescriovaNazvaZmeny
Update-Database
```

## Běžné problémy a řešení

### Problém: "Connection string 'DefaultConnection' not found"
**Řešení:** Zkontrolujte `appsettings.json`, že má správný connection string

### Problém: "Unable to resolve service for type 'ApplicationDbContext'"
**Řešení:** Ujistěte se, že je DbContext registrován v `Program.cs`

### Problém: Migrační chyby
**Řešení:**
```powershell
# Rollback poslední migrace
Remove-Migration

# Vyčistit databázi
Drop-Database

# Vytvořit znovu
Add-Migration InitialCreate
Update-Database
```

### Problém: Hesla nejsou správně
**Řešení:** Použijte heslo obsahující:
- Minimálně 6 znaků
- Alespoň jedno číslo (je vyžadováno v konfiguraci)

Příklady:
- ✅ `Password123`
- ✅ `Test@2025`
- ❌ `password` (bez čísla)

### Problém: HTTPS certifikát
**Řešení:** Pokud se objeví chyba certifikátu:
```powershell
dotnet dev-certs https --trust
```

## Kontakt pro pomoc

Máte-li otázky, zkontrolujte:
1. **AUDIT_REPORT.md** - Detaily o opravách
2. **CHECKLIST.md** - Kontrolní seznam všech funkcí
3. **Program.cs** - Konfigurace aplikace
4. **appsettings.json** - Nastavení

## Užitečné příkazy

```powershell
# Spuštění bez otevření prohlížeče
dotnet run --no-launch-profile

# Build bez spuštění
dotnet build

# Čistit build
dotnet clean

# Projektové informace
dotnet list package

# Aktualizace balíčků
dotnet package update
```

## Architektura aplikace

```
Skolni portal/
├── Controllers/
│   ├── HomeController.cs (Index, About, Programs, News, Contact)
│   └── AccountController.cs (Login, Logout)
├── Models/
│   ├── ErrorViewModel.cs
│   └── LoginViewModel.cs
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml
│   │   ├── About.cshtml
│   │   ├── Programs.cshtml
│   │   ├── News.cshtml
│   │   ├── Contact.cshtml
│   │   └── Privacy.cshtml
│   ├── Account/
│   │   └── Login.cshtml
│   ├── Shared/
│   │   ├── _Layout.cshtml (master layout s navbar a footer)
│   │   ├── Error.cshtml
│   │   └── _ValidationScriptsPartial.cshtml
│   └── _ViewImports.cshtml
├── Data/
│   └── ApplicationDbContext.cs (Entity Framework)
├── wwwroot/
│   ├── css/
│   │   └── site.css (custom styling)
│   ├── js/
│   │   └── site.js
│   └── lib/ (Bootstrap, jQuery, atd.)
├── Program.cs (konfigurace aplikace)
├── appsettings.json (nastavení)
└── Skolni portal.csproj (projekt konfigurace)
```

## Nasazení

Až budete připraveni nasadit do produkce:

1. **Publikujte aplikaci:**
   ```powershell
   dotnet publish -c Release
   ```

2. **Přidejte produkční connection string:**
   - Vytvořte `appsettings.Production.json`
   - Přidejte produkční SQL Server connection string

3. **Nastavte ASPNETCORE_ENVIRONMENT:**
   ```
   ASPNETCORE_ENVIRONMENT=Production
   ```

4. **Zabezpečte aplikaci:**
   - Vygenerujte nový HTTPS certifikát
   - Nastavte správné bezpečnostní headers
   - Zkontrolujte CORS politiku

## 🎉 Hotovo!

Vaší aplikace je nyní připravena k lokálnímu vývoji a testování. Pokud máte jakékoliv otázky, zkontrolujte dokumentaci nebo se vraťte k tomuto návodu.

**Vítejte v návodu ke spuštění školního portálu!** 🚀
