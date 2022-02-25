# EnsekChallenge
Ensek´s remote meter reading challenge

Fully funcitonal API that takes in a CSV and validates each row provided that they conform to the standard set in the Meter_Reading.csv file.
The first line of the CSV is skipped as we are only interested in the readings directly. 
The valid readings are uploaded directly to a SQLite database.
The API returns both the number of successful and failed readings.
I was quite strict with my validation, only 3 readings passed.
A reading had to have a unique AccountID that had a length of 5 (format NNNNN) and was a positive integer above 0.
Before being uploaded to the local SQL database I ensured that the AccountId of each reading matched any of the AccountIds
stored in the Test_Accounts table.
