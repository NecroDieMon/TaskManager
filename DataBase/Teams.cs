//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManager.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Teams
    {
        public int idTeam { get; set; }
        public string NameTeam { get; set; }
        public int idUser { get; set; }
        public int idTask { get; set; }
    
        public virtual Tasks Tasks { get; set; }
        public virtual Users Users { get; set; }
    }
}
