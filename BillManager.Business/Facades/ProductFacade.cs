using BillManager.Core.Factory;
using BillManager.Entity;
using BillManager.Shared.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace BillManager.Core.Facades
{
    /// <summary>
    /// Fachada del tipo "Product" la cual se comunica con el repositorio pertinente para interactuar con los datos de la base de datos
    /// </summary>
    public class ProductFacade
    {
        /// <summary>
        /// Dependencia de la fábrica de core
        /// </summary>
        private readonly FactoryCore ObjFactoryCore;

        /// <summary>
        /// Constructor compuesto de la clase
        /// </summary>
        /// <param name="PrmObjFactoryCore">Instancia de la fábrica de core</param>
        public ProductFacade(FactoryCore PrmObjFactoryCore)
        {
            ObjFactoryCore = PrmObjFactoryCore;
        }

        /// <summary>
        /// Obtiene una lista del tipo ProductDto 
        /// </summary>
        /// <returns>Lista del objeto de transferencia del tipo "Product"</returns>
        public List<ProductDto> GetAll()
        {
            using (var UofW = new UnitOfWorkBillManagerDB())
            {
                var LstProducts = UofW.ProductsRepository().GetAll().ToList();
                return LstProducts.ConvertAll(ObjProduct => new ProductDto()
                {
                    IdProduct = ObjProduct.IdProduct,
                    ProductName = ObjProduct.ProductName
                });
            }
        }
    }
}
