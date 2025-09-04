using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paper_rock_scissors.Class.Db
{
    public static class UserRepository
    {
        public static async void StoreUser(string name, int score)
        {
            using var db = new UserContext();
            User user = new User { Name = name, Score = score };
            db.Add(user);
            await db.SaveChangesAsync();
        }

        public static async Task<List<User>> GetUsers10BestUsers()
        {
            using var db = new UserContext();
            return await db.Users.OrderByDescending(user => user.Score).Take(10).ToListAsync();
        }
    }
}
