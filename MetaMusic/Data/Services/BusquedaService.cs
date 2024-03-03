using MetaMusic.Data.Context;

namespace MetaMusic.Data.Services
{
    public class BusquedaService
    {

        private readonly IMetaMusicDbContext dbContext;
        private readonly IGoogleAuthService googleAuthService;

        public BusquedaService(IMetaMusicDbContext dbContext, IGoogleAuthService googleAuthService)
        {
            this.dbContext = dbContext;
            this.googleAuthService = googleAuthService;
        }
    }
}
