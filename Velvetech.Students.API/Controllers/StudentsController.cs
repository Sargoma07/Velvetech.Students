using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Velvetech.Students.API.Models;
using Velvetech.Students.Domain.Model;
using Velvetech.Students.Domain.Repositories;
using Velvetech.Students.Infrastructure.Pagination;

namespace Velvetech.Students.API.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;

        public StudentsController(
            IStudentRepository repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<PaginationResult<Student>> GetAll([FromQuery] Pagination pagination)
        {
            var paginationQuery = PaginationQuery.Create(
                pagination.Page,
                pagination.PageSize,
                pagination.Filter,
                pagination.FilterBy
            );

            var student = await _repository.GetAll(paginationQuery);

            return _mapper.Map<PaginationResult<Student>>(student);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Student>> GetById(long id)
        {
            var student = await _repository.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return _mapper.Map<Student>(student);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Create([FromBody] StudentDto item)
        {
            var student = Domain.Entities.Student.Create(item);
            await _repository.Add(student);

            return CreatedAtAction(
                nameof(GetById),
                new {id = student.Id},
                new {id = student.Id});
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(long id, [FromBody] StudentDto item)
        {
            if (item.Id != id)
            {
                return Problem(
                    title: "Id не совпадает",
                    detail: "Id не совпадает. Проверьте правильность введенных данных.",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            var student = await _repository.Find(id);

            student.Update(item);
            await _repository.Update(student);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(long id)
        {
            await _repository.Delete(id);
            return NoContent();
        }
    }
}