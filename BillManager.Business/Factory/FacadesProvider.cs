using BillManager.Core.Facades;

namespace BillManager.Core.Factory
{
    /// <summary>
    /// Clase proveedor de fachadas de Core
    /// </summary>
    public class FacadesProvider
    {
        /// <summary>
        /// Dependencia de la fábrica de core
        /// </summary>
        private readonly FactoryCore ObjFactoryCore;

        /// <summary>
        /// Constructor compuesto de la clase
        /// </summary>
        /// <param name="PrmObjFactoryCore">Instancia de la fábrica de core</param>
        public FacadesProvider(FactoryCore PrmObjFactoryCore) 
        {
            ObjFactoryCore = PrmObjFactoryCore;
        }

        /// <summary>
        /// Provee una instancia de la fachada tipo "Bill"
        /// </summary>
        /// <returns>Instancia de la fachada tipo "Bill"</returns>
        public BillFacade BillFacade() => new BillFacade(ObjFactoryCore);
        /// <summary>
        /// Provee una instancia de la fachada tipo "Detail"
        /// </summary>
        /// <returns>Instancia de la fachada tipo "Detail"</returns>
        public DetailFacade DetailFacade() => new DetailFacade(ObjFactoryCore);
        /// <summary>
        /// Provee una instancia de la fachada tipo "Product"
        /// </summary>
        /// <returns>Instancia de la fachada tipo "Product"</returns>
        public ProductFacade ProductFacade() => new ProductFacade(ObjFactoryCore);
    }
}
