namespace BillManager.Shared.DTOs
{
    /// <summary>
    /// Clase con el DTO del tipo "Detail"
    /// </summary>
    public class DetailDto
    {
        /// <summary>
        /// Id del detalle
        /// </summary>
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
