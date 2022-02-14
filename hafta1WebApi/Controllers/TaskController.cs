using hafta1WebApi.DBOperations;
using hafta1WebApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hafta1WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly TaskDbContext _context;
        
        
        public TaskController(TaskDbContext context)
        {
            _context = context;
            
        }

        [HttpGet] // Tarihe göre Sıralama yaparak Getirir. Önce En yakın tarihli olan 
        [CustomActionFilter]
        public ActionResult<List<Tasklar>> Get()
        {
            var tasks = _context.Tasks.OrderBy(t => t.Date).ToList();
            
            return Ok(tasks);

        }
        [HttpGet("{status}")] //Task Aktif ve ya Pasif olanları getirir;
        public ActionResult<List<Tasklar>> GetByStatus(string status)
        {
            var task = _context.Tasks.Where(x => x.Status == status).ToList();
            
            if (task == null)
            {
                return NoContent();
            }
            else { 
                return Ok(task); }
        }
        [HttpGet("search")] //filtreleme 
        [CustomActionFilter]
        public ActionResult<List<Tasklar>> GetByFilter([FromQuery] string search)
        {
            var task = _context.Tasks.Where(x => x.Title.ToLower().Contains(search.ToLower())).ToList();
            if (task == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(task);
            }
        }

        [HttpPost] // Yeni task ekleme
        public ActionResult NewTask([FromBody] Tasklar task)
        {
            try
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
            return StatusCode(201);
        }

        [HttpPut] //Güncelleme

        public ActionResult UpdateTask( [FromForm] Tasklar task)
        {
            var check = _context.Tasks.SingleOrDefault(x => x.Id == task.Id);
            if (check == null) { return NoContent(); }
            else {
                check.Title = task.Title;
                check.Description = task.Description;
                check.Status = task.Status;
                check.Date = task.Date;

                _context.SaveChanges();

                return Ok(check); }
        }

        [HttpPatch("/TaskStatus/{id}")] 
        public ActionResult StatusUpdate(int id, [FromForm] string status)
        {
            var check = _context.Tasks.SingleOrDefault(x => x.Id == id);
            
                Console.WriteLine(status);
                check.Status = status;

                _context.SaveChanges();

                return Ok(check);
            
        }
        [HttpDelete("{id}")] //Task Silme işlemi 

        public ActionResult DeletTask(int id)
        {
            var check = _context.Tasks.SingleOrDefault(x => x.Id == id);
            if (check == null) { return NoContent(); }
            else
            {
                _context.Tasks.Remove(check);
                _context.SaveChanges();

                return Ok();
            }
        }









    }
}
