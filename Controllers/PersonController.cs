using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using API_en_NET.Models;
using AppContext = API_en_NET.Context.AppContext;

using Microsoft.AspNetCore.Cors;

namespace API_en_NET.Controllers
{

    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        public readonly AppContext _dbContext;

        public PersonController(AppContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("students")]

        public IActionResult Lista()
        {
            List<Person> lista = new List<Person>();

            try
            {
                lista = _dbContext.Persons.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch(Exception ex) {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }

        [HttpPost]
        [Route("addStudent")]

        public IActionResult AddPerson([FromBody] Person obj)
        {
            try
            {
                _dbContext.Persons.Add(obj);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Registro Insertado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("student/{idPerson:int}")]

        public IActionResult Consultar(int idPerson)
        {
            Person person = _dbContext.Persons.Find(idPerson);

            if(person == null)
            {
                return BadRequest("Persona no encontrada");
            }

            try
            {

                person = _dbContext.Persons.FirstOrDefault();
               
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Registro encontrado", response = person });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }

        [HttpPut]
        [Route("student")]
        public IActionResult Edit([FromBody] Person obj) 
        {
            Person person = _dbContext.Persons.Find(obj.Id);

            if (person == null)
            {
                return BadRequest("Persona no encontrada");
            }

            try
            {
                person.Name = obj.Name is null ? person.Name : obj.Name;
                person.Email = obj.Email is null ? person.Email : obj.Email;
                person.Phone = obj.Phone is null ? person.Phone : obj.Phone;
                person.Language = obj.Language is null ? person.Language : obj.Language;

                _dbContext.Persons.Update(person);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Registro actualizado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("student/{idPerson:int}")]

        public IActionResult Delete(int idPerson) 
        {
            Person person = _dbContext.Persons.Find(idPerson);

            if (person == null)
            {
                return BadRequest("Persona no encontrada");
            }

            try
            {
                _dbContext.Persons.Remove(person);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Registro eliminado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
