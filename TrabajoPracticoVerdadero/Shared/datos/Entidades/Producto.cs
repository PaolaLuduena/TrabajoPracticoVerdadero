using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoVerdadero.Comunes.datos.Entidades
{
    [Index(nameof(CodProducto), Name = "UQ_Product_CodProducto", IsUnique = true)]

    public class Producto
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "el codigo de producto es obligatorio.")]
        [MaxLength(2, ErrorMessage = "el campo tiene como maximo {1} caracteres.")]

        public string CodProducto { get; set; }

        [Required(ErrorMessage = "el Nombre de producto es obligatorio.")]
        [MaxLength(120, ErrorMessage = "el campo tiene como maximo {1} caracteres.")]

        public string NombreProducto { get; set; }


         [Required(ErrorMessage = "el producto es obligatorio.")]
         public int ClienteID { get; set; }
         public Cliente Cliente { get; set; }



    }
}
