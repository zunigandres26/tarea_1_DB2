using Applicacion.Models;
using Applicacion.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Programa.Models
{
    class Control_Actividad
    {
        public int id_control { get; set; }
        public int id_persona { get; set; }
        public string actividad { get; set; }
        public string fecha { get; set; }
        public string nombrePersona { get => $"{persona.primer_nombre} {persona.primer_apellido}"; }
        public string tipo_persona { get => persona.tipo_persona; }

        public Personas persona
        {
            get
            {
                Database db = new Database();
                return db.getPersona(id_persona);
            }
        }
    }
}
