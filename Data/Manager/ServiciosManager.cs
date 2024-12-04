using Data.Base;
using Data.Dtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Manager
{
    public class ServiciosManager : BaseManager<Servicios>
    {
        public override Task<bool> Borrar(Servicios servicios)
        {
            throw new NotImplementedException();
        }

        public override Task<Servicios> BuscarAsync(LoginDto entity)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<Servicios>> BuscarListaAsync()
        {
            return  contextSingleton.Servicios.FromSqlRaw("ObtenerServicios").ToList();
        }

        public async Task<bool> GuardarServicio (Servicios servicio)
        {
            return contextSingleton.Database.ExecuteSqlRaw($"GuardarServicios {servicio.Id}, {servicio.Nombre}, {servicio.Activo}") > 0;
        }
    }
}
