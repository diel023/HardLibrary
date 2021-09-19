﻿using Hard.Library.Interfaces;
using Hard.Library.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Hard.Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUsersRepository _repository;

        public UsersController(IUsersRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var users = _repository.GetAll();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message
                });
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var user = _repository.GetById(id);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message
                });
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            try
            {
                var user = _repository.Create(model);

                return Created($"api/books/{user.Id}", user);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                    ex.ParamName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message
                });
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(Guid id, [FromBody] User model)
        {
            try
            {
                _repository.Update(id, model);

                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                    ex.ParamName
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new
                {
                    ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message
                });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _repository.Delete(id);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new
                {
                    ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message
                });
            }
        }
    }
}

