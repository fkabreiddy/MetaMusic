using MetaMusic.Data.Context;
using MetaMusic.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MetaMusic.Data.OtherEntities
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IMetaMusicDbContext dbContext;

        public DbInitializer(IMetaMusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Initialize()
        {

            try
            {
                var generos = await dbContext.Generos.ToListAsync();

                if (generos.Count() <= 0)
                {
                    await dbContext.Generos.AddRangeAsync(

                         new Genero { Nombre = "R&B" },
                         new Genero { Nombre = "Pop" },
                         new Genero { Nombre = "Country" },
                         new Genero { Nombre = "Reggae" },
                         new Genero { Nombre = "Dance" },
                         new Genero { Nombre = "Folk" },
                         new Genero { Nombre = "Indie" },
                         new Genero { Nombre = "Blues" },
                         new Genero { Nombre = "Soul" },
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
                         new Genero { Nombre = "Hip-Hop" },
                         new Genero { Nombre = "Baladas" }
                         );
                    await dbContext.SaveChangesAsync();
                }

                var roles = await dbContext.Roles.ToListAsync();

                if (roles.Count() <= 0)
                {
                    await dbContext.Roles.AddAsync(new Rol() { Tipo = "Normal" });
                    await dbContext.Roles.AddAsync(new Rol() { Tipo = "Staff" });
                    await dbContext.SaveChangesAsync();

                }

                
            }
            catch(Exception e)
            {
                await Console.Out.WriteLineAsync(e.InnerException?.Message??e.Message);
            }
      
        }
    }
}
