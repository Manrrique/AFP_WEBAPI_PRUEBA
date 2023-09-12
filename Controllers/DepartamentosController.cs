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
    public class DepartamentosController : ControllerBase
    {
        private readonly PRUEBA_AFPContext _context;
        private readonly IPRUEBA_AFPContextProcedures _contextsp;

        public DepartamentosController(PRUEBA_AFPContext context, IPRUEBA_AFPContextProcedures contextsp)
        {
            _context = context;
            _contextsp = contextsp;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamento()
        {
          if (_context.Departamento == null)
          {
              return NotFound();
          }
            return await _context.Departamento.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<GET_DEPARTAMENTOSResult>>> GetDepartamento(int id)
        {
            var Departamento = await _contextsp.GET_DEPARTAMENTOSAsync(id);
            if (Departamento.Count == 0 || Departamento is null)
            {
                return NotFound("Departamentos no encontrados");
            }

            return Departamento;
        }

        [HttpPost]
        public async Task<ActionResult<Departamento>> PostDepartamento(Departamento departamento)
        {
          if (_context.Departamento == null)
          {
              return Problem("Surgió un problema con la entidad Departamento");
          }
            _context.Departamento.Add(departamento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DepartamentoExists(departamento.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDepartamento", new { id = departamento.Id }, departamento);
        }

        private bool DepartamentoExists(int id)
        {
            return (_context.Departamento?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
