using System;
using System.Web.Mvc;
using BillManager.Models;
using BillManager.Core.Factory;

namespace BillManager.Controllers
{
    /// <summary>
    /// Controlador principal del sistema
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Retorna la vista del administrador de facturas con la lista de facturas existente en la base de datos
        /// </summary>
        /// <returns>Vista con el modelo correspondiente</returns>
        public ActionResult Index()
        {
            var ObjFactoryCore = new FactoryCore();
            var Lst = ObjFactoryCore.FacadesProvider().BillFacade().GetAll();
            var ObjModel = new BillsViewerModel() { LstBillsDto = Lst };
            return View(ObjModel);
        }

        /// <summary>
        /// Retorna la vista parcial del modal para la edición de facturas
        /// El modelo que se retorna lleva precargados los diferentes productos de la base de datos
        /// </summary>
        /// <returns>Vista parcial con el modelo correspondiente</returns>
        [HttpPost]
        public ActionResult EditBillView(BillsCreationModel prmmodel)
        {
            var ObjFactoryCore = new FactoryCore();
            prmmodel.LstProducts = ObjFactoryCore.FacadesProvider().ProductFacade().GetAll();
            return PartialView("~/Views/Modals/BillsData.cshtml", prmmodel);
        }

        /// <summary>
        /// Retorna la vista parcial del modal para la creación de facturas
        /// El modelo que se retorna lleva precargados los diferentes productos de la base de datos
        /// </summary>
        /// <returns>Vista parcial con el modelo correspondiente</returns>
        [HttpGet]
        public ActionResult CreateBillView()
        {
            var ObjFactoryCore = new FactoryCore();
            var ObjModel = new BillsCreationModel()
            {
                LstProducts = ObjFactoryCore.FacadesProvider().ProductFacade().GetAll()
            };
            return PartialView("~/Views/Modals/BillsData.cshtml", ObjModel);
        }

        /// <summary>
        /// Api para crear una nueva factura en el sistema
        /// </summary>
        /// <param name="PrmObjModel">Modelo con los datos de la factura a crear</param>
        /// <returns>True si el proceso fue exitoso, false en caso contrario</returns>
        [HttpPost]
        public bool CreateBills(BillsCreationModel PrmObjModel)
        {
            var ObjFactoryCore  = new FactoryCore();
            try
            {
                var Resultado = ObjFactoryCore.FacadesProvider().BillFacade().CreateBill(PrmObjModel.ObjBillDto);
                return Resultado;
            }
            catch (Exception e) { return false; }
        }

        /// <summary>
        /// Api para actualizar una factura en el sistema
        /// </summary>
        /// <param name="PrmObjModel">Modelo con los datos de la factura a actualizar</param>
        /// <returns>True si el proceso fue exitoso, false en caso contrario</returns>
        [HttpPost]
        public bool UpdateBills(BillsCreationModel PrmObjModel)
        {
            var ObjFactoryCore  = new FactoryCore();
            try
            {
                var Resultado = ObjFactoryCore.FacadesProvider().BillFacade().UpdateBill(PrmObjModel.ObjBillDto);
                return Resultado;
            }
            catch (Exception e) { return false; }
        }

        /// <summary>
        /// Api para eliminar una factura en el sistema
        /// </summary>
        /// <param name="PrmIdBill">Id de la factura a eliminar</param>
        /// <returns>True si el proceso fue exitoso, false en caso contrario</returns>
        [HttpGet]
        public bool DeleteBills(int PrmIdBill)
        {
            var ObjFactoryCore  = new FactoryCore();
            try
            {
                var Resultado = ObjFactoryCore.FacadesProvider().BillFacade().DeleteBill(PrmIdBill);
                return Resultado;
            }
            catch (Exception e) { return false; }
        }
    }
}