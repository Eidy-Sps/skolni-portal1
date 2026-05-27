<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
=======
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
>>>>>>> origin/Franta
using Microsoft.EntityFrameworkCore;

namespace Skolni_portal.Data
{
<<<<<<< HEAD
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
=======
    public class ApplicationDbContext : IdentityDbContext
>>>>>>> origin/Franta
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> origin/Franta
