﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NFLXV.Membership.Database.Extensions;
using NFLXV.Membership.Database.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NFLXV.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeedController : ControllerBase
{
    private readonly IDbService _db;

    public SeedController(IDbService db) => _db = db;
    // POST api/<SeedController>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IResult> Seed()
    {
        try
        {
            await _db.SeedMembershipData();
            return Results.NoContent();
        }
        catch
        {
           
        }
        return Results.BadRequest();
    }


}
