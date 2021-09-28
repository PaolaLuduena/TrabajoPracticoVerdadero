using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoVerdadero.Comunes.datos.Entidades
{
     [Index(nameof(CodCliente), Name = "UQ_Cliente_CodCliente", IsUnique = true)]

    public class Cliente
    {
            public int ID { get; set; }

            [Required(ErrorMessage = "el codigo de cliente es obligatorio.")]
            [MaxLength(2, ErrorMessage = "el campo tiene como maximo {1} caracteres.")]

            public string CodCliente { get; set; }

             [Required(ErrorMessage = "el cliente es obligatorio.")]
             [MaxLength(120, ErrorMessage = "el campo tiene como maximo {1} caracteres.")]

            public string NombreCliente { get; set; }

             public List<Producto> Productos { get; set; }


    }
}
