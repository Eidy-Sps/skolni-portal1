# ✅ Rozvrh a Zadávání Známek - Oprava

## 📋 Co bylo vytvořeno/opraveno

### 1. **Data Modely**
- ✅ `Data/Grade.cs` - Model pro známky žáků
- ✅ `Data/Schedule.cs` - Model pro rozvrh hodin
- ✅ Aktualizován `Data/ApplicationDbContext.cs` - Přidány DbSety a relace

### 2. **Kontrolery**
- ✅ `Controllers/TeacherController.cs` - Správa známek pro učitele
- ✅ `Controllers/StudentController.cs` - Zobrazení rozvrhu a známek pro žáky

### 3. **Views (Zobrazení)**
- ✅ `Views/Teacher/Grades.cshtml` - Formulář a tabulka pro zadávání známek
- ✅ `Views/Student/Schedule.cshtml` - Zobrazení týdenního rozvrhu hodin
- ✅ `Views/Student/Grades.cshtml` - Zobrazení svých známek pro žáky

### 4. **Databáze**
- ✅ Migrace `AddGradesAndSchedules` vytvořena a aplikována
- ✅ Nové tabulky `Grades` a `Schedules` vytvořeny
- ✅ Automatické vytvoření demo rozvrhu pro nové žáky

### 5. **Navigace**
- ✅ Aktualizován `Views/Shared/_Layout.cshtml`
- ✅ Přímý odkaz na "Moje Známky" pro žáky v dropdown menu
- ✅ Přímý odkaz na "Zadávání Známek" pro učitele v dropdown menu

## 🎓 Jak funguje - Učitelé

### Přístup
- Přihlášenému učiteli se zobrazí v navigaci "Moje Známky"
- Nebo cesta: `/Teacher/Grades`

### Funkcionalita
1. **Formulář pro zadání nové známky**:
   - Vybrat třídu (např. 1.A)
   - Vybrat předmět (např. Matematika)
   - Zadat ID žáka
   - Vybrat známku (1-5)
   - Kliknutí na "Zadat Známku"

2. **Tabulka s přehledem**:
   - Zobrazuje všechny zadané známky
   - Sloupce: Žák, Třída, Předmět, Známka, Zadáno, Akce
   - Možnost smazat známku

3. **Logování**:
   - Každé zadání známky se zaloguje do `ILogger`

## 👤 Jak funguje - Žáci

### Přístup na Rozvrh
- V navigaci "Můj Rozvrh" nebo dropdown menu
- Cesta: `/Student/Schedule`

### Funkcionalita Rozvrhu
1. **Přepínače dní v týdnu**:
   - "Všechny dny" - zobrazuje celý týden
   - "Pondělí" až "Pátek" - zobrazuje jednotlivý den

2. **Jednotlivá lekcí**:
   - Čas (začátek - konec)
   - Název předmětu
   - Jméno učitele
   - Místnost/Učebna
   - Třída

3. **Demo Data**:
   - Pokud žák nemá rozvrh, systém automaticky vytvoří demo rozvrh
   - Obsahuje 10 lekcí rozprostřených přes týden

### Přístup na Známky
- V dropdown menu "Moje Známky"
- Cesta: `/Student/Grades`

### Funkcionalita Známek
1. **Statistika**:
   - Celkem známek
   - Průměr
   - Nejlepší známka
   - Nejhorší známka

2. **Tabulka se známkami**:
   - Sloupce: Předmět, Třída, Učitel, Známka, Zadáno
   - Barevné badges podle výsledku
   - Seřazeno od nejnovější

## 🔐 Bezpečnost & Kontroly

### Kontrola Rolí
```csharp
// Pouze autentifikovaní uživatelé
[Authorize]

// Pouze pro učitele
if (!user.IsTeacher) return Forbid();

// Pouze pro žáky
if (user.IsTeacher) return Forbid();
```

### Kontrola Vlastnictví
- Učitel vidí pouze své známky
- Žák vidí pouze svůj rozvrh a své známky

## 📊 Databázové Tabulky

### Grades
```
Id (int) - primární klíč
StudentId (string) - cizí klíč na AspNetUsers
TeacherId (string) - cizí klíč na AspNetUsers
SubjectName (string) - název předmětu
ClassName (string) - třída
GradeValue (int) - hodnota 1-5
CreatedAt (datetime2) - kdy byla zadána
UpdatedAt (datetime2) - kdy byla upravena
```

