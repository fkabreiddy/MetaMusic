using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static MudBlazor.CategoryTypes;

namespace MetaMusic.Data
{
    public class DbScripts : IDbScripts
    {
        private readonly IMetaMusicDbContext dbContext;

        public DbScripts(IMetaMusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CrearGeneros()
        {
            List<Genero> generos = new List<Genero>
        {
            new Genero {Nombre = "R&B" },
            new Genero {Nombre = "Pop" },
            new Genero {Nombre = "Country" },
            new Genero {Nombre = "Reggae" },
            new Genero {Nombre = "Dance" },
            new Genero {Nombre = "Folk" },
            new Genero {Nombre = "Indie" },
            new Genero {Nombre = "Blues" },
            new Genero {Nombre = "Soul" },
            new Genero { Nombre = "Funk" },
            new Genero { Nombre = "Metal" },
            new Genero { Nombre = "Rock" },
            new Genero { Nombre = "Pop Urbano" },
            new Genero { Nombre = "Regueton" },
            new Genero { Nombre = "Bachata" },
            new Genero { Nombre = "Merengue" },
            new Genero { Nombre = "Electro-Pop" },
            new Genero { Nombre = "Electronica" },
            new Genero { Nombre = "Punk" },
            new Genero { Nombre = "Rap" },
            new Genero { Nombre = "Experimental" },
            new Genero { Nombre = "Hyper-Pop" },
            new Genero { Nombre = "Alternativa" },
            new Genero { Nombre = "Classical" },
            new Genero { Nombre = "Disco" },
            new Genero {Nombre = "Hip-Hop" }
        };


            var x = await dbContext.Generos.ToListAsync();

            if (x is null)
                await dbContext.Generos.AddRangeAsync(generos);

            await dbContext.SaveChangesAsync();


        }

        public async Task CrearRoles()
        {
            Rol normal = new Rol() { Tipo = "Normal" };
            Rol staff = new Rol() { Tipo = "Staff" };

            var x = await dbContext.Roles.ToListAsync();

            if (x is null)
            {
                await dbContext.Roles.AddAsync(normal);
                await dbContext.Roles.AddAsync(staff);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
