using System.Collections.Generic;
using BillManager.Shared.DTOs;

namespace BillManager.Models
{
    /// <summary>
    /// Clase modelo que contiene la lista de productos disponibles y recibe un objeto del tipo BillDto con los datos de la nueva factura a crear
    /// </summary>
    public class BillsCreationModel
    {
        /// <summary>
        /// Objeto con la información de la factura a crear
        /// </summary>
        public BillDto ObjBillDto { get; set; }
        /// <summary>
        /// Lista de productos disponibles en el sistema
        /// </summary>
        public List<ProductDto> LstProducts { get; set; }
    }
}