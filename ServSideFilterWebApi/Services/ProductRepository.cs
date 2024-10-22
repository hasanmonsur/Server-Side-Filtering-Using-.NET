using Dapper;
using ServSideFilterWebApi.Data;
using ServSideFilterWebApi.DTOs;
using ServSideFilterWebApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ServSideFilterWebApi.Services
{
    public class ProductRepository
    {
        private readonly DapperContext _connection;

        public ProductRepository(DapperContext connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Products>> GetFilteredDataAsync(FilterOptions filters)
        {
            var query = new StringBuilder("SELECT * FROM Products WHERE 1=1");

            // Build dynamic SQL based on filters
            if (!string.IsNullOrEmpty(filters.SearchTerm))
            {
                query.Append(" AND (Name LIKE @SearchTerm OR Description LIKE @SearchTerm)");
            }
            if (filters.MinValue.HasValue)
            {
                query.Append(" AND Price >= @MinValue");
            }
            if (filters.MaxValue.HasValue)
            {
                query.Append(" AND Price <= @MaxValue");
            }
            if (filters.StartDate.HasValue)
            {
                query.Append(" AND EntryDate >= @StartDate");
            }
            if (filters.EndDate.HasValue)
            {
                query.Append(" AND EntryDate <= @EndDate");
            }

            // Define parameters
            var parameters = new
            {
                SearchTerm = $"%{filters.SearchTerm}%",
                filters.MinValue,
                filters.MaxValue,
                filters.StartDate,
                filters.EndDate
            };


            // Execute query using Dapper
            using (var db = _connection.CreateConnection())
            {
                return await db.QueryAsync<Products>(query.ToString(), parameters);
            }
        }
    }
}
