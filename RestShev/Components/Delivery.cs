
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace RestShev.Components
{

using System;
    using System.Collections.Generic;
    
public partial class Delivery
{

    public int ID { get; set; }

    public Nullable<int> EmployeeID { get; set; }

    public Nullable<int> OrederID { get; set; }



    public virtual Deliverer Deliverer { get; set; }

    public virtual Employee Employee { get; set; }

    public virtual Order Order { get; set; }

}

}
