using RecSysApi.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.Context
{
    public static class DbInitializer
    {
        public static void Initialize(RecSysApiContext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var accountId = Guid.NewGuid();
            var users = new User[]
            {
                new User{FirstName="Carson",LastName="Alexander",Email="carson@gmail.com",Password="test1234", 
                    Account = new Account{
                        AccountID = accountId,
                        Name = "CarsonSharedAccount"
                    },
                },
                //new User{UserID = Guid.NewGuid(),FirstName="Meredith",LastName="Alonso",Email="alonso@gmail.com",Password="test1234"},
                //new User{UserID = Guid.NewGuid(),FirstName="Arturo",LastName="Anand",Email="Anand@gmail.com",Password="test1234"},
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
