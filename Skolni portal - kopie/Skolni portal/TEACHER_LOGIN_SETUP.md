# Přihlašování Učitelů - Školní Portál

## Přidané Funkce

Byla implementována funkčnost pro registraci a přihlašování učitelů s následujícími vlastnostmi:

### 1. **Registrace Učitelů**
- Uživatelé se mohou registrovat jako běžní uživatelé nebo jako učitelé
- Při registraci jako učitel je vyžadován **správní kód** (admin kód)
- Výchozí admin kód: `UCITEL2026`
- Kontrola domény - pouze emaily s příponou `@spstrutnovska.cz` jsou povoleny

### 2. **Správa Kódů Učitelů**
- Admin panel dostupný na: `/Admin/TeacherCodes`
- Administrátoři (uživatelé s "admin" v emailu) mohou:
  - Vytvořit nové kódy pro registraci učitelů
  - Deaktivovat stávající kódy
  - Vidět seznam všech kódů a jejich stav

### 3. **Správa Učitelů**
- Admin panel dostupný na: `/Admin/Teachers`
- Administrátoři mohou:
  - Vidět seznam všech registrovaných učitelů
  - Odebrat roli učitele jakémukoliv uživateli

### 4. **Zadávání Známek (Učitelé)**
- Učitelé mohou zadávat a spravovat známky svých žáků
- Přístup dostupný na: `/Teacher/Grades`
- Funkčnost:
  - Výběr třídy a předmětu
  - Zadání známky pro jednotlivé žáky
  - Úprava a mazání zadaných známek
  - Přehled všech známek podle tříd a předmětů
  - Možnost exportu známek
  - Automatické ukládání změn

### 5. **Rozvrh Hodin (Žáci)**
- Žáci si mohou prohlédnout svůj rozvrh hodin
- Přístup dostupný na: `/Student/Schedule`
- Funkčnost:
  - Zobrazení denního a týdenního rozvrhu
  - Informace o předmětě a místnosti
  - Možnost filtrace podle dne v týdnu
  - Zobrazení jména učitele
  - Přehledný formát s časem začátku a konce

## Databázové Změny

Byly přidány následující změny do databáze:

### AspNetUsers (rozšíření)
- **Nové pole**: `IsTeacher` (bit) - označuje, je-li uživatel učitel

### TeacherCodes (nová tabulka)
- `Id` (primární klíč)
- `Code` (string) - samotný kód
- `IsActive` (bit) - aktivní/neaktivní
- `CreatedAt` (datetime2) - čas vytvoření

### Grades (nová tabulka)
- `Id` (primární klíč)
- `StudentId` (string) - odkaz na žáka
- `TeacherId` (string) - odkaz na učitele
- `SubjectName` (string) - název předmětu
- `ClassName` (string) - třída žáka
- `GradeValue` (int) - hodnota známky (1-5)
- `CreatedAt` (datetime2) - kdy byla známka zadána
- `UpdatedAt` (datetime2) - kdy byla naposledy upravena

### Schedules (nová tabulka)
- `Id` (primární klíč)
- `StudentId` (string) - odkaz na žáka
- `ClassName` (string) - třída
- `DayOfWeek` (int) - den v týdnu (0-6, pondělí-neděle)
- `StartTime` (time) - čas začátku
- `EndTime` (time) - čas konce
- `SubjectName` (string) - název předmětu
- `TeacherName` (string) - jméno učitele
- `Classroom` (string) - místnost/učebna

## Registrační Formulář

V souboru `Views/Account/Register.cshtml` byla přidána:
- Checkbox "Registruji se jako učitel"
- Pole pro zadání správního kódu (viditelné pouze při zaškrtnutí checkboxu)
- JavaScript pro zobrazení/skrytí pole podle stavu checkboxu

## Jak to Funguje

### Registrace jako Běžný Uživatel:
1. Jděte na `/Account/Register`
2. Vyplňte školní email a heslo
3. Nechte checkbox "Registruji se jako učitel" prázdný
4. Klikněte "Zaregistrovat se"

### Registrace jako Učitel:
1. Jděte na `/Account/Register`
2. Vyplňte školní email a heslo
3. Zaškrtněte checkbox "Registruji se jako učitel"
4. Zadejte správní kód (výchozí: `UCITEL2026`)
5. Klikněte "Zaregistrovat se"

### Správa Učitelů (Pro Administrátory):
1. Přihlaste se jako administrátor (email s "admin" v názvu)
2. V dropdown menu vpravo nahoře uvidíte "Administrace"
3. Zvolte "Správa kódů učitelů" nebo "Seznam učitelů"
4. Spravujte kódy a učitele

