namespace BillManager.Core.Factory
{
   /// <summary>
   /// Fábrica que provee de distintos recursos de Core
   /// </summary>
   public class FactoryCore
    {
        /// <summary>
        /// Constructor simple de la fábrica
        /// </summary>
        public FactoryCore() { }

        /// <summary>
        /// Fabrica una instancia del proveedor de fachadas
        /// </summary>
        /// <returns>Instancia del proveedor de fachadas</returns>
        public FacadesProvider FacadesProvider() => new FacadesProvider(this);
    }
}
