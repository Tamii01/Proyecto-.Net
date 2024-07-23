
let table = $("#servicios").DataTable({
    ajax: {
        url: "https://localhost:7059/api/Servicios/BuscarServicios",
        dataSrc: ''
    },
    columns: [
        { data: 'id', title: 'Id' },
        { data: 'nombre', title: 'Nombre' },
        {
            data: function (row) {
            return row.activo == true ? "Si" : "No"
            }, title: 'activo'
        },
        {
            data: function (row) {
                var botones =
                    `<td><a href='javascript:EditarServicio(${JSON.stringify(row)})' <i class = "fa-solid fa-pen-to-square editarServicio"></i></td>` +
                    `<td><a href='javascript:EliminarServicio(${JSON.stringify(row)})' <i class = "fa-solid fa-trash eliminarServicio"></i></td>`
                return botones;
            }
        }
    ],
    language: {
        url: '//cdn.datatables.net/plug-ins/2.0.8/i18n/es-MX.json',
    },
});

const GuardarServicio  = () => {
    $.ajax({
        type: "POST",
        url: "/Servicios/ServiciosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            debugger
            $('#serviciosAddPartial').html(resultado);
            $('#serviciosModal').modal('show');
        }
        
    });
}


const EditarServicio = (row) => {
    $.ajax({
        type: "POST",
        url: "/Servicios/ServiciosAddPartial",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $('#serviciosAddPartial').html(resultado);
            $('#serviciosModal').modal('show');
        }

    });
}

const EliminarServicio = (row) => {
    
    Swal.fire({
        title: "Estas seguro?",
        text: "Vas a eliminar al servicioe!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Eliminar!",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/Servicios/EliminarServicio",
                data: JSON.stringify(row),
                contentType: "application/json",
                dataType: "html",
                success: function () {
                    Swal.fire({
                        title: "Eliminado!",
                        text: "Se elimino el servicioe.",
                        icon: "success"
                    });
                    table.ajax.reload();
                }

            });
            
        }
    });


   
}