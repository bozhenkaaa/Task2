

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
public class GenresController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GenresController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGenresAsync()
    {
        var genres = await _unitOfWork.GenreRepository.GetAllAsync();
        var genresDto = _mapper.Map<IEnumerable<GenreDto>>(genres);
        return Ok(genresDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenreByIdAsync(Guid id)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        var genreDto = _mapper.Map<GenreDto>(genre);
        return Ok(genreDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenreAsync([FromBody] GenreDto genreDto)
    {
        var genre = _mapper.Map<Genre>(genreDto);
        await _unitOfWork.GenreRepository.AddAsync(genre);
        await _unitOfWork.CompleteAsync();

        genreDto.Id = genre.Id;
        return CreatedAtRoute(nameof(GetGenreByIdAsync), new { id = genreDto.Id }, genreDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGenreAsync(Guid id, [FromBody] GenreDto genreDto)
    {
        var genreToUpdate = await _unitOfWork.GenreRepository.GetByIdAsync(id);

        if (genreToUpdate == null)
        {
            return NotFound();
        }

        _mapper.Map(genreDto, genreToUpdate);
        _unitOfWork.GenreRepository.Update(genreToUpdate);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenreAsync(Guid id)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        _unitOfWork.GenreRepository.Remove(genre);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}