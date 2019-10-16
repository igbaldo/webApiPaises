﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPaises.Models;

namespace WebApiPaises.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Pais")]
    public class PaisController : Controller
    {
        private readonly ApplicationDbContext context;

        public PaisController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Pais> Get()
        {
            return context.Paises.ToList();
        }

        [HttpGet("{id}", Name = "paisCreado")]
        public IActionResult GetById(int id)
        {
            var pais = context.Paises.Include(x => x.Provincias).FirstOrDefault(x => x.Id == id);

            if (pais == null)
            {
                return NotFound();
            }

            return Ok(pais);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Pais pais)
        {
            if (ModelState.IsValid)
            {
                context.Paises.Add(pais);
                context.SaveChanges();

                return new CreatedAtRouteResult("paisCreado", new { id = pais.Id });
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put([FromBody]Pais pais, int id)
        {
            if (pais.Id == id)
            {
                return BadRequest(ModelState);
            }

            context.Entry(pais).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var pais = context.Paises.FirstOrDefault(x => x.Id == id);

            if (pais == null)
            {
                return NotFound();
            }

            context.Paises.Remove(pais);
            context.SaveChanges();
            return Ok(pais);
        }
    }
}