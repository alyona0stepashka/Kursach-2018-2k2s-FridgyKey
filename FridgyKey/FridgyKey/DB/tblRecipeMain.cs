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
    
    public partial class tblRecipeMain
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public Nullable<int> ingredID { get; set; }
        public string notation { get; set; }
        public byte[] image { get; set; }
    
        public virtual tblRecipeIn tblRecipeIn { get; set; }
    }
}
