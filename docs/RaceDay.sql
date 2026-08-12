Create Database RaceDay;
Use RaceDay;

Create table Users (
User_ID int primary key,
FullName varchar(50) not null,
Email varchar(50) not null,
PasswordHash varchar(50) not null,
Role varchar(20) not null,
CreatedAt datetime not null,
Gender varchar(20) null,
DateOfBirth date null
);

Create table Venues (
Venue_ID int primary key,
Name varchar(50) not null,
City varchar(50) not null,
Province varchar(50) not null,
Address varchar(50) not null
);

Create table Events (
Event_ID int primary key,
Organiser_ID int not null,
Venue_ID int not null,
Title varchar(50) not null,
Description varchar(50) not null,
EventDate datetime not null,
Status varchar(50) not null,
EventType varchar(50) not null
Foreign key (Organiser_ID) references Users(User_ID),
Foreign key (Venue_ID) references Venues(Venue_ID)
);

Create table EventCategories (
Category_ID int primary key,
Event_ID int not null,
CategoryName Varchar(50) not null,
DistanceKm decimal(5,2) not null,
EntryFee decimal(10,2) not null,
MaxParticipants int not null,
StartDateTime datetime not null,
MinAge int null,
RouteDescription varchar(max) null,
Foreign key (Event_ID) references Events(Event_ID)
);

Create table Enrollment (
Enrollment_ID int primary key,
User_ID int not null,
Category_ID int not null,
EnrollmentDate datetime not null,
PaymentStatus varchar(50) not null,
BibNumber varchar(20) null,
EmergancyContact varchar(50) null,
Foreign key (User_ID) references Users(User_ID),
Foreign key (Category_ID) references EventCategories(Category_ID)
);

Create table Results (
Result_ID int not null,
Enrollment_ID int not null,
FinishTimeMinutes decimal(6,2) null,
OverallRank int null,
Completed int null,
CategoryRank int null,
Foreign key (Enrollment_ID) references Enrollment(Enrollment_ID)
);

Insert into Users (User_ID, FullName, Email, PasswordHash, Role, CreatedAt, Gender, DateOfBirth) values
(101, 'Iman Adams', 'iman@raceday.co.za', 'pass123', 'Organiser', '2026-01-10', 'Female', '1995-04-12'),
(102, 'Kiersten Smith', 'kiersten@events.co.za', 'pass456', 'Organiser', '2026-01-12', 'Female', '1998-08-20'),
(103, 'Mahmood Mohammad', 'mahmood@gmail.com', 'pass789', 'Participant', '2026-02-01', 'Male', '2000-03-15'),
(104, 'Jamie Abrahams', 'jamie@yahoo.com', 'pass012', 'Participant', '2026-02-05', 'Male', '2002-11-30');

Insert into Venues (Venue_ID, Name, City, Province, Address) values
(201, 'Green Point Athletics Stadium', 'Cape Town', 'Western Cape', '1 Fritz Sonnenberg Rd'),
(202, 'Cape Town Stadium Grounds', 'Cape Town', 'Western Cape', 'V&A Waterfront');

INSERT INTO Events (Event_ID, Organiser_ID, Venue_ID, Title, Description, EventDate, Status, EventType) VALUES
(301, 101, 201, 'Cape Coastal Marathon', 'Scenic coastal road race', '2026-09-15 06:00:00', 'Upcoming', 'Running'),
(302, 102, 202, 'Community Trail Run', 'Township run event', '2026-10-10 07:00:00', 'Upcoming', 'Running'),
(303, 101, 201, 'Peninsula Cycle Challenge', 'Scenic coastal cycle', '2026-11-05 06:30:00', 'Upcoming', 'Cycling');

Insert into EventCategories (Category_ID, Event_ID, CategoryName, DistanceKm, EntryFee, MaxParticipants, StartDateTime, MinAge, RouteDescription) values
(401, 301, 'Full Marathon', 42.20, 350.00, 2000, '2026-09-15 06:00:00', 18, 'Coastal route along Atlantic seaboard'),
(402, 302, '10km Road Run', 10.00, 120.00, 1500, '2026-10-10 07:00:00', 14, 'Flat street circuit'),
(403, 303, '45km Cycle', 45.00, 400.00, 2500, '2026-11-05 06:30:00', 16, 'Highway loop and coastal road');

Insert into Enrollment (Enrollment_ID, User_ID, Category_ID, EnrollmentDate, PaymentStatus, BibNumber, EmergancyContact) values
(501, 103, 401, '2026-03-01', 'Paid', '1001', '0821234567'),
(502, 104, 402, '2026-03-05', 'Paid', '2005', '0839876543');

Insert into Results (Result_ID, Enrollment_ID, FinishTimeMinutes, OverallRank, Completed, CategoryRank) values
(601, 501, 215.45, 42, 1, 12),
(602, 502, 48.12, 10, 1, 3);

Select * from Users;
Select * from Venues;
Select * from Events;
Select * from EventCategories;
Select * from Enrollment;
Select * from Results;