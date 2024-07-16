
let table = $("#usuarios").DataTable({
    ajax: {
        url: "https://localhost:7059/api/Usuarios/BuscarUsuario",
        dataSrc: ''
    },
    columns: [
        { data: 'id', title: 'Id' },
        { data: 'nombre', title: 'Nombre' },
        { data: 'apellido', title: 'Apellido' },
        {
            data: function (row) {
                console.log(row)
            return moment(row.fecha_Nacimiento).format("DD/MM/YYYY")
        }, title: "Fecha de nacimiento"},
        { data: 'mail', title: 'Mail' },
        { data: 'roles.nombre', title: 'Rol' },
        {
            data: function (row) {
            return row.activo == true ? "Si" : "No"
            }, title: 'activo'
        },
        {
            data: function (row) {
                var botones =
                    `<td><a hreft='javascript:EditarUsuario(${JSON.stringify(row)})' <i class = "fa-solid fa-pen-to-square editarUsuario"></i></td>` +
                    `<td><a hreft='javascript:EliminarUsuario(${JSON.stringify(row)})' <i class = "fa-solid fa-trash eliminarUsuario"></i></td>`
                return botones;
            }
        }
    ],
    language: {
        url: '//cdn.datatables.net/plug-ins/2.0.8/i18n/es-MX.json',
    },
});