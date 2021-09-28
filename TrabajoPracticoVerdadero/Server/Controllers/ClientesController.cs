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
    [Route("api/clientes")]//ruta del recurso,nombre de controlador clientes

    public class ClientesController:ControllerBase//es una clase que se encuentra en Mvc
    { 
        private readonly dbContext context;//DbContex manejador de la base de datos
       
        public ClientesController(dbContext context)//constructor
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<List<Cliente>>Get()
        {
            
            return  context.Clientes.Include(x => x.Productos).ToList();//devuelve una lista de clientes
        }

        [HttpGet("{id:int}")]
        public ActionResult<Cliente>Get(int id)//devuelve un id de cliente,sobrecarga de metodos mismo nombre, pero destinta cantidad de parametros
                                             //devuelve un cliente
        {
            

            //variable que guarda un cliente
            var cliente = context.Clientes.Where(x => x.ID == id).Include(x => x.Productos).FirstOrDefault();
            //contex es un objeto en la base de datos,cliente=tabla cliente dentro de la base de datos,x=es un registro de cliente que deve cumplir la condicion,

            if (cliente == null)
            {
                return NotFound($"No existe el cliente con id igual a {id}.");
                //retorna un mensaje de que no encontro el cliente
            }
            return cliente;
       }

        [HttpPost]
        public ActionResult<Cliente>Post(Cliente cliente)
        {
            try
            {
                context.Clientes.Add(cliente);//adiciona un cliente,agrega un cliente
                context.SaveChanges();
                return cliente;//retorna cliente
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);// si no esta bien el codigo devuelve accion como resultado
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Cliente cliente)//modificamos un cliente
                                                             //FromBody donde lo quiere poner
        {
            if (id != cliente.ID)
            {
                return BadRequest("Datos incorrectos");//retorna un mensaje de error
            }

            var pepe =  context.Clientes.Where(x => x.ID == id).FirstOrDefault();//verifica si existe la variable pepe
            if (pepe == null)
            {
                return NotFound("no existe el cliente a modificar.");
            }
            pepe.CodCliente = cliente.CodCliente;
            pepe.NombreCliente = cliente.NombreCliente;
            try
            {
                context.Clientes.Update(pepe);//actualiza la tabla con los datos nuevos
                context.SaveChanges();//hace un comit de esos datos

                return Ok("Los datos han sido cambiados");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)//borramos un cliente
        {
            var cliente = await context.Clientes.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (cliente == null)
            {
                return NotFound($"No existe el cliente con id igual a {id}.");
            }

            try
            {
                context.Clientes.Remove(cliente);//elimina
                context.SaveChanges();

                return Ok($"El cliente {cliente.NombreCliente} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }








}

