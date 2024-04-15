using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace AppSimpleExemple.Produtos
{
    public class ProductRequest
    {
        public string Name { get; set; }
    }

    public class ProductResponse
    {
        public string Name { get; set; }
    }

    public class InserirProdutoRequestValidations : Validator<ProductRequest>
    {
        public InserirProdutoRequestValidations()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }

    [HttpPost("/produtos")]
    [AllowAnonymous]
    public class InserirProdutoEndpoint : Endpoint<ProductRequest, ProductResponse>
    {
        public override async Task HandleAsync(ProductRequest req, CancellationToken ct)
        {
            await SendAsync(new ProductResponse { Name = req.Name });
        }
    }
}
