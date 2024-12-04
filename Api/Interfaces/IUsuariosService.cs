using Data.Dtos;
using Data.Entities;

namespace Api.Interfaces
{
    public interface IUsuariosService
    {
        Task<List<Usuarios>> BuscarUsuarios();
        Task<bool> GuardarUsuario(CrearCuentaDto crearCuentaDto);
    }
}
