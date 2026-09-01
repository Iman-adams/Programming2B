# RaceDay event and race management system
## YouTube video

## CI/CD
<img width="1472" height="610" alt="image" src="https://github.com/user-attachments/assets/3f7193c8-24a4-4269-8b00-3da4ac529af5" />


## Section A
<img width="1493" height="652" alt="RaceDay_ERD" src="https://github.com/user-attachments/assets/79fd527a-96e4-404b-b5c8-b8e727d22223" />

### Overview
- Section A establishes the relational model for the entire RaceDay system. The database is structured to prevent redundant data storage while maintaining strict referential integrity across all entities.

## Section B
### Overview
- Section B details the functional API architecture required for client-server interaction. The specification outlines HTTP verbs, endpoint routing rules, payload requirements, authentication constraints, and standardized HTTP response status codes.

## Section c
### Overview
- Section C provides the complete T-SQL database script required to build, structure, and populate the physical RaceDay database in SQL Server.
- It defines all six tables using structured 3-digit primary key series (100s through 600s) while strictly enforcing foreign key relationships between users, venues, events, categories, enrollments, and results.
- It includes realistic sample seed data to test the system and ends with verification SELECT queries to instantly check that all tables were created and populated correctly.
How it the it looks when all the tables are executed:
<img width="1111" height="580" alt="image" src="https://github.com/user-attachments/assets/e8f7bb6e-1ea9-4f66-a7bf-c022ee20298e" />
