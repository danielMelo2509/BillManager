using System.Linq;

namespace BillManager.Entity.Shared.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para los métodos genéricos con la base de datos
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidad con la cual interacturar</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Inserta un nuevo elemento a la base de datos
        /// Este método no invoca SaveChanges, este debe ser invocado por aparte
        /// </summary>
        /// <param name="PrmObjEntity">Objeto a insertar</param>
        void Insert(TEntity PrmObjEntity);
        /// <summary>
        /// Elimina un elemento de la base de datos
        /// Este método no invoca SaveChanges, este debe ser invocado por aparte
        /// </summary>
        /// <param name="PrmObjEntity">Objeto a eliminar</param>
        void Delete(TEntity PrmObjEntity);
        /// <summary>
        /// Actualiza un elemento de la base de datos
        /// Este método no invoca SaveChanges, este debe ser invocado por aparte
        /// </summary>
        /// <param name="PrmObjEntity">Objeto a actualizar</param>
        void Update(TEntity PrmObjEntity);
        /// <summary>
        /// Obtiene un elemento por medio de su llave primaria
        /// </summary>
        /// <param name="PrmIds">Id a localizar</param>
        /// <returns>Elemento que coincide con el parámetro</returns>
        TEntity GetById(params object[] PrmIds);
        /// <summary>
        /// Obtiene todos los elementos de una tabla
        /// </summary>
        /// <returns>Objeto del tipo IQueryable con los elementos encontrados en la tabla</returns>
        IQueryable<TEntity> GetAll();
    }
}
