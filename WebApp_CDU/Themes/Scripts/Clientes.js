$(document).ready(function () {

    $("body").on("click", "#btnAutenticar", function () { ValidarRecapctha(); });

    window.onbeforeunload = ConfirmarSalida;
    window.onunload = CerrarSesion;
});

var onSubmitLogin = function (token) {
    $("#formAutenticacion").submit();
};

function ValidarRecapctha() {
    if ($("#formAutenticacion").valid()) {
        grecaptcha.execute();
    }
}

function DesplegarInfoCliente(data)
{
    if (data.Resultado) {
        grecaptcha.reset();
        Alerta.MostrarAlerta(data.Mensaje);
    }
    else {
        //$("#contenido").html(data);
        //Alerta.Cerrar();
        document.write(data);
    }    
}

function MostrarCargador() {
    Alerta.MostrarCargador();
}

function NotificarErrorAutenticacion(data)
{
    grecaptcha.reset();
    Alerta.MostrarAlerta("Ha ocurrido un error durante la autenticación.");
}

function VerInformacionCliente() {
    $.ajax({
        url: urlObtenerInformacionCliente,
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
            Alerta.MostrarAlerta("Error obteniendo los datos del cliente.");
        },
        beforeSend: function () {
            Alerta.MostrarCargador();
        }
    });
}

function MostrarActualizacionDatos() {
    $.ajax({
        url: urlMostrarActualizacionDatos,
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
            Alerta.MostrarAlerta("Error obteniendo los datos del cliente.");
        },
        beforeSend: function () {
            Alerta.MostrarCargador();
        }
    });
}

function CerrarSesion() {
    $.ajax({
        url: urlCerrarSesion,
        type: "POST",
        async: false,
        contentType: "application/json",
        success: function (data) {
            location.href = location.origin;
        },
        error: function (jqXmlHttpRequest, textStatus, errorThrown) {
            Alerta.MostrarAlerta("Error cerrando la sesion.");
        },
        beforeSend: function () {
            Alerta.MostrarCargador();
        }
    });
}

function ConfirmarSalida() {
    var isSesionAbierta = false;

    $.ajax({
        url: urlIsSesionAbierta,
        type: "POST",
        async: false,
        contentType: "application/json",
        success: function (data) {
            if (data == 'True') {
                isSesionAbierta = true;
            }
        }        
    });

    if (isSesionAbierta) {
        return "Se cerrará la sesión, ¿Desea salir de esta página?";
    }    
}

function MostrarErrorOSesionTerminada(error)
{
    if (error.Resultado == "SessionError") {
        Alerta.MostrarAlertaSesion(error.Mensaje);
    }
    else {
        Alerta.MostrarAlerta(error.Mensaje);
    }
}

function ConfirmacionActualizacionDatos(data) {
    if (data.Resultado && data.Resultado == "Exito") {
        $("#btnActDatos").attr("disabled", true);
        Alerta.MostrarAlerta(data.Mensaje);
    }
    else {
        MostrarErrorOSesionTerminada(data);
    }
}

function NotificarErrorActualización() {
    Alerta.MostrarAlerta("Ha ocurrido un error durante la actualización de tus datos.");
}