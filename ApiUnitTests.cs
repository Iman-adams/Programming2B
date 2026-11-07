using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Controllers;
using RaceDay.API.Data;
using RaceDay.API.Models;
using Xunit;

namespace RaceDay.Tests
{
    public class ApiUnitTests
    {
        // Helper method to create a clean In-Memory database context for each test
        private RaceDayDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<RaceDayDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new RaceDayDbContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        // Helper method to simulate session state for roles
        private DefaultHttpContext GetHttpContextWithSession(string role, int userId)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Session = new TestSession();
            httpContext.Session.SetString("UserRole", role);
            httpContext.Session.SetInt32("UserId", userId);
            return httpContext;
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsCreated()
        {
            // Arrange
            var db = GetDatabaseContext();
            var controller = new AuthController(db);
            var newUser = new User
            {
                FullName = "Test Runner",
                Email = "runner@raceday.co.za",
                PasswordHash = "hashedpassword123",
                Role = "Participant"
            };

            // Act
            var result = await controller.Register(newUser);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task CreateEvent_ParticipantRole_ReturnsForbidden403()
        {
            // Arrange
            var db = GetDatabaseContext();
            var controller = new EventsController(db);

            // Simulate a logged-in 'Participant' attempting to create an event (Organiser-only feature)
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = GetHttpContextWithSession("Participant", 103)
            };

            var illegalEvent = new Event
            {
                Title = "Unauthorized Run",
                Description = "Participants cannot create events",
                EventType = "Running",
                Status = "Upcoming",
                OrganiserId = 103,
                VenueId = 1
            };

            // Act
            var result = await controller.PostEvent(illegalEvent);

            // Assert - Should return 403 Forbidden or BadRequest depending on policy filter
            var actionResult = Assert.IsType<ObjectResult>(result.Result ?? result);
            Assert.True(actionResult.StatusCode == 403 || actionResult.StatusCode == 400);
        }

        [Fact]
        public async Task GetVenues_ReturnsListOfVenues()
        {
            // Arrange
            var db = GetDatabaseContext();
            db.Venues.Add(new Venue { VenueId = 201, Name = "Green Point Stadium", City = "Cape Town", Province = "Western Cape", Address = "1 Fritz Sonnenberg Rd" });
            await db.SaveChangesAsync();

            var controller = new VenuesController(db);

            // Act
            var result = await controller.GetVenues();

            // Assert
            var venues = Assert.IsAssignableFrom<IEnumerable<Venue>>(result.Value);
            Assert.Single(venues);
        }
    }

    // Mock Session class required for testing controller session states
    public class TestSession : ISession
    {
        private readonly Dictionary<string, byte[]> _sessionStorage = new();
        public bool IsAvailable => true;
        public string Id => "TestSession";
        public IEnumerable<string> Keys => _sessionStorage.Keys;

        public void Clear() => _sessionStorage.Clear();
        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public void Remove(string key) => _sessionStorage.Remove(key);
        public void Set(string key, byte[] value) => _sessionStorage[key] = value;
        public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value!);
    }
}