### Zadávání Známek (Pro Učitele):
1. Přihlaste se jako učitel
2. Jděte na `/Teacher/Grades` nebo klikněte na "Moje Známky" v menu
3. Vyberte třídu a předmět
4. Zadejte známky pro jednotlivé žáky
5. Změny se uloží automaticky
6. Můžete prohlížet historii všech zadaných známek
7. Máte přístup do všech tříd, které vyučujete

### Prohlížení Rozvrhu (Pro Žáky):
1. Přihlaste se jako žák
2. Jděte na `/Student/Schedule` nebo klikněte na "Můj Rozvrh" v menu
3. Zobrazí se vám týdenní rozvrh vašich hodin
4. Kliknutím na den v týdnu se zobrazí detailní informace
5. V rozvrhu vidíte:
   - Čas výuky (začátek a konec)
   - Název předmětu
   - Jméno učitele
   - Místnost (učebnu)

## Soubory, které Byly Změněny/Přidány

### Přidané soubory:
- `Data/TeacherCode.cs` - model pro správní kódy
- `Data/Grade.cs` - model pro známky žáků
- `Data/Schedule.cs` - model pro rozvrh hodin
- `Controllers/AdminController.cs` - admin panel
- `Controllers/TeacherController.cs` - správa známek (pouze pro učitele)
- `Controllers/StudentController.cs` - zobrazení rozvrhu (pouze pro žáky)
- `Views/Admin/TeacherCodes.cshtml` - správa kódů
- `Views/Admin/Teachers.cshtml` - seznam učitelů
- `Views/Teacher/Grades.cshtml` - zadávání a správa známek
- `Views/Student/Schedule.cshtml` - rozvrh hodin
- `Migrations/[timestamp]_AddTeacherSupport.cs` - databázová migrace
- `Migrations/[timestamp]_AddGradesAndSchedule.cs` - migrace pro známky a rozvrh

### Upravené soubory:
- `Data/ApplicationUser.cs` - přidáno `IsTeacher` pole
- `Data/ApplicationDbContext.cs` - přidány DbSet pro Grades a Schedules
- `ViewModels/RegisterViewModel.cs` - přidány `IsTeacher` a `TeacherCode` properties
- `Controllers/AccountController.cs` - přidána logika pro ověření kódu učitele
- `Views/Account/Register.cshtml` - přidáno UI pro registraci učitelů
- `Views/Shared/_Layout.cshtml` - přidány odkazy na nové části podle role uživatele
- `Views/_ViewImports.cshtml` - přidáno using pro datové modely

## Bezpečnostní Poznámky

1. **Administrátoři** jsou určeni podle toho, zda mají v emailu slovo "admin"
2. V budoucnu doporučuji implementovat správné role (ASP.NET Identity Roles)
3. Správní kódy nejsou hashované v databázi - pro produkci zvažte jejich hashování
4. Všechny admin akce jsou chráněny atributem `[Authorize]`
5. **Zadávání Známek**: Pouze učitelé mohou zadávat známky, žáci je mohou pouze prohlížet
6. **Rozvrh**: Každý žák vidí pouze svůj rozvrh, nemůže vidět rozvrhy ostatních
7. Přístup ke všem částem je kontrolován pomocí `[Authorize]` atributu

## Příklad Admin Emailu

Pokud chcete vytvořit administrátora, zadejte email jako:
- `admin@spstrutnovska.cz`
- `administrace@spstrutnovska.cz`
- `vedeni@spstrutnovska.cz` (pokud obsahuje "admin")

Jednoduše vložte "admin" někam do emailu a budete mít přístup k admin panelu.

## Poznámka o Migraci

Migrace byly úspěšně aplikovány do databáze a přidaly:
- Nové pole `IsTeacher` v tabulce AspNetUsers (default: false)
- Nové tabulky `TeacherCodes`, `Grades` a `Schedules`
- Potřebné indexy pro optimální výkon
- Výchozí admin kód `UCITEL2026`

## Příští Kroky / TODO

Pro kompletní systém zvažte přidání:
1. Hashování správních kódů
2. Implementace správných ASP.NET Identity rolí
3. Zobrazení známek pro žáky (moje známky)
4. Notifikace rodičů o novách známkách
5. Systém absence
6. Hlášení zkoušek
7. Přístup pro rodiče
8. Export rozvrhu do PDF/ICS
