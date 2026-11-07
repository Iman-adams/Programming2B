using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.API.Models
{
    public class User
    {
        [Key]
        [Column("User_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "Participant";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [StringLength(20)]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }

    public class Venue
    {
        [Key]
        [Column("Venue_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VenueId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Province { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Address { get; set; } = string.Empty;
    }

    public class Event
    {
        [Key]
        [Column("Event_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }

        [Column("Organiser_ID")]
        public int OrganiserId { get; set; }

        [Column("Venue_ID")]
        public int VenueId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Description { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Upcoming";

        [Required]
        [StringLength(50)]
        public string EventType { get; set; } = string.Empty;

        [ForeignKey("OrganiserId")]
        public User? Organiser { get; set; }

        [ForeignKey("VenueId")]
        public Venue? Venue { get; set; }
    }

    public class EventCategory
    {
        [Key]
        [Column("Category_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Column("Event_ID")]
        public int EventId { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(5,2)")]
        public decimal DistanceKm { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal EntryFee { get; set; }

        public int MaxParticipants { get; set; }

        public DateTime StartDateTime { get; set; }

        public int? MinAge { get; set; }

        public string? RouteDescription { get; set; }

        [ForeignKey("EventId")]
        public Event? Event { get; set; }
    }

    public class Enrollment
    {
        [Key]
        [Column("Enrollment_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentId { get; set; }

        [Column("User_ID")]
        public int UserId { get; set; }

        [Column("Category_ID")]
        public int CategoryId { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; } = "Pending";

        [StringLength(20)]
        public string? BibNumber { get; set; }

        [StringLength(50)]
        public string? EmergencyContact { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("CategoryId")]
        public EventCategory? Category { get; set; }
    }

    public class Result
    {
        [Key]
        [Column("Result_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResultId { get; set; }

        [Column("Enrollment_ID")]
        public int EnrollmentId { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal? FinishTimeMinutes { get; set; }

        public int? OverallRank { get; set; }

        public int? Completed { get; set; }

        public int? CategoryRank { get; set; }

        [ForeignKey("EnrollmentId")]
        public Enrollment? Enrollment { get; set; }
    }
}