using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace Ucp.Models.Data
{
    // Схема таблицы со статусами контрактов с компаниями
    public class ContractStatus
    {
        public int ContractStatusId { get; set; }
        [Display(Name = "Статус контракта")]
        public string ContrStatusVal { get; set; }

        public virtual List<Company> Companies { get; set; }
    }

    // Схема таблицы БД со списком компаний и их свойствами
    public class Company
    {
        public int CompanyId { get; set; }

        [Required (ErrorMessage ="Введите название компании")]
        [StringLength(80, ErrorMessage ="Название должно быть длинной от 4 до 80 символов", MinimumLength = 4)]
        [Display(Name = "Название компании")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Статус контракта")]
        public int ContractStatusId { get; set; } // Внешний ключ для связи с ContractStatus

        public virtual List<User> Users { get; set; }
        public virtual ContractStatus ContractStatus { get; set; }
    }        
    
    // Схема таблицы БД со списком пользователей и их свойствами
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Введите имя пользователя")]
        [StringLength(80, ErrorMessage = "Имя должно быть длинной от 4 до 80 символов", MinimumLength = 4)]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Введите логин")]
        [StringLength(20, ErrorMessage = "Логин должен быть длинной от 4 до 20 символов", MinimumLength = 4)]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Введите пароль")]
        [StringLength(20, ErrorMessage = "Пароль должен быть длинной от 4 до 20 символов", MinimumLength = 4)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Название компании")]
        public int CompanyId { get; set; } //Внешний ключ для связи с таблицей Company

        public virtual Company Company { get; set; }
    }

    //Производный контекст
    public class UcpContext : DbContext
    {
        public DbSet<ContractStatus> Statuses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }

        //public UcpContext() : base("UcpDbConnection")
        //{

        //}

        public static UcpContext Create()
        {
            return new UcpContext();
        }

        // Отключение плюрализации имен
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }


}