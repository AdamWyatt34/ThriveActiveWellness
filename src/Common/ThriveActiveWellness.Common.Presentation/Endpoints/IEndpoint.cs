﻿using Microsoft.AspNetCore.Routing;

namespace ThriveActiveWellness.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}