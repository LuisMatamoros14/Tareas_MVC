﻿async function manejarErrorApi(respuesta) {
    let mensajeError = '';

    if (respuesta.status == 400) {
        mensajeError = await respuesta.text();
    } else if (respuesta.status == 404) {
        mensajeError = recursoNoEncontrado;
    } else {
        mensajeError = errorInesperado;
    }

    mostrarMensajeError(mensajeError);
}

function mostrarMensajeError(mensaje) {

    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: mensaje
    });
}

function confirmarAccion({ callbackAceptar, callbackCancelar, titulo }) {
    swal.fire({
        title: titulo || "Realmente deseas eliminar la tarea?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si",
        focusConfirm: true
    }).then((resultado)=>{
        if (resultado.isConfirmed) {
            callbackAceptar();
        } else if (callbackCancelar) {
            //algo
            callbackCancelar();
        }
    });
}

function descargarArchivo(url, nombre) {
    var link = document.createElement('a');
    link.download = nombre;
    link.target = "_blank";
    link.href = url;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    delete link;
}