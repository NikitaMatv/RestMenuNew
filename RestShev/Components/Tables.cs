
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
    
public partial class Tables
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Tables()
    {

        this.Order = new HashSet<Order>();

    }


    public int Id { get; set; }

    public Nullable<int> NumberOfSeats { get; set; }

    public Nullable<int> EmployeeID { get; set; }



    public virtual Employee Employee { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Order> Order { get; set; }

}

}
