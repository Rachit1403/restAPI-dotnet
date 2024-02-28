using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restAPI_practice.Data;
using restAPI_practice.Entities;

namespace restAPI_practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            var heroes = await _dataContext.SuperHeroes.ToListAsync();

            return Ok(heroes);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if(hero is null) return NotFound("Hero not found!!!");
            return Ok(hero);

        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _dataContext.SuperHeroes.Add(hero);
            await _dataContext.SaveChangesAsync();
            
            //return Ok(await _dataContext.SuperHeroes.ToListAsync());
            return Ok("Hero Added!!!");

        }

        [HttpPut]
        public async Task<ActionResult<String>> UpdateHero(SuperHero hero)
        {
            var dbHero = await _dataContext.SuperHeroes.FindAsync(hero.Id);
            if (dbHero is null)
            {
                return NotFound("Hero not found!!!");
            }

            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;

            await _dataContext.SaveChangesAsync();
            
            //return Ok(await _dataContext.SuperHeroes.ToListAsync());
            return Ok("Hero Updated!!!");

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<String>> DeleteHero(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if (hero is null)
            {
                return NotFound("Hero not found!!!");
            }

            _dataContext.SuperHeroes.Remove(hero);
            await _dataContext.SaveChangesAsync();

            return Ok("Hero Deleted!!!");

        }

    }
}
