var Alerta = {

    ventana: null,
    titulo: "Canal Digital Unificado",

    MostrarAlerta: function(mensaje){
        this.Cerrar();
        this.ventana = alertify.alert(mensaje)
            .set({
                title: this.titulo,
                label: 'Aceptar',
                closable: false,
                maximizable: false,
                resizable: false,
                padding: true,
                basic: false
            });

        //this.ventana.show();
    },

    MostrarCargador: function () {
        this.Cerrar();
        var stringHtml = '<div style="text-align: center;"><img src="Themes/Images/Cargador.gif" alt="Procesando..." height="70" width="70"><br /><br /><p>Procesando tu información...</p></div>';
        this.ventana = alertify.alert(stringHtml)
            .set({
                title: this.titulo,
                closable: false,
                maximizable: false,
                resizable: false,
                padding: false,
                basic: true
            });

        //this.ventana.show();
    },

    Cerrar: function () {
        if (this.ventana != null) {
            this.ventana.close();
        }
    },

    MostrarAlertaSesion: function (mensaje) {
        this.Cerrar();
        this.ventana = alertify.alert(mensaje)
            .set({
                title: this.titulo,
                label: 'Aceptar',
                closable: false,
                maximizable: false,
                resizable: false,
                padding: true,
                basic: false,
                onok: function() { location.href = location.origin; }
            });

        //this.ventana.show();
    },

    PreguntaActualizarEstadoProducto: function (numeroProducto, estado) {
        this.Cerrar();
        this.ventana = alertify.confirm('¿Estás seguro que deseas cambiar el estado de producto a ' + estado + '?')
            .set({
                title: this.titulo,
                labels: { ok: 'Aceptar', cancel: 'Cancelar' },
                closable: false,
                maximizable: false,
                resizable: false,
                padding: true,
                basic: false,
                onok: function () { ActualizarEstadoProducto(numeroProducto, estado); }
            });
    },

    PreguntaCerrarSesion: function () {
        this.Cerrar();
        this.ventana = alertify.confirm('¿Estás seguro que deseas salir?')
            .set({
                title: this.titulo,
                labels: { ok: 'Aceptar', cancel: 'Cancelar' },
                closable: false,
                maximizable: false,
                resizable: false,
                padding: true,
                basic: false,
                onok: function () { CerrarSesion(); }
            });
    }
}