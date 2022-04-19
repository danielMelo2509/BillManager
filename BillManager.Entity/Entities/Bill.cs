using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillManager.Entity.Entities
{
    /// <summary>
    /// Clase que modela la entidad del tipo "Bill"
    /// </summary>
    public class Bill
    {
        /// <summary>
        /// Id de la factura
        /// Llave primaria autoincremental
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBill { get; set; }
        /// <summary>
        /// Llave foranea a la tabla Details
        /// </summary>
        [ForeignKey("IdBill")]
        public virtual ICollection<Detail> Details { get; set; }
        /// <summary>
        /// Número de la factura
        /// </summary>
        public int BillNumber { get; set; }
        /// <summary>
        /// Fecha de la factura
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Tipo de pago
        /// </summary>
        public string PaymentType { get; set; }
        /// <summary>
        /// Documento del cliente
        /// </summary>
        public long CustomerDocument { get; set; }
        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Subtotal de la factura
        /// </summary>
        public long Subtotal { get; set; }
        /// <summary>
        /// Descuento a aplicar a la factura
        /// </summary>
        public int Discount { get; set; }
        /// <summary>
        /// Impuesto de valor agregado
        /// </summary>
        public int IVA { get; set; }
        /// <summary>
        /// Descuento total a la factura
        /// </summary>
        public double TotalDiscount { get; set; }
        /// <summary>
        /// Total de impuestos
        /// </summary>
        public double TotalTax { get; set; }
        /// <summary>
        /// Total de la factura
        /// </summary>
        public double Total { get; set; }
    }
}
