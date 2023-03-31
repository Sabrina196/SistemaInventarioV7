let datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $('#tblDatos').DataTable({
        /* Lenguaje de la Tabla */ 
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
        /* LLamada al Método ObtenerTodos */
        "ajax": {
            "url": "/Admin/Marca/ObtenerTodos",
        },
        "columns": [
            { "data": "nombre", "width": "20%" },
            {
                "data": "estado",
                "render": function (data) {
                    if (data == true) {
                        return  `<p class='text-primary fw-bold'>Activo</p>`;
                    }
                    else {
                        return `<p class='text-danger fw-bold'>Inactivo</p>`;
                    }
                }, "width": "20%" 
            },
            {
                "data": "id",
                "render": function (data)
                {
                    return `
                    <div class="text-center">
                        <a href="/Admin/Marca/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                            <i class="bi bi-pencil-square"></i>
                        </a>

                        <a onclick=Delete("/Admin/Marca/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                            <i class="bi bi-trash3-fill"></i>
                        </a>

                    </div>

                    `;
                }, "width":"20%",
            }
        ]
    })
}

/*Función Delete */
function Delete(url) {
    swal({
        /*Le doy estilo a la ventana */
        title: "¿Está seguro de Eliminar la Marca?",
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
                        toastr.success(data.message);
                    }
                }
            });
        }
    });

}