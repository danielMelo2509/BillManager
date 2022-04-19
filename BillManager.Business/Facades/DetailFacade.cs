using BillManager.Core.Factory;
using BillManager.Entity;
using BillManager.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillManager.Core.Facades
{
    /// <summary>
    /// Fachada del tipo "Detail" la cual se comunica con el repositorio pertinente para interactuar con los datos de la base de datos
    /// </summary>
    public class DetailFacade
    {
        /// <summary>
        /// Dependencia de la fábrica de core
        /// </summary>
        private readonly FactoryCore ObjFactoryCore;

        /// <summary>
        /// Constructor compuesto de la clase
        /// </summary>
        /// <param name="PrmObjFactoryCore">Instancia de la fábrica de core</param>
        public DetailFacade(FactoryCore PrmObjFactoryCore)
        {
            ObjFactoryCore = PrmObjFactoryCore;
        }

        /// <summary>
        /// Obtiene una lista del tipo DetailDto 
        /// </summary>
        /// <returns>Lista del objeto de transferencia del tipo "Detail"</returns>
        public List<DetailDto> GetAll(UnitOfWorkBillManagerDB PrmObjUofW)
        {
            try
            {
                using (PrmObjUofW)
                {
                    var LstDetails = PrmObjUofW.DetailsRepository().GetAll().ToList();
                    return LstDetails.ConvertAll(ObjDetalle => new DetailDto()
                    {
                        IdDetail = ObjDetalle.IdDetail,
                        IdBill = ObjDetalle.IdBill,
                        IdProduct = ObjDetalle.IdProduct,
                        Quantity = ObjDetalle.Quantity,
                        UnitPrice = ObjDetalle.UnitPrice
                    });
                }
            }
            catch(Exception Ex)
            {
                //Espacio para insights
                return null;
            }
        }
    }
}
