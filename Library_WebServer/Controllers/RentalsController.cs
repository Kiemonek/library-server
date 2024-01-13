﻿using Library_WebServer.Database;
using Library_WebServer.Models.Database;
using Library_WebServer.Models.Requests.Rental;
using Library_WebServer.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_WebServer.Controllers;

[ApiController]
[Route("[controller]/")]
public class RentalsController : ControllerBase
{
    private readonly ILogger<RentalsController> _logger;
    private readonly LibraryDbContext _libraryDbContext;

    public RentalsController(ILogger<RentalsController> logger, LibraryDbContext libraryDbContext)
    {
        _logger = logger;
        _libraryDbContext = libraryDbContext;
    }

    [HttpGet]
    [Route("{rentalId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetRental(Guid rentalId)
    {
        //TODO: Add request validation
        var rental = _libraryDbContext.Rentals
            .Include(x => x.LibraryPublication)
            .Include(x => x.LibraryUser)
            .SingleOrDefault(x => x.Id == rentalId);

        if (rental == null)
        {
            return NotFound();
        }

        return Ok(new RentalResponseModel(rental));
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult PostReservation([FromBody] RentalRequestBaseModel rental)
    {
        //TODO: Add request validation
        //TODO: Add db data validation
        RentalDbModel newRental = new RentalDbModel()
        {
            Date = rental.Date,
            IsBorrow = rental.IsBorrow,
            LibraryPublication = _libraryDbContext.Publications.Single(x => x.Id == rental.PublicationId),
            LibraryUser = _libraryDbContext.Users.Single(x => x.Id == rental.UserId)
        };

        _libraryDbContext.Rentals.Add(newRental);

        _libraryDbContext.SaveChanges();

        return Ok(new RentalResponseModel(newRental));
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult PutReservation([FromBody] RentalRequestUpdateModel rental)
    {
        //TODO: Add request validation
        //TODO: Add db data validation
        RentalDbModel newRental = new RentalDbModel()
        {
            Id = rental.Id,
            Date = rental.Date,
            IsBorrow = rental.IsBorrow,
            LibraryPublication = _libraryDbContext.Publications.Single(x => x.Id == rental.PublicationId),
            LibraryUser = _libraryDbContext.Users.Single(x => x.Id == rental.UserId)
        };

        _libraryDbContext.Rentals.Update(newRental);

        _libraryDbContext.SaveChanges();

        return Ok(new RentalResponseModel(newRental));
    }

    [HttpDelete]
    [Route("{rentalId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteReservation(Guid rentalId)
    {
        var rental = _libraryDbContext.Rentals.SingleOrDefault(x => x.Id == rentalId);

        if (rental == null)
        {
            return NotFound();
        }

        _libraryDbContext.Rentals.Remove(rental);
        _libraryDbContext.SaveChanges();

        return Ok();
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetPublications([FromQuery] int? top, [FromQuery] int? skip)
    {
        //TODO: Add request validation
        List<RentalResponseModel> rentals = _libraryDbContext.Rentals
            .Include(p => p.LibraryPublication)
            .Include(p => p.LibraryUser)
            .OrderBy(x => x.Date)
            .Take(top ?? 10)
            .Skip(skip ?? 0)
            .Select(x => new RentalResponseModel(x))
            .ToList();

        return Ok(rentals);
    }
}

