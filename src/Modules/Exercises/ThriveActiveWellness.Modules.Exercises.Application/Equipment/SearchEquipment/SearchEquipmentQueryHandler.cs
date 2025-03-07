using System.Data.Common;
using Dapper;
using ThriveActiveWellness.Common.Application.Data;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.GetEquipment;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.SearchEquipment;

public class SearchEquipmentQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<SearchEquipmentQuery, SearchEquipmentResponse>
{
    public async Task<Result<SearchEquipmentResponse>> Handle(SearchEquipmentQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = $"""
                            SELECT e.id AS {nameof(EquipmentResponse.EquipmentId)},
                                   e.name AS {nameof(EquipmentResponse.Name)} 
                            FROM  exercises.equipment e 
                            WHERE e.name ILIKE @Search
                            ORDER BY e.name
                            OFFSET @Offset
                            LIMIT @PageSize
                            """;
        
        var parameters = new SearchEquipmentParameters(
            string.IsNullOrEmpty(request.Search) ? null : $"%{request.Search}%",
            (request.Page - 1) * request.PageSize,
            request.PageSize);
        
        IEnumerable<EquipmentResponse> equipment = await connection.QueryAsync<EquipmentResponse>(sql, parameters);
        
        const string countSql = $"""
                                 SELECT COUNT(*) 
                                 FROM exercises.equipment e 
                                 WHERE e.name ILIKE @Search
                                 """;
        
        int totalCount = await connection.ExecuteScalarAsync<int>(countSql, new { parameters.Search });
        
        return new SearchEquipmentResponse(request.Page, request.PageSize, totalCount, equipment.ToList());
    }
    
    private sealed record SearchEquipmentParameters(string? Search, int Offset, int PageSize);
}
