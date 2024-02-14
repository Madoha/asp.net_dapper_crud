using asp.netDapperCrudPatrick.Models;
using asp.netDapperCrudPatrick.Services;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace asp.netDapperCrudPatrick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly SuperHeroService _heroService;
        public SuperHeroController(SuperHeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllSuperHeroes()
        {
            var heroes = await _heroService.GetAllHeroes();
            return heroes != null ? Ok(heroes) : NoContent();
        }

        [HttpGet("{heroId}")]
        public async Task<ActionResult<SuperHero>> GetHero(int heroId)
        {
            var hero = await _heroService.GetHero(heroId);
            return hero != null ? Ok(hero) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateHero(SuperHero hero)
        {
            var createHero = await _heroService.CreateHero(hero);
            return createHero != null ? Ok(createHero) : BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero hero)
        {
            var updateHero = await _heroService.UpdateHero(hero);
            return updateHero != null ? Ok(updateHero) : BadRequest();
        }

        [HttpDelete("{heroId}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int heroId)
        {
            var deleteHero = await _heroService.DeleteHero(heroId);
            return deleteHero != null ? Ok(deleteHero) : BadRequest();
        }

    }
}
