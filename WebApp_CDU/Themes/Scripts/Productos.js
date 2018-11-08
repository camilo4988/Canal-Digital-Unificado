function VerDetalleProducto(numeroProducto) {
    $.ajax({
        url: urlObtenerDetalleProducto,
        data: "{ numeroProducto: " + numeroProducto + " }",
        type: "POST",
        //async: true,
        contentType: "application/json",
        success: function (data) {
            if (data.Resultado) {
                MostrarErrorOSesionTerminada(data);
            }
            else {
                $("#contenido").html(data);
                Alerta.Cerrar();
            }            
        },
        error: function (jqXmlHttpRequest, textStatus, errorThrown) {
            //console.log(jqXmlHttpRequest.responseText);
            Alerta.MostrarAlerta("Ha ocurrido un error consultando los datos del producto.");
        },
        beforeSend: function () {
            Alerta.MostrarCargador();
        }
    });
}

function ActualizarEstadoProducto(numeroProducto, estado) {
    $.ajax({
        url: urlActualizarEstadoProducto,
        data: "{ numeroProducto: " + numeroProducto + ", estado: '" + estado + "' }",
        type: "POST",
        //async: true,
        contentType: "application/json",
        success: function (data) {
            if (data.Resultado && data.Resultado == "Exito") {
                $("#EstadoProducto").html(estado);
                $("#bloquearProducto").remove();
                Alerta.MostrarAlerta(data.Mensaje);
            }
            else {    
                MostrarErrorOSesionTerminada(data);
            }
        },
        error: function (jqXmlHttpRequest, textStatus, errorThrown) {
            //console.log(jqXmlHttpRequest.responseText);
            Alerta.MostrarAlerta("Ha ocurrido un error actualizando el estado del producto.");
        },
        beforeSend: function () {
            Alerta.MostrarCargador();
        }
    });
}