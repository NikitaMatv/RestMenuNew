//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestMenu.Components
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order_Meal
    {
        public Nullable<int> MealID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public int ID { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> StatusId { get; set; }
    
        public virtual Meal Meal { get; set; }
        public virtual Order Order { get; set; }
        public virtual Status_Order_Meal Status_Order_Meal { get; set; }
    }
}
