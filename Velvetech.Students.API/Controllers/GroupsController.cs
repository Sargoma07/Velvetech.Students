using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Velvetech.Students.API.Models;
using Velvetech.Students.Domain.Model;
using Velvetech.Students.Domain.Repositories;

namespace Velvetech.Students.API.Controllers
{
    [ApiController]
    [Route("groups")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GroupsController(
            IGroupRepository groupRepository,
            IStudentRepository studentRepository,
            IMapper mapper
        )
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Group>>> GetAll(string name)
        {
            ICollection groups;

            if (!string.IsNullOrEmpty(name))
            {
                groups = await _groupRepository.GetAll(x => x.Name.ToLower() == name.ToLower());
            }
            else
            {
                groups = await _groupRepository.GetAll();
            }

            return _mapper.Map<Group[]>(groups);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Group>> GetById(long id)
        {
            var group = await _groupRepository.GetById(id);

            if (group == null)
            {
                return NotFound();
            }

            return _mapper.Map<Group>(group);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] GroupDto item)
        {
            var group = Domain.Entities.Group.Create(item);
            await _groupRepository.Add(group);

            return CreatedAtAction(
                nameof(GetById),
                new {id = group.Id},
                new {id = group.Id}
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(long id, [FromBody] GroupDto item)
        {
            if (item.Id != id)
            {
                return Problem(
                    title: "Id не совпадает",
                    detail: "Id не совпадает. Проверьте правильность введенных данных.",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            var group = await _groupRepository.Find(id);

            group.Update(item);
            await _groupRepository.Update(group);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(long id)
        {
            await _groupRepository.Delete(id);
            return NoContent();
        }

        [HttpPost("{groupId}/students")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddStudent(long groupId, [FromBody] StudentDto item)
        {
            var group = await _groupRepository.Find(groupId);
            if (group == null)
            {
                return NotFound();
            }

            var student = await _studentRepository.Find(item.Id);
            if (student == null)
            {
                return Problem(
                    title: "Студент не найден",
                    detail: "Студент не найден. Проверьте правильность введенных данных.",
                    statusCode: StatusCodes.Status404NotFound
                );
            }

            await _groupRepository.AddStudent(group, student);

            return CreatedAtAction(
                nameof(GetById),
                new {id = group.Id},
                new {id = group.Id}
            );
        }

        [HttpDelete("{groupId}/students")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteStudent(long groupId, [FromBody] StudentDto item)
        {
            var group = await _groupRepository.Find(groupId);
            if (group == null)
            {
                return NotFound();
            }

            var student = await _studentRepository.Find(item.Id);
            if (student == null)
            {
                return Problem(
                    title: "Студент не найден",
                    detail: "Студент не найден. Проверьте правильность введенных данных.",
                    statusCode: StatusCodes.Status404NotFound
                );
            }

            await _groupRepository.DeleteStudent(group, student);

            return NoContent();
        }
    }
}