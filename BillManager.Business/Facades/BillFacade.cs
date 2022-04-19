using BillManager.Entity;
using BillManager.Shared.DTOs;
using System.Collections.Generic;
using System.Linq;
using BillManager.Core.Factory;
using System;
using BillManager.Entity.Entities;
using BillManager.Business.Utils;

namespace BillManager.Core.Facades
{
    /// <summary>
    /// Fachada del tipo "Bill" la cual se comunica con el repositorio pertinente para interactuar con los datos de la base de datos
    /// </summary>
    public class BillFacade
    {
        /// <summary>
        /// Dependencia de la fábrica de core
        /// </summary>
        private readonly FactoryCore ObjFactoryCore;

        /// <summary>
        /// Constructor compuesto de la clase
        /// </summary>
        /// <param name="PrmObjFactoryCore">Instancia de la fábrica de core</param>
        public BillFacade(FactoryCore PrmObjFactoryCore) 
        {
            ObjFactoryCore = PrmObjFactoryCore;
        }

        /// <summary>
        /// Obtiene una lista del tipo BillDto 
        /// </summary>
        /// <returns>Lista del objeto de transferencia del tipo "Bill"</returns>
        public List<BillDto> GetAll()
        {
            try
            {
                List<BillDto> LstBillsDto = new List<BillDto>();
                using (var ObjUofW = new UnitOfWorkBillManagerDB())
                {
                    var LstBills = ObjUofW.BillsRepository().GetAll().ToList();
                    var LstDetails = ObjFactoryCore.FacadesProvider().DetailFacade().GetAll(ObjUofW);
                    foreach (var Factura in LstBills)
                    {
                        var FacturamModel = new BillDto()
                        {
                            IdBill = Factura.IdBill,
                            Details = LstDetails.Where(ObjDetalle => ObjDetalle.IdBill == Factura.IdBill).ToList(),
                            BillNumber = Factura.BillNumber,
                            Date = Factura.Date,
                            PaymentType = Factura.PaymentType,
                            CustomerDocument = Factura.CustomerDocument,
                            CustomerName = Factura.CustomerName,
                            Subtotal = Factura.Subtotal,
                            Discount = Factura.Discount,
                            IVA = Factura.IVA,
                            TotalDiscount = Factura.TotalDiscount,
                            TotalTax = Factura.TotalTax,
                            Total = Factura.Total
                        };
                        LstBillsDto.Add(FacturamModel);
                    }
                }
                return LstBillsDto;
            }
            catch(Exception Ex)
            {
                //Espacio para telemetría
                return null;
            }
        }

        /// <summary>
        /// Crea una nueva factura en la base de datos con sus correspondientes detalles
        /// </summary>
        /// <param name="PrmObjBillDto">Objeto con la información de la nueva factura</param>
        /// <returns>True si el proceso fue exitoso, false en caso contrario</returns>
        public bool CreateBill(BillDto PrmObjBillDto)
        {
            try
            {
                var ObjBillTotals = new BillTotalized(PrmObjBillDto);
                var ObjBill = new Bill()
                {
                    BillNumber = PrmObjBillDto.BillNumber,
                    Details = PrmObjBillDto.Details.ConvertAll(ObjDetail => new Detail()
                    {
                        IdProduct = ObjDetail.IdProduct,
                        Quantity = ObjDetail.Quantity,
                        UnitPrice = ObjDetail.UnitPrice
                    }),
                    Date = PrmObjBillDto.Date,
                    PaymentType = PrmObjBillDto.PaymentType,
                    CustomerDocument = PrmObjBillDto.CustomerDocument,
                    CustomerName = PrmObjBillDto.CustomerName,
                    Subtotal = ObjBillTotals.SubTotal,
                    Discount = PrmObjBillDto.Discount,
                    IVA = PrmObjBillDto.IVA,
                    TotalDiscount = ObjBillTotals.TotalDiscount,
                    TotalTax = ObjBillTotals.TotalTax,
                    Total = ObjBillTotals.Total
                };
                using (var UofW = new UnitOfWorkBillManagerDB())
                {
                    UofW.BillsRepository().Insert(ObjBill);
                    UofW.Save();
                    return true;
                }
            }
            catch(Exception Ex)
            {
                //Espacio para insights
                return false;
            }
        }

