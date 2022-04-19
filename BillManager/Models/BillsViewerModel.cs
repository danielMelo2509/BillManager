using System.Collections.Generic;
using BillManager.Shared.DTOs;

namespace BillManager.Models
{
    /// <summary>
    /// Modelo para la vista que permite listar las facturas y ver sus detalles correspondientes
    /// </summary>
    public class BillsViewerModel
    {
        /// <summary>
        /// Lista con los DTOs referentes a los datos y detalles de las facturas registradas en el sistema
        /// </summary>
        public List<BillDto> LstBillsDto { get; set; }
    }
}