# 🎨 Vylepšení UI - Přihlášení & Role Viditelnosti

## Co se změnilo

Kompletně jsem přepracoval uživatelské rozhraní tak, aby bylo **jasně vidět, kdo je přihlášen a jakou má roli**.

### 1. **Status Bar (Informační Lišta)**
- **Horní barevný pruh** (fialový/modrý gradient) viditelný když je uživatel přihlášen
- Zobrazuje:
  - 👤 Přihlášeného uživatele (email)
  - 🎓 Jeho roli (Administrátor / Učitel / Žák)
- Umístění: **Hned pod adresním řádkem prohlížeče**

### 2. **Moderní Navigační Bar**
- **Gradient design** (fialový → purpurový)
- Bílý text a ikony
- Dynamické menu podle role:
  - **Učitelé**: Vidí "Moje Známky" v menu
  - **Žáci**: Vidí "Můj Rozvrh" v menu
  - **Administrátoři**: Vidí admin funkce
- Responsivní hamburger menu na mobilech

### 3. **Vylepšený User Badge (Tlačítko Profilu)**
```
┌─────────────────────┐
│ 🎓 jan.novak@...   │
│ Učitel              │
└─────────────────────┘
```

Tlačítko obsahuje:
- **Ikonu podle role**:
  - 🎓 Mortarboard = Učitel
  - 👤 Person = Žák
  - 🛡️ Shield = Administrátor
- **Email uživatele**
- **Barevný badge s rolí** (modrý/zelený/červený)
- **Efektní hover animace**

### 4. **Dropdown Menu**
V menu jsou nyní přímé zkratky:
- Můj profil
- Nastavení
- **Pro učitele**: Zadávání Známek
- **Pro žáky**: Můj Rozvrh, Moje Známky
- **Pro adminy**: Správa kódů, Seznam učitelů
- Odhlásit se

## Technické Implementace

### Nový Service: ApplicationClaimsPrincipalFactory
Automaticky přidává `IsTeacher` claim do user claims při přihlášení.

```csharp
// V Program.cs
.AddClaimsPrincipalFactory<ApplicationClaimsPrincipalFactory>()
```

### Detekce Role v Layoutu
```csharp
bool isTeacher = User.FindFirst("IsTeacher")?.Value == "True";
bool isAdmin = userEmail?.Contains("admin") ?? false;
```

## CSS Vylepšení

### Barvy podle Role
- **Učitel** (Teacher): Modrá (#3b82f6)
- **Žák** (Student): Zelená (#22c55e)
- **Administrátor** (Admin): Červená (#ef4444)

### Animace
- Hover efekty na tlačítka
- Smooth transitions
- Responsive design na mobilech

## Soubory, které Byly Upraveny

### Nové:
- `Services/ApplicationClaimsPrincipalFactory.cs` - Claims factory

### Upravené:
- `Views/Shared/_Layout.cshtml` - Kompletní redesign
- `Controllers/AccountController.cs` - Přidání claims
- `Program.cs` - Registrace ClaimsPrincipalFactory
- `wwwroot/css/site.css` - Responsive CSS

## Jak to Vypadá

### Bez Přihlášení
```
[Logo] SPŠ Trutnovská     Navigace        [Registrace] [Přihlásit se]
```

### S Přihlášením (Žák)
```
✅ Přihlášen jako: jan.novak@spstrutnovska.cz     👤 Žák
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
[Logo] SPŠ Trutnovská  |Můj Rozvrh|Menu...    👤 jan.novak@... [Žák▼]
```

### S Přihlášením (Učitel)
```
✅ Přihlášen jako: katerina.nemecka@spstrutnovska.cz     🎓 Učitel
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
[Logo] SPŠ Trutnovská  |Moje Známky|Menu...    🎓 katerina.nemecka@... [Učitel▼]
```

### S Přihlášením (Admin)
```
✅ Přihlášen jako: admin@spstrutnovska.cz     🛡️ Administrátor
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
[Logo] SPŠ Trutnovská  |Menu...    🛡️ admin@spstrutnovska.cz [Admin▼]
```

## Funkce Dropdown Menu

Kliknutím na user badge se otevře menu s volbami:

**Pro Žáky:**
- Můj Rozvrh (přímý odkaz)
- Moje Známky
- Odhlásit se

**Pro Učitele:**
- Zadávání Známek (přímý odkaz)
- Odhlásit se

**Pro Adminy:**
- Správa kódů učitelů
- Seznam učitelů
- Odhlásit se

## Výhody Nového Designu

✅ **Jasná Role Viditelnost** - Vždy vidíte, kdo jste  
✅ **Barevná Diferenciace** - Barvy porovídají rolím  
✅ **Intuitvní Navigace** - Role-specific menu items  
✅ **Moderní Design** - Gradient, animace, shadow efekty  
✅ **Responsivní** - Funguje na všech zařízeních  
✅ **Přístupnost** - Ikony + text pro snadné pochopení  

## Bezpečnost

Role detekce je založena na:
1. **IsTeacher field** v databázi (ApplicationUser.IsTeacher)
2. **Claims** které se přidávají automaticky
3. **Email kontrola** pro adminy (obsahuje "admin")

Toto je bezpečné, protože:
- Claims jsou vázány na authentifikovaného uživatele
- Role se kontrolují na serveru
- UI je jen vizuální reprezentace

## Testing

Pro otestování:

1. **Registrujte se jako Žák**
   - Bez checkboxu "Registruji se jako učitel"
   - Budete vidět "👤 Žák" v status baru

2. **Registrujte se jako Učitel**
   - Zaškrtněte checkbox
   - Zadejte kód: `UCITEL2026`
   - Budete vidět "🎓 Učitel" v status baru

3. **Registrujte se jako Admin**
   - Email s "admin": `admin@spstrutnovska.cz`
   - Budete vidět "🛡️ Administrátor" v status baru
