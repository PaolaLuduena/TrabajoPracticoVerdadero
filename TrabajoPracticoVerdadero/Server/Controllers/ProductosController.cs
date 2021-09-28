using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabajoPracticoVerdadero.Comunes.datos;
using TrabajoPracticoVerdadero.Comunes.datos.Entidades;

namespace TrabajoPracticoVerdadero.Server.Controllers
{
    [ApiController]
    [Route("api/productos")]//ruta del recurso,nombre de controlador paises

    public class ProductosController:ControllerBase//es una clase que se encuentra en Mvc
    { 
        private readonly dbContext context;//DbContex manejador de la base de datos

        public ProductosController(dbContext context)//constructor
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<List<Producto>> Get()
        {
            // Select * from Paises ----- SQL
            return context.Productos.Include(x=> x.Cliente).ToList();//devuelve una lista de paises
        }

        [HttpGet("{id:int}")]
        public ActionResult<Producto> Get(int id)//devuelve un id de pais,sobrecarga de metodos mismo nombre, pero destinta cantidad de parametros
                                                //devuelve un pais

        {
            // Select * from Paises Where Id = id ----- SQL

            //variable que guarda un pais
            var producto = context.Productos.Where(x => x.ID == id).Include(x => x.Cliente).FirstOrDefault();
            //contex es un objeto en la base de datos,paises=tabla paises dentro de la base de datos,x=es un registro de paises que deve cumplir la condicion,

            if (producto == null)
            {
                return NotFound($"No existe el producto con id igual a {id}.");
                //retorna un mensaje de que no encontro el pais
            }
            return producto;
        }

        [HttpPost]
        public ActionResult<Producto> Post(Producto producto)
        {
            try
            {
                context.Productos.Add(producto);//adiciona un pais,agrega un pais
                context.SaveChanges();
                return producto;//retorna pais
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);// si no esta bien el codigo devuelve accion como resultado
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Producto producto)//modificamos un pais
                                                                   //FromBody donde lo quiere poner
        {
            if (id != producto.ID)
            {
                return BadRequest("Datos incorrectos");//retorna un mensaje de error
            }

            var pepe = context.Productos.Where(x => x.ID == id).FirstOrDefault();//verifica si existe la variable pepe
            if (pepe == null)
            {
                return NotFound("no existe el producto a modificar.");
            }
            pepe.CodProducto = producto.CodProducto;
            pepe.NombreProducto = producto.NombreProducto;
            pepe.ClienteID = producto.ClienteID;

            try
            {
                context.Productos.Update(pepe);//actualiza la tabla con los datos nuevos
                context.SaveChanges();//hace un comit de esos datos
                return Ok("Los datos han sido cambiados");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)//borramos un pais
        {
            var producto = context.Productos.Where(x => x.ID == id).FirstOrDefault();
            if (producto == null)
            {
                return NotFound($"No existe el producto con id igual a {id}.");
            }

            try
            {
                context.Productos.Remove(producto);//elimina
                context.SaveChanges();
                return Ok($"El producto {producto.NombreProducto} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }



}

