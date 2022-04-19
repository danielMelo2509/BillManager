$(document).ready(function () {

    /*Acción click para abrir el modal de edición de facturas*/
    $('.BtnEditarFactura').click(function () {
        var Modelo = {};
        Modelo.ObjBillDto = $(this).data("billobject");
        $.ajax({
            url: '/Home/EditBillView',
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: '{prmmodel:' + JSON.stringify(Modelo) + '}'
        }).always(function (data) {
            $('#ModalBillViewer').html(data.responseText);
            $('#myModal').modal('show');
        });
    });

    /*Acción click para abrir el modal de creación de facturas*/
    $('.BtnCreateBill').click(function () {
        $.get('/Home/CreateBillView', function (data) {
            $('#ModalBillViewer').html(data);
            $('#myModal').modal('show');
        });
    });

    /*Acción click para eliminar una factura*/
    $('.BtnEliminarFactura').click(function () {
        var NumeroFactura = $(this).data('numero-factura');
        var IdFactura = $(this).data('id-factura');
        var Cuerpo = "La factura con número " + NumeroFactura + " será eliminada del sistema."
        MensajeSwal('¿Deseas eliminar esta factura?', Cuerpo, "", "question", "", true, true, false, 100000, function (result) {
            if (result.value) {

                $.get('/Home/DeleteBills', { PrmIdBill: IdFactura }, function (resultRequest) {
                    if (resultRequest == "True") {
                        Cuerpo = "La factura fue eliminada exitosamente.";
                        MensajeSwal("Proceso exitoso", Cuerpo, "", "success", "", true, false, false, 8000, function () { });
                    }
                    else {
                        Cuerpo = "Hubo un error al eliminar la factura, por favor intenta de nuevo.";
                        MensajeSwal("Proceso exitoso", Cuerpo, "", "error", "", true, false, false, 8000, function () { });
                    }
                });
            }
        });
    });

    /*Acción click para agregar un nuevo detalle a la tabla*/
    $(document).on('click', '#BtnAdd', function () {
        var NombreProducto = $("#DetalleProducto").val();
        var ProductoId = $("#DetalleProducto").find(':selected').data('producto-id');
        var Cantidad = $("#DetalleCantidad").val();
        var ValorUn = $("#DetallePrecioUn").val();

        if (ValidarDatosDetalle(NombreProducto, Cantidad, ValorUn)) {

            $("#DetalleProducto").val($("#DetalleProducto option:first").val());
            $("#DetalleCantidad").val("");
            $("#DetallePrecioUn").val("");

            AgregarDetalleaTabla(NombreProducto, ProductoId, Cantidad, ValorUn);
        }
    });

    /*Acción click para remover un detalle de la tabla*/
    $(document).on('click', '.BtnRemoverDetalle', function () {
        $(this).parent().parent().remove();
    });

    /*Acción click consumir la api de creación de facturas*/
    $(document).on('click', '#Crear', function () {
        var ObjModelo = CrearObjetoFactura(false);
        if (ObjModelo != null) {
            var Cuerpo = "";
            $.post('/Home/CreateBills', { PrmObjModel: ObjModelo }, function (result) {
                if (result == "True") {
                    Cuerpo = "La factura fue calculada y almacenada exitosamente.";
                    MensajeSwal("Proceso exitoso", Cuerpo, "", "success", "", true, false, false, 8000, function () { });
                }
                else {
                    Cuerpo = "Hubo un error al crear la nueva factura, por favor intenta de nuevo.";
                    MensajeSwal("Proceso exitoso", Cuerpo, "", "error", "", true, false, false, 8000, function () { });
                }
            });
        }
    });

    /*Acción click consumir la api de actualización de facturas*/
    $(document).on('click', '#Guardar', function () {
        var ObjModelo = CrearObjetoFactura(true);
        if (ObjModelo != null) {
            var Cuerpo = "";
            $.post('/Home/UpdateBills', { PrmObjModel: ObjModelo }, function (result) {
                if (result == "True") {
                    Cuerpo = "La factura fue actualizada exitosamente.";
                    MensajeSwal("Proceso exitoso", Cuerpo, "", "success", "", true, false, false, 8000, function () { });
                }
                else {
                    Cuerpo = "Hubo un error al actualizar la factura, por favor intenta de nuevo.";
                    MensajeSwal("Proceso exitoso", Cuerpo, "", "error", "", true, false, false, 8000, function () { });
                }
            });
        }
    });

    /*Función que valida el ingreso integral de los campos de detalle*/
    function ValidarDatosDetalle(prmNombreProducto, prmCantidad, prmValorUnitario) {
        var Titulo = "Dato faltante";
        var Cuerpo = "";

        if (!prmNombreProducto) {
            Cuerpo = "Debes ingresar el producto por favor.";
            MensajeSwal(Titulo, Cuerpo, "", "warning", "", true, false, true, 8000, function () { });

            return false;
        }
        else if (!prmCantidad) {
            Cuerpo = "Debes ingresar la cantidad por favor.";
            MensajeSwal(Titulo, Cuerpo, "", "warning", "", true, false, true, 8000, function () { });
            return false;
        }
        else if (!prmValorUnitario) {
            Cuerpo = "Debes ingresar el valor unitario por favor.";
            MensajeSwal(Titulo, Cuerpo, "", "warning", "", true, false, true, 8000, function () { });
            return false;
        }
        return true;
    }

    /*Función que agrega una nueva fila a la tabla de detalles*/
    function AgregarDetalleaTabla(prmNombreProducto, prmIdProducto, prmCantidad, prmValorUnitario) {
        $("#TablaDetalles").find('tbody')
            .append($('<tr>')
                .append($('<td>').addClass("color-black Producto").text(prmNombreProducto).attr('data-id-producto', prmIdProducto))
                .append($('<td>').addClass("color-black Cantidad").text(prmCantidad))
                .append($('<td>').addClass("color-black PrecioUn").text(prmValorUnitario))
                .append($('<td>').addClass("button-table-cell")
                    .append($('<button>').addClass("btn BtnRemoverDetalle")
                        .append($('<i>').addClass("fa fa-remove"))
                    )
                )
            );
    }

    /*Función que construye el objeto de factura para envíar como modelo a la api de creación y edición*/
    function CrearObjetoFactura(EsEdicion) {
        var LstDetails = ObtenerArregloDetalles(EsEdicion);
        if (LstDetails.length != 0) {
            var ObjModeloCreacion = {};
            ObjModeloCreacion.ObjBillDto = {};
            if (EsEdicion)
                ObjModeloCreacion.ObjBillDto.IdBill = $('#NumeroFactura').data("id-factura");
            ObjModeloCreacion.ObjBillDto.Details = LstDetails;
            ObjModeloCreacion.ObjBillDto.BillNumber = $('#NumeroFactura').val();
            ObjModeloCreacion.ObjBillDto.Date = $('#FechaFactura').val();
            ObjModeloCreacion.ObjBillDto.PaymentType = $('#TipoPago').val();
            ObjModeloCreacion.ObjBillDto.CustomerDocument = $('#DocumentoCliente').val();
            ObjModeloCreacion.ObjBillDto.CustomerName = $('#NombreCliente').val();
            ObjModeloCreacion.ObjBillDto.Discount = $('#Descuento').val();
            ObjModeloCreacion.ObjBillDto.IVA = $('#IVA').val();
            ObjModeloCreacion.ObjBillDto.TotalDiscount = $('#TotalDescuento').val();
            ObjModeloCreacion.ObjBillDto.TotalTax = $('#TotalImpuesto').val();
            ObjModeloCreacion.ObjBillDto.Subtotal = $('#Subtotal').val();
            ObjModeloCreacion.ObjBillDto.Total = $('#Total').val();
            return ObjModeloCreacion;
        }
        else {
            var Cuerpo = "Debes ingresar al menos un detalle por favor";
            MensajeSwal("Tabla vacía", Cuerpo, "", "warning", "", true, false, true, 8000, function () { });
            return null;
        }
    }

    /*Función que construye el arreglo (lista) de detalles de una factura*/
    function ObtenerArregloDetalles(EsEdicion) {
        var ArregloDetalles = new Array();
        $("#TablaDetalles tbody tr").each(function () {
            var ObjDetalle = {};
            if (EsEdicion)
                ObjDetalle.IdDetail = $(this).find("td").eq(0).data('id - detalle');
            ObjDetalle.IdProduct = $(this).find("td").eq(0).data('id-producto');
            ObjDetalle.Quantity = $(this).find("td").eq(1).text();
            ObjDetalle.UnitPrice = $(this).find("td").eq(2).text();
            ArregloDetalles.push(ObjDetalle);
        })
        return ArregloDetalles;
    }

    /*Función que permite crear pop-ups de mensajes*/
    function MensajeSwal(Titulo, Cuerpo, MensajeValidator, Tipo, Input, MostrarBotonAceptar, MostrarBotonCancelar, HabilitarClickExterno, Timer, Function) {
        if (Titulo != null)
            Titulo = Titulo.replace(/\"/g, "").replace(/°/g, "&quot;");
        if (Cuerpo != null)
            Cuerpo = Cuerpo.replace(/\"/g, "").replace(/°/g, "&quot;");
        Swal.fire({
            allowOutsideClick: HabilitarClickExterno,
            type: Tipo,
            input: Input,
            title: Titulo,
            html: Cuerpo,
            showCancelButton: MostrarBotonCancelar,
            showConfirmButton: MostrarBotonAceptar,
            confirmButtonText: 'Aceptar',
            cancelButtonText: 'Cancelar',
            timer: Timer,
            inputAttributes: {
                'text-align': 'center'
            },
            onClose: () => {
                if (Tipo == "success") {
                    location.reload();
                }
            }
        }).then(function (dynamicVar) { if (Function != undefined) { Function(dynamicVar); } })
    }
});