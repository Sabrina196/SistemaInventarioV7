let datatable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        /*Lenguaje de la Tabla */
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Página",
            "zeroRecords": "Ningún Registro",
            "info": "Mostrar pág _PAGE_ de _PAGES_",
            "infoEmpty": "No hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar: ",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        /* Llamada a la función ObtenerTodos */
        "ajax": {
            "url": "/Admin/Producto/ObtenerTodos"
        },
        "columns": [
            { "data": "numeroSerie"},
            { "data": "descripcion"},
            { "data": "categoria.nombre"},
            { "data": "marca.nombre"},
            {
                "data": "precio",
                "render": function (data) {
                    var peso = data.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                    return `<p class="text-end fw-bold">${peso}</p>`;
                }
            },
            {
                "data": "estado",
                "render": function (data) {
                    if (data == true) {
                        return `<p class="text-success fw-bold">Activo</p>`;
                    }
                    else {
                        return `<p class="text-danger fw-bold"> Inactivo</p>`;
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                        <a href="/Admin/Producto/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                            <i class="bi bi-pencil-square"></i>
                        </a>

                        <a onclick=Delete("/Admin/Producto/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                            <i class="bi bi-trash3-fill"></i>
                        </a>

                    </div>
                    `;
                }, "width": "20%"
            }
        ]
    });
}

/*Función Delete*/
function Delete(url) {
    swal({
        title: "¿Está seguro de Eliminar el Producto?",
        text: "Este registro no se podrá recuperar",
        icon: "warning",
        buttons: {
            cancel: {
                text: "Cancelar",
                value: null,
                visible: true,
                className: "",
                closeModal: true,
            },
            confirm: {
                text: "Eliminar",
                value: true,
                visible: true,
                className: "",
                closeModal: true
            }
        },
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
