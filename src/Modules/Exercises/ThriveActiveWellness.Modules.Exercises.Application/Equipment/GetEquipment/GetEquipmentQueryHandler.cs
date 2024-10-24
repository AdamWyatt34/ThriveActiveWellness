using System.Data.Common;
using Dapper;
using ThriveActiveWellness.Common.Application.Data;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Domain.Equipment;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.GetEquipment;

public class GetEquipmentQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetEquipmentQuery, EquipmentResponse>
{

    public async Task<Result<EquipmentResponse>> Handle(GetEquipmentQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = $"""
                            SELECT e.id AS {nameof(EquipmentResponse.EquipmentId)},
                                   e.name AS {nameof(EquipmentResponse.Name)} 
                            FROM  exercises.equipment e 
                            WHERE e.id = @EquipmentId
                            """;
        
        EquipmentResponse? equipment = await connection.QuerySingleOrDefaultAsync<EquipmentResponse>(sql, request);
        
        if (equipment is null)
        {
            return Result.Failure<EquipmentResponse>(EquipmentErrors.NotFound);
        }
        
        return equipment;
    }
}
