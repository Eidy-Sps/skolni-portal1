# ✅ Změna UI - Zadávání Známek (ID → Email/Jméno)

## 📋 Co se Změnilo

### Starý Stav ❌
```
Zadávání Známky - Formulář:
┌─────────────────┐
│ Třída: 1.A      │
│ Předmět: Matem  │
│ ID Žáka: [123]  │ ← PROBLÉM: Musíme vědět ID
│ Známka: [1-5]   │
└─────────────────┘
```

### Nový Stav ✅
```
Zadávání Známky - Formulář:
┌────────────────────────────────────┐
│ Třída: 1.A                         │
│ Předmět: Matematika                │
│ Žák: [▼ student@spstrutnovska.cz] │ ← DROPDOWN se všemi žáky
│ Známka: [1-5]                      │
└────────────────────────────────────┘
```

## 🔧 Technické Změny

### 1. Controller - Grada.cs

**Nová Logika v GET /Teacher/Grades:**
```csharp
// Získání všech žáků (bez učitelů)
var allStudents = await _userManager.Users
    .Where(u => !u.IsTeacher)
    .OrderBy(u => u.Email)
    .ToListAsync();

ViewBag.Students = allStudents; // Pošle seznam do View
```

**Změna POST /Teacher/AddGrade:**
```csharp
// Staré: AddGrade(int studentId, ...)
// Nové: AddGrade(string studentEmail, ...)

var student = await _userManager.FindByNameAsync(studentEmail);
// Místo: FindByIdAsync(studentId.ToString())
```

### 2. View - Grades.cshtml

**Formulář:**
```html
<!-- Staré: -->
<input type="number" name="studentId" placeholder="ID žáka" />

<!-- Nové: -->
<select name="studentEmail" required>
    <option value="">-- Vyberte žáka --</option>
    @foreach (var student in ViewBag.Students)
    {
        <option value="@student.Email">@student.Email</option>
    }
</select>
```

**Tabulka:**
```html
<!-- Staré: -->
<td>@grade.StudentId</td>

<!-- Nové: -->
<td>@(grade.Student?.Email ?? grade.StudentId)</td>
```

## 📊 Výsledek

### Formulář
```
┌──────────────────────────────────────────────┐
│ Nová Známka                                  │
├──────────────────────────────────────────────┤
│ Třída: [1.A           ]                      │
│ Předmět: [Matematika  ]                      │
│ Žák: [▼ student1@spstrutnovska.cz         ] │
│       [  student2@spstrutnovska.cz         ] │
│       [  student3@spstrutnovska.cz         ] │
│ Známka: [▼ 1 - Výborně ]                    │
│ [✓ Zadat Známku]                            │
└──────────────────────────────────────────────┘
```

### Tabulka Výsledků
```
┌─────────────────────────────────────────────────────┐
│ Žák         │ Třída │ Předmět      │ Známka │ Akce  │
├─────────────────────────────────────────────────────┤
│ student1... │ 1.A   │ Matematika   │ ⓶     │ ✗     │
│ student2... │ 1.A   │ Fyzika       │ ⓷     │ ✗     │
│ student3... │ 1.B   │ Matematika   │ ⓵     │ ✗     │
└─────────────────────────────────────────────────────┘
```

## 🎯 Výhody

✅ **Intuitivnější** - Vidíme email místo ID  
✅ **Bezpečnější** - Nemůžeme zadat nesprávné ID  
✅ **Rychlejší** - Dropdown je rychlejší než psaní  
✅ **Lepší UX** - Vidíme seznam všech žáků  

## 🧪 Test

### Scénář 1: Standardní Zadání

1. Přihlaste se jako učitel
2. Jděte na `/Teacher/Grades`
3. Formulář obsahuje:
   - **Třída**: Text input (1.A)
   - **Předmět**: Text input (Matematika)
   - **Žák**: **DROPDOWN** se všemi žáky
   - **Známka**: Select (1-5)
4. Vyberte žáka z dropdownu
5. Klikněte "Zadat Známku"
6. ✅ Známka se zobrazí v tabulce s jménem žáka (email)

### Scénář 2: Ověření Tabulky

1. V tabulce vidíte:
   - Jméno žáka (email) místo ID
   - Všechny ostatní údaje se nezměnily
   - Možnost smazat (✗ tlačítko)

## 📁 Upravené Soubory

1. **`Controllers/TeacherController.cs`**
   - Metoda `Grades()` - Přidání ViewBag.Students
   - Metoda `AddGrade()` - Změna parametru studentEmail

2. **`Views/Teacher/Grades.cshtml`**
   - Formulář - Input změněn na Select
   - Tabulka - Nadpis "Žák" místo "Žák (ID)"
   - Tabulka - Zobrazení email místo ID

## 🔄 Migration

❌ **Není potřeba** - Databáze se nemění, jen logika aplikace

## 🚀 Deployment

```bash
# Build
dotnet build

# Run
dotnet run

# Test
http://localhost:5000/Teacher/Grades
```

## 💡 Budoucí Vylepšení

- [ ] Filtrování žáků podle třídy
- [ ] Vyhledávání v dropdownu
- [ ] Zobrazení křestního jména místo emailu
- [ ] Hromadné zadání známek (CSV import)

---

**Status**: ✅ **HOTOVO A TESTOVANÉ**
