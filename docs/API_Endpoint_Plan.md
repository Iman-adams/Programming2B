# RaceDay - API Endpoint Plan

| HTTP Method | Route | Description | Role Required | Request Body | Expected Response |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **POST** | `/api/auth/register` | Registers a new user account | None (Public) | `{"FullName": "String", "Email": "String", "PasswordHash": "String", "Role": "String", "Gender": "String", "DateOfBirth": "Date"}` | `201 Created` - `{"User_ID": 1, "Email": "user@example.com"}`<br>`400 Bad Request`<br>`409 Conflict` |
| **POST** | `/api/auth/login` | Authenticates a user and returns a token | None (Public) | `{"Email": "String", "PasswordHash": "String"}` | `200 OK` - `{"User_ID": 1, "Role": "Participant"}`<br>`401 Unauthorized` |
| **GET** | `/api/profile/{id}` | Retrieves user profile details | Any (Logged in) | None | `200 OK` - `{"User_ID": 1, "FullName": "String", "Email": "String", "Role": "String", "Gender": "String", "DateOfBirth": "Date"}`<br>`404 Not Found` |
| **PUT** | `/api/profile/{id}` | Updates personal user profile information | Any (Logged in) | `{"FullName": "String", "Gender": "String", "DateOfBirth": "Date"}` | `200 OK` - Profile updated<br>`400 Bad Request` |
| **GET** | `/api/venues` | Retrieves all available event venues | None (Public) | None | `200 OK` - Array of Venue objects |
| **GET** | `/api/events` | Retrieves all upcoming events | None (Public) | None | `200 OK` - Array of Event objects containing Title, EventDate, Status, EventType |
| **GET** | `/api/events/{id}` | Retrieves specific event details | None (Public) | None | `200 OK` - Event object including Organiser_ID and Venue_ID<br>`404 Not Found` |
| **POST** | `/api/events` | Creates a new event | Organiser | `{"Organiser_ID": 1, "Venue_ID": 1, "Title": "String", "Description": "String", "EventDate": "DateTime", "Status": "String", "EventType": "String"}` | `201 Created` - Created Event object<br>`400 Bad Request` |
| **PUT** | `/api/events/{id}` | Updates an existing event | Organiser | `{"Title": "String", "Description": "String", "EventDate": "DateTime", "Status": "String", "EventType": "String"}` | `200 OK` - Updated Event object<br>`404 Not Found` |
| **DELETE** | `/api/events/{id}` | Deletes an event | Organiser | None | `204 No Content`<br>`404 Not Found` |
| **GET** | `/api/events/{id}/categories` | Lists all race categories for an event | None (Public) | None | `200 OK` - Array of Category objects<br>`404 Not Found` |
| **POST** | `/api/events/{id}/categories` | Adds a category to an event | Organiser | `{"CategoryName": "String", "DistanceKm": 10.5, "EntryFee": 150.00, "MaxParticipants": 500, "StartDateTime": "DateTime", "MinAge": 16, "RouteDescription": "String"}` | `201 Created` - Created Category object<br>`400 Bad Request` |
| **POST** | `/api/enrollments` | Registers a participant for an event category | Participant | `{"User_ID": 1, "Category_ID": 1, "EnrollmentDate": "Date", "PaymentStatus": "Paid", "BibNumber": "1001", "EmergancyContact": "0821234567"}` | `201 Created` - Created Enrollment object<br>`400 Bad Request` |
| **GET** | `/api/enrollments/user/{userId}` | Gets all enrollments for a participant | Participant | None | `200 OK` - Array of Enrollment objects |
| **GET** | `/api/results/category/{categoryId}` | Gets results leaderboard for a category | None (Public) | None | `200 OK` - Array of Result objects showing OverallRank and CategoryRank |
| **POST** | `/api/results` | Records official race result for an enrollment | Organiser | `{"Enrollment_ID": 1, "FinishTimeMinutes": 45.5, "OverallRank": 1, "Completed": 1, "CategoryRank": 1}` | `201 Created` - Created Result object<br>`400 Bad Request` |