### Schedules
```
Id (int) - primární klíč
StudentId (string) - cizí klíč na AspNetUsers
ClassName (string) - třída
DayOfWeek (int) - 0=Pondělí až 4=Pátek
StartTime (time) - čas začátku
EndTime (time) - čas konce
SubjectName (string) - název předmětu
TeacherName (string) - jméno učitele
Classroom (string) - místnost
```

## 🧪 Testování

### Test 1: Žák si prohlíží rozvrh
1. Registrujte se jako žák (bez checkboxu učitele)
2. Klikněte na "Můj Rozvrh" v menu
3. Měli byste vidět týdenní rozvrh
4. Klikněte na různé dny pro filtraci

### Test 2: Žák si prohlíží své známky
1. Jako žák jděte do dropdown menu
2. Klikněte na "Moje Známky"
3. Měli byste vidět statistiku (budou prázdná, než vám učitel zadá známky)

### Test 3: Učitel zadá známku
1. Registrujte se jako učitel (zaškrtněte checkbox + kód UCITEL2026)
2. Klikněte na "Moje Známky" v menu
3. Vyplňte formulář:
   - Třída: 1.A
   - Předmět: Matematika
   - ID žáka: (ID z prvního testu)
   - Známka: 2 (Dobře)
4. Klikněte "Zadat Známku"
5. Měla by se zobrazit v tabulce

### Test 4: Ověření v žákově profilu
1. Přihlaste se zpět jako žák
2. Jděte do "Moje Známky"
3. Měli byste vidět nově zadanou známku od učitele
4. Statistika by měla být aktualizovaná

## 📁 Soubory a Struktury

```
Controllers/
├── TeacherController.cs        (nový)
└── StudentController.cs        (nový)

Data/
├── Grade.cs                    (nový)
├── Schedule.cs                 (nový)
└── ApplicationDbContext.cs     (upraveno)

Views/
├── Teacher/
│   └── Grades.cshtml          (nový)
├── Student/
│   ├── Schedule.cshtml         (nový)
│   └── Grades.cshtml          (nový)
└── Shared/
    └── _Layout.cshtml         (upraveno)

Migrations/
└── [timestamp]_AddGradesAndSchedules.cs (nová)
```

## 🐛 Typické Problémy & Řešení

### Problem: "Strany není nalezena" (404)
**Řešení**: Ujistěte se, že:
1. Build je úspěšný (`dotnet build`)
2. Migrace byla aplikována (`dotnet ef database update`)
3. Kontroler ma správné atributy (`[Route("Student")]`)

### Problem: "Přístup odepřen" (Forbid)
**Řešení**: 
1. Ujistěte se, že jste přihlášeni
2. Učitelé nemohou přistupovat na `/Student/*`
3. Žáci nemohou přistupovat na `/Teacher/*`

### Problem: Žák nemá rozvrh
**Řešení**:
1. Poprvé si žák prohlédne rozvrh → automaticky se vytvoří demo rozvrh
2. Pokud ne, kontaktujte správce

## 🔄 Migrační Příkazy

Pokud byste chtěli znovu migrovat:

```bash
# Undo poslední migrace
dotnet ef migrations remove

# Vytvoření nové migrace
dotnet ef migrations add AddGradesAndSchedules

# Aplikace na databázi
dotnet ef database update

# Rollback posledního updatu
dotnet ef database update PreviousMigration
```

## ✨ Bonusové Funkce

- ✅ Demo rozvrh automaticky vytvoří žákovi při prvním přístupu
- ✅ Barevné badges pro různé výsledky
- ✅ Responsivní design na mobilech
- ✅ Ikony z Bootstrap Icons
- ✅ Smooth transitions a animace
- ✅ Filtrování rozvrhu podle dne
- ✅ Statistika známek (průměr, nejlepší, nejhorší)

## 🎯 Příští Kroky (TODO)

- [ ] Přidat filtry podle třídy/předmětu v tabulce známek
- [ ] Exportovat rozvrh do PDF
- [ ] Exportovat známky do CSV
- [ ] Notifikace pro rodiče
- [ ] Archivace starých známek
- [ ] Detailní analytika výkonu
