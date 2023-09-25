using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Task2.DTOs;
using Task2.Models;
using Task2.Services;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthorsAsync()
    {
        var authors = await _unitOfWork.AuthorRepository.GetAllAsync();
        var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);
        return Ok(authorsDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorByIdAsync(Guid id)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        var authorDto = _mapper.Map<AuthorDto>(author);
        return Ok(authorDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthorAsync([FromBody] AuthorDto authorDto)
    {
        var author = _mapper.Map<Author>(authorDto);
        await _unitOfWork.AuthorRepository.AddAsync(author);
        await _unitOfWork.CompleteAsync();

        authorDto.Id = author.Id;
        return CreatedAtRoute(nameof(GetAuthorByIdAsync), new { id = authorDto.Id }, authorDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthorAsync(Guid id, [FromBody] AuthorDto authorDto)
    {
        var authorToUpdate = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

        if (authorToUpdate == null)
        {
            return NotFound();
        }

        _mapper.Map(authorDto, authorToUpdate);
        _unitOfWork.AuthorRepository.Update(authorToUpdate);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthorAsync(Guid id)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        _unitOfWork.AuthorRepository.Remove(author);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}