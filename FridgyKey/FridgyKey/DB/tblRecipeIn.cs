//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FridgyKey.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblRecipeIn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblRecipeIn()
        {
            this.tblRecipeMain = new HashSet<tblRecipeMain>();
        }
    
        public int Id { get; set; }
        public Nullable<int> productID { get; set; }
        public Nullable<int> amount { get; set; }
        public string ei { get; set; }
        public Nullable<int> kolvo { get; set; }
    
        public virtual tblKkal tblKkal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRecipeMain> tblRecipeMain { get; set; }
    }
}
