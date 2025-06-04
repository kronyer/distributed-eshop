namespace Basket.Endpoints
{
    public static class BasketEndpoints
    {
        public static void MapBasketEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("basket");

            group.MapGet("/{userName}", async (string userName, BasketService basketService) =>
            {
                var shoppingCart = await basketService.GetBasketAsync(userName);
                return shoppingCart is not null ? Results.Ok(shoppingCart) : Results.NotFound();
            })
                .WithName("GetBasket")
                .WithSummary("Get a user's shopping basket")
                .WithDescription("Retrieves the shopping basket for a specified user.")
                .Produces<ShoppingCart>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .RequireAuthorization();
                

            group.MapPost("/", async (ShoppingCart shoppingCart, BasketService basketService) =>
            {
                await basketService.UpdateBasketAsync(shoppingCart);
                return Results.Created("GetBasket", shoppingCart);
            })
                .WithName("UpdateBasket")
                .WithSummary("Update a user's shopping basket")
                .WithDescription("Updates the shopping basket for a specified user.")
                .Produces<ShoppingCart>(StatusCodes.Status201Created)
                .RequireAuthorization();


            group.MapDelete("/{userName}", async (string userName, BasketService basketService) =>
            {
                await basketService.DeleteBasketAsync(userName);
                return Results.NoContent();
            })
                .WithName("DeleteBasket")
                .WithSummary("Delete a user's shopping basket")
                .WithDescription("Deletes the shopping basket for a specified user.")
                .Produces(StatusCodes.Status204NoContent)
                .RequireAuthorization();

        }
    }
}
