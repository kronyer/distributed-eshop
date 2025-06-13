namespace Catalog.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/products");

            //GET all
            group.MapGet("/", async (ProductService productService) =>
            {
                var products = await productService.GetAllProductsAsync();
                return Results.Ok(products);
            })
                .WithName("GetAllProducts")
                .Produces<IEnumerable<Product>>(StatusCodes.Status200OK);

            //GET by id
            group.MapGet("/{id:int}", async (int id, ProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            })
                .WithName("GetProductById")
                .Produces<Product>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            //POST
            group.MapPost("/", async (Product product, ProductService productService) =>
            {
                if (product is null)
                {
                    return Results.BadRequest("Product cannot be null.");
                }
                await productService.AddProductAsync(product);
                return Results.Created($"/products/{product.Id}", product);
            })
                .WithName("AddProduct")
                .Produces<Product>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);

            //PUT
            group.MapPut("/{id:int}", async (int id, Product productDto, ProductService productService) =>
            {
                if (productDto is null)
                {
                    return Results.BadRequest("Product cannot be null.");
                }
                var product = await productService.GetProductByIdAsync(id);
                if (product is null)
                {
                    return Results.NotFound();
                }
                await productService.UpdateProductAsync(product, productDto);
                return Results.NoContent();
            })
                .WithName("UpdateProduct")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest);

            //DELETE
            group.MapDelete("/{id:int}", async (int id, ProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(id);
                if (product is null)
                {
                    return Results.NotFound();
                }
                await productService.DeleteProductByIdAsync(id);
                return Results.NoContent();
            })
                .WithName("DeleteProduct")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);

            //SUPPORT
            group.MapGet("/support/{query}", async (string query, ProductAIService productService) =>
            {
                var response = await productService.SupportAsync(query);
                if (response is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(response);
            })
                .WithName("Support")
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status200OK);

        }
    }
}
