﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestWaiter.Components
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RestarauntDeliveryEntities : DbContext
    {
        public RestarauntDeliveryEntities()
            : base("name=RestarauntDeliveryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cotegories> Cotegories { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Deliverer> Deliverer { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<DiscountCode> DiscountCode { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRole { get; set; }
        public virtual DbSet<Ingridient> Ingridient { get; set; }
        public virtual DbSet<Meal> Meal { get; set; }
        public virtual DbSet<Meal_Ingridient> Meal_Ingridient { get; set; }
        public virtual DbSet<Options> Options { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Order_Meal> Order_Meal { get; set; }
        public virtual DbSet<RequestStatus> RequestStatus { get; set; }
        public virtual DbSet<Restaurant> Restaurant { get; set; }
        public virtual DbSet<Status_Order_Meal> Status_Order_Meal { get; set; }
        public virtual DbSet<StatusOrder> StatusOrder { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Supplier_ingridient> Supplier_ingridient { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tables> Tables { get; set; }
    }
}
