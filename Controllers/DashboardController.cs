using Microsoft.AspNetCore.Mvc;
using Byway.Api.Services;

namespace Byway.Api.Controllers;

[ApiController]
[Route("api/v1/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly CatalogService _catalog;
    public DashboardController(CatalogService catalog) { _catalog = catalog; }

    [HttpGet("summary")]
    public IActionResult Summary()
    {
        return Ok(new
        {
            instructors = _catalog.Instructors.Count,
            categories = _catalog.Categories.Count,
            courses = _catalog.Courses.Count,
            monthlySubscriptionsTotal = 1234.56m
        });
    }
}
