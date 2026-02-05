using FinWise.API.Data;
using FinWise.API.Dtos;
using FinWise.API.Models;
using FinWise.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinWise.API.Controllers;

    [ApiController]
    [Route("auth")]
public class AuthController : ControllerBase
{
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext db, JwtService jwt)
    {
        _db = db;
        _jwt = jwt; 
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest req)
    {
        var email = req.Email.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(req.Password) || req.Password.Length < 6)
            return BadRequest("Senha Precisa ter no mínimo 6 caracteres.");

        var exists = await _db.Users.AnyAsync(u => u.Email == email);
        if (exists) return Conflict("Email já cadastrado.");

        var user = new User
        {
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
            CreatedAt = DateTime.UtcNow,
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var token = _jwt.GenerateToken(user);
        return Ok(new AuthResponse(token, user.Email));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest req)
    {
        var email = req.Email.Trim().ToLowerInvariant();

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user is null) return Unauthorized("Credenciais inválidas.");

        var ok = BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash);
        if (!ok) return Unauthorized("Credenciais inválidas.");

        var token = _jwt.GenerateToken(user);
        return Ok(new AuthResponse(token, user.Email));
    }
}

