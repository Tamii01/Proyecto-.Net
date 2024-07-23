
let table = $("#roles").DataTable({
    ajax: {
        url: "https://localhost:7059/api/Roles/BuscarRoles",
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
                    `<td><a href='javascript:EditarRol(${JSON.stringify(row)})' <i class = "fa-solid fa-pen-to-square editarRol"></i></td>` +
                    `<td><a href='javascript:EliminarRol(${JSON.stringify(row)})' <i class = "fa-solid fa-trash eliminarRol"></i></td>`
                return botones;
            }
        }
    ],
    language: {
        url: '//cdn.datatables.net/plug-ins/2.0.8/i18n/es-MX.json',
    },
});

const GuardarRol  = () => {
    $.ajax({
        type: "POST",
        url: "/Roles/RolesAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            debugger
            $('#rolesAddPartial').html(resultado);
            $('#rolModal').modal('show');
        }
        
    });
}


const EditarRol = (row) => {
    $.ajax({
        type: "POST",
        url: "/Roles/RolesAddPartial",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $('#rolesAddPartial').html(resultado);
            $('#rolModal').modal('show');
        }

    });
}

const EliminarRol = (row) => {
    
    Swal.fire({
        title: "Estas seguro?",
        text: "Vas a eliminar al role!",
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
                url: "/Roles/EliminarRol",
                data: JSON.stringify(row),
                contentType: "application/json",
                dataType: "html",
                success: function () {
                    Swal.fire({
                        title: "Eliminado!",
                        text: "Se elimino el role.",
                        icon: "success"
                    });
                    table.ajax.reload();
                }

            });
            
        }
    });


   
}