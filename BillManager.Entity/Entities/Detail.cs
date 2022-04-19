using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillManager.Entity.Entities
{
    /// <summary>
    /// Clase que modela la entidad del tipo "Detail"
    /// </summary>
    public class Detail
    {
        /// <summary>
        /// Id del detalle
        /// Llave primaria autoincremental
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetail { get; set; }
        /// <summary>
        /// Id de la factura relacionada
        /// </summary>
        public int IdBill { get; set; }
        /// <summary>
        /// Id del producto relacionado
        /// </summary>
        public int IdProduct { get; set; }
        /// <summary>
        /// Cantidad de producto
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Precio unitario
        /// </summary>
        public int UnitPrice { get; set; }
    }
}
