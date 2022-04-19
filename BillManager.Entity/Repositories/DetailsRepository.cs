using BillManager.Entity.Entities;
using BillManager.Entity.Shared.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BillManager.Entity.Repositories
{
    /// <summary>
    /// Repositorio del tipo "Detail" con el cual se puede interactuar con la entidad del tipo mencionado
    /// </summary>
    public class DetailsRepository : GenericRepository<Detail, BillManagerDB>
    {
        /// <summary>
        /// Constructor compuesto de la clase
        /// </summary>
        /// <param name="PrmObjContext">Contexto de la base de datos</param>
        public DetailsRepository(BillManagerDB PrmObjContext) : base(PrmObjContext) { }

        /// <summary>
        /// Obtiene todos los detalles existentes asignadas a una factura específica
        /// </summary>
        /// <param name="PrmIntIdBill">Id de la factura a coorelacionar</param>
        /// <returns>Objeto enumerable del tipo Detail</returns>
        public IEnumerable<Detail> GetByIdBill(int PrmIntIdBill) => ObjDbSet.Where(x => x.IdBill == PrmIntIdBill);

        /// <summary>
        /// Valida si un detalle específico ya existe en la base de datos
        /// </summary>
        /// <param name="PrmObjDetail">Objeto a validar</param>
        /// <returns>True si el detalle ya existe, false en caso contrario</returns>
        public bool ValidateExistence(Detail PrmObjDetail) => ObjDbSet.Any(x => x.IdDetail == PrmObjDetail.IdDetail);
    }
}
