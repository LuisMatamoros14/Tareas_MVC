﻿let inputArchivoTarea = document.getElementById("archivoATarea");

function manejarClickAgregarArchivoAdjunto() {
    inputArchivoTarea.click();
}

async function manejarSeleccionArchivoTarea(event) {
    const archivos = event.target.files;
    const archivosArreglo = Array.from(archivos);

    const idTarea = tareaEditarVM.id;
    const formData = new FormData();
    for (var i = 0; i < archivosArreglo.length; i++) {
        formData.append("archivos", archivosArreglo[i]);
    }

    const respuesta = await fetch(`${urlArchivos}/${idTarea}`, {
        body: formData,
        method: "POST",
    });

    if (!respuesta.ok) {
        manejarErrorApi(respuesta);
        return;
    } 

    const json = await respuesta.json();
    prepararArchivosAdjuntos(json);
    inputArchivoTarea.value = null;
}

function prepararArchivosAdjuntos(archivosAdjuntos) {
    archivosAdjuntos.forEach(archivoAdjunto => {
        let fechaCreacion = archivoAdjunto.fechaCreacion;
        if (archivoAdjunto.fechaCreacion.indexOf('Z') === -1) {
            fechaCreacion += 'Z';
        }

        const fechaCreacionDT = new Date(fechaCreacion);
        archivoAdjunto.publicado = fechaCreacionDT.toLocaleString();

        tareaEditarVM.archivosAdjuntos.push(new archivoAdjuntoViewModel({...archivoAdjunto, modoEdicion: false }));
    });
}

let tituloArchivoAdjuntoAnterior;
function manejarClickTituloArchivoAdjunto(archivoAdjunto) {
    archivoAdjunto.modoEdicion(true);
    tituloArchivoAdjuntoAnterior = archivoAdjunto.titulo();
    $("[name=txtArchivoAdjuntoTitulo]:visible").focus();
}

async function manejarFocusoutTituloArchivoAdjunto(archivoAdjunto) {
    archivoAdjunto.modoEdicion(false);
    const idTarea = archivoAdjunto.id;

    if (!archivoAdjunto.titulo()) {
        archivo.titulo(tituloArchivoAdjuntoAnterior);
    }

    if (archivoAdjunto.titulo() === tituloArchivoAdjuntoAnterior) {
        return;
    }

    const data = JSON.stringify(archivoAdjunto.titulo());
    const respuesta = await fetch(`${urlArchivos}/${idTarea}`, {
        body: data,
        method: 'PUT',
        headers: {'Content-Type':'application/json'}
    });

    if (!respuesta.ok) {
        manejarErrorApi(respuesta);
    }
}