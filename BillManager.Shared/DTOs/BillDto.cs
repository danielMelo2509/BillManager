using System;
using System.Collections.Generic;

namespace BillManager.Shared.DTOs
{
    /// <summary>
    /// Clase con el DTO del tipo "Bill"
    /// </summary>
    public class BillDto
    {
        /// <summary>
        /// Id de la factura
        /// </summary>
        public int IdBill { get; set; }
        /// <summary>
        /// Lista de detalles asociados a una factura
        /// </summary>
        public virtual List<DetailDto> Details { get; set; }
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
