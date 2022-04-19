using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillManager.Entity.Entities
{
    /// <summary>
    /// Clase que modela la entidad del tipo "Product"
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Id del producto
        /// Llave primaria autoincremental
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduct { get; set; }
        /// <summary>
        /// Nombre del producto
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Llave foranea a la tabla Details
        /// </summary>
        [ForeignKey("IdProduct")]
        public virtual ICollection<Detail> Details { get; set; }
    }
}
