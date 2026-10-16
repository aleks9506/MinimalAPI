using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using ToDo.Application.Common.Handlers;

namespace ToDo.Application.Common.Transformers
{
    public class ApiKeySecurityTransformer:
        IOpenApiDocumentTransformer
    {
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

        public ApiKeySecurityTransformer(
            IAuthenticationSchemeProvider authenticationSchemeProvider
            )
        {
            this._authenticationSchemeProvider = authenticationSchemeProvider;
        }

        public async Task TransformAsync(
            OpenApiDocument openApiDocument,
            OpenApiDocumentTransformerContext context,
            CancellationToken cancellationToken = default
            )
        {
            var authenticationSchemes = await _authenticationSchemeProvider
                .GetAllSchemesAsync();

            if (authenticationSchemes.Any(s => 
                s.Name == ApiKeyAuthenticationHandler.SchemaName))
            {
                openApiDocument.Components ??= new OpenApiComponents();
                openApiDocument.Components.SecuritySchemes ??= 
                    new Dictionary<string, IOpenApiSecurityScheme>();
                openApiDocument.Components.SecuritySchemes.TryAdd(
                    ApiKeyAuthenticationHandler.SchemaName,
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.ApiKey,
                        In = ParameterLocation.Header,
                        Name = "api-key",
                        Description = "API Key required for this endpoint"
                    }
                );

                openApiDocument.Security ??= [];
                openApiDocument.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference(
                            ApiKeyAuthenticationHandler.SchemaName,
                            openApiDocument)] = []
                });
            }
        }
    }
}
