# WaracleHotelBooking

This solution was completed as a technical task for Waracle.

The API hosted at https://waracle-hotelbooking-api.azurewebsites.net/swagger/index.html implements a hotel booking system.

//Assumptions
- A booking's start and end date correlate with Check-In and Check-out dates. As such, a room may have a booking begin on the same day that booking for that same room has ended.
- A booking may not start after the current date.
- When requesting availble rooms, the end user will be presented with *all* options available, as the number of guests may exceed the capacity of any particular room, but multiple room bookings would fit the required party.

//Hosting & Database
-The solution is hosted in an Azure App Service and makes uses of an Azure SQL Database. 
-The app service and database are on basic/free tier plans. Performance could be improved with more standard plans.

//CI/CD
- This repo is a mirror of a repo in Azure DevOps. That repo is part of a build and release pipeline for easy and reliable deployment.

//Sample data
- The database can be reset (cleared entirely) and seeded.
- The database seed populates 3 Hotels, each with 6 rooms in a variety of room types
- Room types are represented as enums: 0 - Deluxe, 1 - Single, 2 - Double
- Hotel 1 ("Pinnacle View Suites") has been pre-populated with the bookings noted in the "//Pre-populated bookings" section.
- Hotels 2 and 3 have no bookings pre-populated.
- Sample data could be imrpoved with a larger base of pre-populated hotels and bookings.
- Sample data booking ref's are based on new Guids each seed. Testing could be aided by seeding with preset Refs.

//Testing
- XUnit tests cover a large portion of the functionality
- Testing could be improved with additional test cases and a larger set of sample data.

//Pre-populated bookings for Hotel 1
| Room Name | Start Date | End Date   | Guests |
|-----------|------------|------------|--------|
| 101       | 2025-12-04 | 2025-12-08 | 1      |
| 101       | 2025-12-09 | 2025-12-11 | 1      |
| 102       | 2025-12-01 | 2025-12-02 | 1      |
| 102       | 2025-12-08 | 2025-12-10 | 1      |
| 201       | 2025-12-01 | 2025-12-03 | 1      |
| 201       | 2025-12-09 | 2025-12-12 | 2      |
| 301       | 2025-12-01 | 2025-12-05 | 4      |
| 301       | 2025-12-05 | 2025-12-09 | 3      |
| 301       | 2025-12-09 | 2025-12-12 | 4      |
| 302       | 2025-12-05 | 2025-12-08 | 4      |