        /// <summary>
        /// Actualiza una factura y sus correspondientes detalles en la base de datos
        /// </summary>
        /// <param name="PrmObjBillDto">Objeto con la información de la factura a actualizar</param>
        /// <returns>True si el proceso fue exitoso, false en caso contrario</returns>
        public bool UpdateBill(BillDto PrmObjBillDto)
        {
            try
            {
                var ObjBillTotals = new BillTotalized(PrmObjBillDto);
                var ObjBill = new Bill()
                {
                    IdBill = PrmObjBillDto.IdBill,
                    BillNumber = PrmObjBillDto.BillNumber,
                    Date = PrmObjBillDto.Date,
                    PaymentType = PrmObjBillDto.PaymentType,
                    CustomerDocument = PrmObjBillDto.CustomerDocument,
                    CustomerName = PrmObjBillDto.CustomerName,
                    Subtotal = ObjBillTotals.SubTotal,
                    Discount = PrmObjBillDto.Discount,
                    IVA = PrmObjBillDto.IVA,
                    TotalDiscount = ObjBillTotals.TotalDiscount,
                    TotalTax = ObjBillTotals.TotalTax,
                    Total = ObjBillTotals.Total
                };

                using (var UofW = new UnitOfWorkBillManagerDB())
                {
                    UofW.BillsRepository().Update(ObjBill);
                    UpdateBillDetails(UofW, PrmObjBillDto);
                    UofW.Save();
                    return true;
                }
            }
            catch (Exception Ex)
            {
                //Espacio para insights
                return false;
            }
        }

        /// <summary>
        /// Ejecuta la actualización de los detalles de una factura
        /// Si el detalle ya existe lo actualiza 
        /// Si el detalle no existe lo inserta
        /// Si hay un detalle existente en la base de datos que no esté en el objeto a actualizar, este se elimina
        /// </summary>
        /// <param name="PrmUofW">Unidad de trabajo para interactuar con la base de datos</param>
        /// <param name="PrmObjBillDto">Objeto con los datos de la factura a actualizar</param>
        private void UpdateBillDetails(UnitOfWorkBillManagerDB PrmUofW, BillDto PrmObjBillDto)
        {
            var LstDetails = PrmObjBillDto.Details.ConvertAll(ObjDetail => new Detail()
            {
                IdDetail = ObjDetail.IdDetail,
                IdBill = PrmObjBillDto.IdBill,
                IdProduct = ObjDetail.IdProduct,
                Quantity = ObjDetail.Quantity,
                UnitPrice = ObjDetail.UnitPrice
            });
            LstDetails.ForEach(ObjDetail =>
            {
                if (PrmUofW.DetailsRepository().ValidateExistence(ObjDetail))
                    PrmUofW.DetailsRepository().Update(ObjDetail);
                else
                    PrmUofW.DetailsRepository().Insert(ObjDetail);
            });
            var LstActualDetails = PrmUofW.DetailsRepository().GetByIdBill(PrmObjBillDto.IdBill);
            LstActualDetails.ToList().ForEach(ObjExistentDetail =>
            {
                if (!LstDetails.Any(x => x.IdDetail == ObjExistentDetail.IdDetail))
                    PrmUofW.DetailsRepository().Delete(ObjExistentDetail);
            });
        }

        /// <summary>
        /// Elimina una factura de la base de datos por medio del Id de la misma, esto junto con sus respectivos detalles
        /// </summary>
        /// <param name="PrmIntIdBill">Id de la factura que se desea eliminar</param>
        /// <returns>True si el proceso fue exitoso, false en caso contrario</returns>
        public bool DeleteBill(int PrmIntIdBill)
        {
            try
            {
                using (var UofW = new UnitOfWorkBillManagerDB())
                {
                    UofW.BillsRepository().Delete(new Bill()
                    {
                        IdBill = PrmIntIdBill,
                    });
                    UofW.Save();
                    return true;
                }
            }
            catch (Exception Ex)
            {
                //Espacio para insights
                return false;
            }
        }
    }
}
