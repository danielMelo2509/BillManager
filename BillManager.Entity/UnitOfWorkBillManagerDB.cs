using BillManager.Entity.Repositories;
using BillManager.Entity.Repositories.Interfaces;

namespace BillManager.Entity
{
    /// <summary>
    /// Unidad de trabajo con la cual interactuar con la base de datos BillManagerDB
    /// </summary>
    public class UnitOfWorkBillManagerDB : IRepositoriesProvider
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private readonly BillManagerDB ObjDbContext;
        /// <summary>
        /// Objeto a retornar con la instancia del repositorio "Bills"
        /// </summary>
        private BillsRepository ObjBillsRepository;
        /// <summary>
        /// Objeto a retornar con la instancia del repositorio "Details"
        /// </summary>
        private DetailsRepository ObjDetailsRepository;
        /// <summary>
        /// Objeto a retornar con la instancia del repositorio "Products"
        /// </summary>
        private ProductsRepository ObjProductsRepository;

        /// <summary>
        /// Constructor de la clase simple
        /// </summary>
        public UnitOfWorkBillManagerDB()
        {
            ObjDbContext = new BillManagerDB();
        }

        /// <summary>
        /// Si el objeto no tiene una instancia del repositorio "Bills" crea una nueva instancia, la carga y retorna
        /// </summary>
        /// <returns>Instancia del repositorio "Bills"</returns>
        public BillsRepository BillsRepository()
        {
            if (ObjBillsRepository == null)
                ObjBillsRepository = new BillsRepository(ObjDbContext);
            return ObjBillsRepository;
        }

        /// <summary>
        /// Si el objeto no tiene una instancia del repositorio "Details" crea una nueva instancia, la carga y retorna
        /// </summary>
        /// <returns>Instancia del repositorio "Details"</returns>
        public DetailsRepository DetailsRepository()
        {
            if (ObjDetailsRepository == null)
                ObjDetailsRepository = new DetailsRepository(ObjDbContext);
            return ObjDetailsRepository;
        }

        /// <summary>
        /// Si el objeto no tiene una instancia del repositorio "Products" crea una nueva instancia, la carga y retorna
        /// </summary>
        /// <returns>Instancia del repositorio "Products"</returns>
        public ProductsRepository ProductsRepository()
        {
            if (ObjProductsRepository == null)
                ObjProductsRepository = new ProductsRepository(ObjDbContext);
            return ObjProductsRepository;
        }

        /// <summary>
        /// Guarda los cambios realizados a la base de datos 
        /// </summary>
        public void Save() => ObjDbContext.SaveChanges();

        /// <summary>
        /// Libera los recusos consumidos por el contexto y los objetos de la clase
        /// </summary>
        public void Dispose()
        {
            ObjDbContext.Dispose();
            ObjBillsRepository = null;
            ObjDetailsRepository = null;
            ObjProductsRepository = null;
        }
    }
}
