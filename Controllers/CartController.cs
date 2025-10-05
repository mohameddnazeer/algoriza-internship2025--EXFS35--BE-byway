using Microsoft.AspNetCore.Mvc;
using Byway.Api.Services;

namespace Byway.Api.Controllers;

[ApiController]
[Route("api/v1")]
public class CartController : ControllerBase
{
    private readonly CartService _cart;
    private readonly CatalogService _catalog;
    public CartController(CartService cart, CatalogService catalog) { _cart = cart; _catalog = catalog; }

    private string UserId => "u1"; // demo

    [HttpGet("cart")]
    public IActionResult GetCart() => Ok(_cart.GetCart(UserId));

    [HttpPost("cart")]
    public IActionResult Add([FromBody] dynamic body)
    {
        int courseId = (int)body.courseId;
        var course = _catalog.Courses.FirstOrDefault(c => c.Id == courseId);
        if (course is null) return NotFound("Course not found");
        _cart.Add(UserId, courseId, course.Price);
        return Ok(_cart.GetCart(UserId));
    }

    [HttpDelete("cart/{courseId:int}")]
    public IActionResult Remove(int courseId)
    {
        _cart.Remove(UserId, courseId);
        return Ok(_cart.GetCart(UserId));
    }

    [HttpPost("checkout")]
    public IActionResult Checkout()
    {
        const decimal taxRate = 0.15m;
        var items = _cart.GetCart(UserId);
        var subtotal = items.Sum(i => i.UnitPrice);
        var total = subtotal + (subtotal * taxRate);
        var order = new Byway.Api.Models.Order { Id = 1, Subtotal = subtotal, TaxRate = taxRate, Total = total, UserId = int.Parse(UserId), CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
        _cart.Clear(UserId);
        return Ok(order);
    }
}
