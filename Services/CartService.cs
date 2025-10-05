using Byway.Api.Models;

namespace Byway.Api.Services;

public class CartService
{
    private readonly Dictionary<string, List<CartItem>> _carts = new();

    public IReadOnlyList<CartItem> GetCart(string userId)
    {
        if (!_carts.TryGetValue(userId, out var list))
            _carts[userId] = list = new List<CartItem>();
        return list;
    }

    public void Add(string userId, int courseId, decimal price)
    {
        var list = (List<CartItem>)GetCart(userId);
        if (list.Any(i => i.CourseId == courseId)) return;
        list.Add(new CartItem { CourseId = courseId, UnitPrice = price, UserId = int.Parse(userId), CreatedAt = DateTime.UtcNow });
    }

    public void Remove(string userId, int courseId)
    {
        var list = (List<CartItem>)GetCart(userId);
        list.RemoveAll(i => i.CourseId == courseId);
    }

    public void Clear(string userId)
    {
        var list = (List<CartItem>)GetCart(userId);
        list.Clear();
    }
}
