using BillManager.Shared.DTOs;
using System.Linq;

namespace BillManager.Business.Utils
{
    /// <summary>
    /// Clase que obtiene los totales de una factura
    /// </summary>
    public class BillTotalized
    {
        /// <summary>
        /// Subtotal de la factura
        /// </summary>
        public long SubTotal { get; set; }
        /// <summary>
        /// Total descuento de la factura
        /// </summary>
        public double TotalDiscount { get; set; }
        /// <summary>
        /// Total impuesto de la factura
        /// </summary>
        public double TotalTax { get; set; }
        /// <summary>
        /// Total de la factura
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// Constructor de la clase que calcula los totalizados de la factura
        /// </summary>
        /// <param name="PrmObjBillDto">Objeto con los datos de la factura a calcular</param>
        public BillTotalized(BillDto PrmObjBillDto)
        {
            SubTotal = PrmObjBillDto.Details.Sum(x => x.UnitPrice * x.Quantity);
            TotalDiscount = (SubTotal * PrmObjBillDto.Discount) / 100f;
            TotalTax = ((SubTotal - TotalDiscount) * PrmObjBillDto.IVA) / 100f;
            Total = SubTotal - TotalDiscount + TotalTax;
        }
    }
}
