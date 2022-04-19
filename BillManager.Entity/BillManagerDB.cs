using BillManager.Entity.Entities;
using System.Data.Entity;

namespace BillManager.Entity
{
    /// <summary>
    /// Clase contexto de la base de datos BillManagerDB
    /// </summary>
    public class BillManagerDB : DbContext
    {
        /// <summary>
        /// Constructor de la clase simple 
        /// Constuye el objeto de la clase padre con el nombre de la base de datos
        /// </summary>
        public BillManagerDB() : base("BillManagerDB") { }

        /// <summary>
        /// DbSet del tipo Bill
        /// </summary>
        public DbSet<Bill> Bills { get; set; }
        /// <summary>
        /// DbSet del tipo Detail
        /// </summary>
        public DbSet<Detail> Details { get; set; }
        /// <summary>
        /// DbSet del tipo Product
        /// </summary>
        public DbSet<Product> Products { get; set; }

    }
}
