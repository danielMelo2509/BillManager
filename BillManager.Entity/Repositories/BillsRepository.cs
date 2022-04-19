using BillManager.Entity.Entities;
using BillManager.Entity.Shared.Repositories;

namespace BillManager.Entity.Repositories
{
    /// <summary>
    /// Repositorio del tipo "Bill" con el cual se puede interactuar con la entidad del tipo mencionado
    /// </summary>
    public class BillsRepository : GenericRepository<Bill, BillManagerDB>
    {
        /// <summary>
        /// Constructor compuesto de la clase
        /// </summary>
        /// <param name="PrmObjContext">Contexto de la base de datos</param>
        public BillsRepository(BillManagerDB PrmObjContext) : base(PrmObjContext) { }
    }
}
