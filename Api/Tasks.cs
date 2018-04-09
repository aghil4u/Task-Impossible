using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskImpossible.Data;
using TaskImpossible.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskImpossible.Api
{
    [Route("api/[controller]")]
    public class Tasks : ControllerBase
    {
        private readonly ImpossibleContext _context;

        private readonly IHostingEnvironment _environment;

        public Tasks(ImpossibleContext context, IHostingEnvironment env)
        {
            _context = context;
            _environment = env;
        }
        // GET: api/<controller>
        [HttpGet]
        public List<iTask> Get()
        {
            return _context.Tasks.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{s}")]
        public List<iTask> Get(string s, string so, string cf, int? page, double? lt, double? ln, double? r)
        {
            return _context.Tasks.Where(tsk=>tsk.Description.Contains(s)).ToList();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
