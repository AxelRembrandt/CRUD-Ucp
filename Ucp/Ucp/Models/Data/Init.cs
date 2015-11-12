using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Ucp.Models.Data
{
    // Пересоздание и заполнение БД, если модель БД была изменена или БД отсутствует.
    public class Init : DropCreateDatabaseIfModelChanges<UcpContext>
    {
        protected override void Seed(UcpContext context)
        {
            var Statuses = new List<ContractStatus>
            {
                new ContractStatus { ContrStatusVal="Ещё не заключен" },
                new ContractStatus { ContrStatusVal="Заключен" },
                new ContractStatus { ContrStatusVal="Расторгнут" }
            };
            Statuses.ForEach(s => context.Statuses.Add(s));
            context.SaveChanges();

            var Companies = new List<Company>
            {
                new Company { CompanyName="Name1", ContractStatusId=1 },
                new Company { CompanyName="Name2", ContractStatusId=3 },
                new Company { CompanyName="Name3", ContractStatusId=2 },
                new Company { CompanyName="Name4", ContractStatusId=1 }
            };
            Companies.ForEach(c => context.Companies.Add(c));
            context.SaveChanges();
                        
            var Users = new List<User>
            {
                new User { UserName="User1", Login="LUser1", Password="qazplo", CompanyId=1 },
                new User { UserName="User2", Login="LUser2", Password="wsxokm", CompanyId=2 },
                new User { UserName="User3", Login="LUser3", Password="edcijn", CompanyId=2 },
                new User { UserName="User4", Login="LUser4", Password="rfvuhb", CompanyId=4 },
                new User { UserName="User5", Login="LUser5", Password="tgbygv", CompanyId=1 },
                new User { UserName="User6", Login="LUser6", Password="yhntfc", CompanyId=4 },
                new User { UserName="User7", Login="LUser7", Password="ujmrdx", CompanyId=3 },
                new User { UserName="User8", Login="LUser8", Password="iklesz", CompanyId=1 }             
            };
            Users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
        }
    }
}