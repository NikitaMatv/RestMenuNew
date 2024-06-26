//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestBoss.Components
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Delivery = new HashSet<Delivery>();
            this.Order_Meal = new HashSet<Order_Meal>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> DateTimesSt { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> OptionsID { get; set; }
        public Nullable<int> Code { get; set; }
        public Nullable<int> Price { get; set; }
        public string Address { get; set; }
        public Nullable<int> TableId { get; set; }
        public Nullable<System.DateTime> DataTimeEnd { get; set; }
        public Nullable<int> DiscountId { get; set; }
        public Nullable<int> DiscountPrice { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> FunktionId { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Delivery> Delivery { get; set; }
        public virtual DiscountCode DiscountCode { get; set; }
        public virtual Funktoin Funktoin { get; set; }
        public virtual Options Options { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Meal> Order_Meal { get; set; }
        public virtual StatusOrder StatusOrder { get; set; }
        public virtual Tables Tables { get; set; }
    }
}
