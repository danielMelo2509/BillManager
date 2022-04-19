using System.Linq;
using BillManager.Entity.Shared.Repositories.Interfaces;
using System.Data.Entity;

namespace BillManager.Entity.Shared.Repositories
{
    /// <summary>
    /// Clase que implementa los métodos genéricos de la base de datos
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidad con la cual interactuar</typeparam>
    /// <typeparam name="TDbContext">Tipo de contexto con el cual interactuar</typeparam>
    public class GenericRepository<TEntity, TDbContext> : IGenericRepository<TEntity> where TEntity : class where TDbContext : DbContext
    {
        /// <summary>
        /// DbSet de la entidad a interactuar
        /// </summary>
        protected DbSet<TEntity> ObjDbSet;
        /// <summary>
        /// Contexto con el cual interactuar
        /// </summary>
        protected TDbContext ObjDbContext;

        /// <summary>
        /// Constructor de la clase compuesto
        /// </summary>
        /// <param name="PrmObjDbContext">Contexto con el cual interactuar</param>
        public GenericRepository(TDbContext PrmObjDbContext)
        {
            ObjDbContext = PrmObjDbContext;
            ObjDbSet = ObjDbContext.Set<TEntity>();
        }

        /// <summary>
        /// Inserta un nuevo elemento a la base de datos
        /// Este método no invoca SaveChanges, este debe ser invocado por aparte
        /// </summary>
        /// <param name="PrmObjEntity">Objeto a insertar</param>
        public void Insert(TEntity PrmObjEntity) => ObjDbSet.Add(PrmObjEntity);

        /// <summary>
        /// Elimina un elemento de la base de datos
        /// Este método no invoca SaveChanges, este debe ser invocado por aparte
        /// </summary>
        /// <param name="PrmObjEntity">Objeto a eliminar</param>
        public void Delete(TEntity PrmObjEntity)
        {
            ObjDbSet.Attach(PrmObjEntity);
            ObjDbSet.Remove(PrmObjEntity);
            ObjDbContext.Entry(PrmObjEntity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Actualiza un elemento de la base de datos
        /// Este método no invoca SaveChanges, este debe ser invocado por aparte
        /// </summary>
        /// <param name="PrmObjEntity">Objeto a actualizar</param>
        public void Update(TEntity PrmObjEntity) => ObjDbContext.Entry(PrmObjEntity).State = EntityState.Modified;

        /// <summary>
        /// Obtiene un elemento por medio de su llave primaria
        /// </summary>
        /// <param name="PrmIds">Id a localizar</param>
        /// <returns>Elemento que coincide con el parámetro</returns>
        public TEntity GetById(params object[] PrmIds) => ObjDbSet.Find(PrmIds);

        /// <summary>
        /// Obtiene todos los elementos de una tabla
        /// </summary>
        /// <returns>Objeto del tipo IQueryable con los elementos encontrados en la tabla</returns>
        public IQueryable<TEntity> GetAll() => ObjDbSet;
    }
}
