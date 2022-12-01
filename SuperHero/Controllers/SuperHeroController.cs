using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero.Data;
using SuperHero.Models;

namespace SuperHero.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class SuperHeroController : ControllerBase {
		private readonly DataContext _context;

		public SuperHeroController(DataContext context) {
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<SuperHeros>>> Get() {
			return Ok(await _context.SuperHeroes.ToListAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<SuperHeros>> Get(int id) {
			var hero = await _context.SuperHeroes.FindAsync(id);
			if(hero == null)
				return BadRequest("Hero not found.");
			return Ok(hero);
		}

		[HttpPost]
		public async Task<ActionResult<List<SuperHeros>>> AddHero(SuperHeros hero) {
			_context.SuperHeroes.Add(hero);
			await _context.SaveChangesAsync();

			return Ok(await _context.SuperHeroes.ToListAsync());
		}

		[HttpPut]
		public async Task<ActionResult<List<SuperHeros>>> UpdateHero(SuperHeros request) {
			var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
			if(dbHero == null)
				return BadRequest("Hero not found.");

			dbHero.Name = request.Name;
			dbHero.FirstName = request.FirstName;
			dbHero.LastName = request.LastName;
			dbHero.Place = request.Place;

			await _context.SaveChangesAsync();

			return Ok(await _context.SuperHeroes.ToListAsync());
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<List<SuperHeros>>> Delete(int id) {
			var dbHero = await _context.SuperHeroes.FindAsync(id);
			if(dbHero == null)
				return BadRequest("Hero not found.");

			_context.SuperHeroes.Remove(dbHero);
			await _context.SaveChangesAsync();

			return Ok(await _context.SuperHeroes.ToListAsync());
		}
	}
}
