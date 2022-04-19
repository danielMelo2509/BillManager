using System;

namespace BillManager.Entity.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz proveedora que define los distintos tipos de repositorios del sistema
    /// </summary>
    public interface IRepositoriesProvider : IDisposable
    {
        /// <summary>
        /// Construye una instancia del repositorio del tipo "Bills"
        /// </summary>
        /// <returns>Instancia del repositorio del tipo "Bills"</returns>
        BillsRepository BillsRepository();
        /// <summary>
        /// Construye una instancia del repositorio del tipo "Details"
        /// </summary>
        /// <returns>Instancia del repositorio del tipo "Details"</returns>
        DetailsRepository DetailsRepository();
        /// <summary>
        /// Construye una instancia del repositorio del tipo "Products"
        /// </summary>
        /// <returns>Instancia del repositorio del tipo "Products"</returns>
        ProductsRepository ProductsRepository();
        /// <summary>
        /// Guarda todos los cambios realizados a la base de datos
        /// </summary>
        void Save();
    }
}
