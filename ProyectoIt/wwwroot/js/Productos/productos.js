let token = getCookie("Token");
let table = $("#productos").DataTable({
    ajax: {
        url: "https://localhost:7059/api/Productos/BuscarProducto",
        dataSrc: '',
        headers: { "Authorization": "Bearer " + token }
    },
    columns: [
        { data: 'id', title: 'Id' },
        {
            data: 'imagen', render: function (data) {
                if (data != "" && data != null) {
                    return `<img src="data:image/jpeg;base64,${data}" width="150px" style="border-radius:15px">`;
                } else {
                    return `<img src="/images/Image_not_available.png" width="150px" height="100px" style="background-color:white; border-radius:15px" >`;
                }
            }, title: 'Imagen'
        },
        { data: 'descripcion', title: 'descripcion' },
        { data: 'precio', title: 'precio' },
        { data: 'stock', title: 'stock' },
        {
            data: function (row) {
                return row.activo == true ? "Si" : "No"
            }, title: 'activo'
        },
        {
            data: function (row) {
                var botones =
                    `<td><a href='javascript:EditarProducto(${JSON.stringify(row)})' <i class = "fa-solid fa-pen-to-square editarProducto"></i></td>` +
                    `<td><a href='javascript:EliminarProducto(${JSON.stringify(row)})' <i class = "fa-solid fa-trash eliminarProducto"></i></td>`
                return botones;
            }
        }
    ],
    language: {
        url: '//cdn.datatables.net/plug-ins/2.0.8/i18n/es-MX.json',
    },
});

const GuardarProducto = () => {
    $.ajax({
        type: "POST",
        url: "/Productos/ProductosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $('#productosAddPartial').html(resultado);
            $('#productoModal').modal('show');
        }

    });
}


const EditarProducto = (row) => {
    $.ajax({
        type: "POST",
        url: "/Productos/ProductosAddPartial",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $('#productosAddPartial').html(resultado);
            $('#productoModal').modal('show');
        }

    });
}

const EliminarProducto = (row) => {

    Swal.fire({
        title: "Estas seguro?",
        text: "Vas a eliminar al producto!",
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
                url: "/Productos/EliminarProducto",
                data: JSON.stringify(row),
                contentType: "application/json",
                dataType: "html",
                success: function () {
                    Swal.fire({
                        title: "Eliminado!",
                        text: "Se elimino el producto.",
                        icon: "success"
                    });
                    table.ajax.reload();
                }

            });

        }
    });



}