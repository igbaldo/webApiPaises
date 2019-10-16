using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApiPaises.Models
{
    public class Provincia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        [ForeignKey("Pais")]
        public int IdPais { get; set; }

        [JsonIgnore]
        public Pais Pais { get; set; }
    }
}
