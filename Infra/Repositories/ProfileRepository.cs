using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ProfileRepository : Repository<Profile>
    {
        public ProfileRepository(DbContext dbContext)
            :base(dbContext)
        {

        }
    }
}
