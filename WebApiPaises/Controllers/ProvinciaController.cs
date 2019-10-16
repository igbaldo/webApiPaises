using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPaises.Models;

namespace WebApiPaises.Controllers
{
    [Produces("application/json")]
    [Route("api/Pais/{IdPais}/Provincia")]
    public class ProvinciaController : Controller
    {
        public ApplicationDbContext context;

        public ProvinciaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Provincia> GetAll(int idPais)
        {
            return context.Provincias.Where(x => x.IdPais == idPais).ToList();
        }

        [HttpGet("{id}", Name = "provinciaById")]
        public IActionResult GetById(int id)
        {
            var provincia = context.Provincias.FirstOrDefault(x => x.Id == id);

            if (provincia == null)
            {
                return NotFound();
            }

            return Ok(provincia);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                context.Provincias.Add(provincia);
                context.SaveChanges();

                return new CreatedAtRouteResult("provinciaById", new { id = provincia.Id });
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put([FromBody]Provincia provincia, int id)
        {
            if (provincia.Id == id)
            {
                return BadRequest(ModelState);
            }

            context.Entry(provincia).State = EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var provincia = context.Provincias.FirstOrDefault(x => x.Id == id);

            if (provincia == null)
            {
                return NotFound();
            }

            context.Provincias.Remove(provincia);
            context.SaveChanges();
            return Ok(provincia);
        }
    }
}