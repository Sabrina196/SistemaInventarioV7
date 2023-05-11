let datatable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
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
        /*Llamada al Método "ObtenerTodos"*/
        "ajax": {
            "url": "/Inventario/Inventario/ObtenerTodos"
        },
        "columns": [
            { "data": "bodega.nombre" },
            { "data": "producto.descripcion" },
            {
                "data": "producto.costo",
                "render": function (data) {
                    var peso = data.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                    return `<p class="text-end fw-bold">${peso}</p>`;
                }
            },
            { "data": "cantidad", "className":"text-end" },
        ]

    });
}