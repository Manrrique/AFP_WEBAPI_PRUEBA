using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AFP_WEBAPI_PRUEBA.Data;
using AFP_WEBAPI_PRUEBA.Models;

namespace AFP_WEBAPI_PRUEBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly PRUEBA_AFPContext _context;
        private readonly IPRUEBA_AFPContextProcedures _contextsp;

        public EmpresasController(PRUEBA_AFPContext context, IPRUEBA_AFPContextProcedures contextsp)
        {
            _context = context;
            _contextsp = contextsp;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresa()
        {
            if (_context.Empresa == null)
            {
                return NotFound();
            }
            return await _context.Empresa.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<GET_EMPRESAResult>>> GetEmpresa(int id)
        {

            var Empresas = await _contextsp.GET_EMPRESAAsync(id);
            if (Empresas.Count == 0 || Empresas is null)
            {
                return NotFound("Empresa no encontrada");
            }

            return Empresas;
        }

        [HttpPost]
        public async Task<ActionResult<Empresa>> PostEmpresa(Empresa empresa)
        {
            if (_context.Empresa == null)
            {
                return Problem("Surgió un problema con la entidad Empresa");
            }
            _context.Empresa.Add(empresa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmpresaExists(empresa.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmpresa", new { id = empresa.Id }, empresa);
        }

        private bool EmpresaExists(int id)
        {
            return (_context.Empresa?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
