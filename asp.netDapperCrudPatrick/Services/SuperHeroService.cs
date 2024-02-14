using asp.netDapperCrudPatrick.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace asp.netDapperCrudPatrick.Services
{
    public class SuperHeroService
    {
        private readonly IConfiguration _configuration;
        public SuperHeroService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<SqlConnection> GetConnection()
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();
            return connection;
        }

        public async Task<IEnumerable<SuperHero>> GetAllHeroes()
        {
            using var connection = await GetConnection();
            return await connection.QueryAsync<SuperHero>("select * from superheroes");
        }

        public async Task<SuperHero> GetHero(int heroId)
        {
            using var connection = await GetConnection();
            return await connection.QueryFirstAsync<SuperHero>("select * from superheroes where id = @Id", new { Id = heroId });
        }

        public async Task<IEnumerable<SuperHero>?> CreateHero(SuperHero hero)
        {
            using var connection = await GetConnection();
            var result = await connection.ExecuteAsync("insert into superheroes (name, firstname, lastname, place) values (@Name, @FirstName, @LastName, @Place)", hero);
            return result > 0 ? await GetAllHeroes() : null;
        }

        public async Task<IEnumerable<SuperHero>?> UpdateHero(SuperHero hero)
        {
            using var connection = await GetConnection();
            var result = await connection.ExecuteAsync("update superheroes set name = @Name, firstname = @FirstName, lastname = @LastName, place = @Place where id = @Id", hero);
            return result > 0 ? await GetAllHeroes() : null;
        }

        public async Task<IEnumerable<SuperHero>> DeleteHero(int heroId)
        {
            using var connection = await GetConnection();
            var result = await connection.ExecuteAsync("delete from superheroes where id = @Id", new { Id = heroId });
            return result > 0 ? await GetAllHeroes() : null;
        }
    }
}
