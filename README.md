# s3827202_s3687609_a2

Group 11:
Theo Riandy   s3827202
Shaoxuan Wei  s3687609

This is a ASP.NET core 5.0 Web Application

Packages used:
-IdentityAPI
-sendgrid (to send emails)
-Entity framework core
-Simple Hashing

Project Structure (in solution):
McbaWebAPI -> http//Localhost:5100
-This contains the WebAPI data for the admin features

McbaAdmin
-This project contains the Admin web application

s3827202_s3687609_a2
-This project contains Customer and admin feature using the identity API

Starting points:
Program.cs in s3827202_s3687609_a2 project

NOTES:
-Run multiple project for s3827202_s3687609_a2 and McbaWebAPI, for the admin part in the s3827202_s3687609_a2 project rely on the WebAPI
-Migration is available in the s3827202_s3687609_a2 project (s3827202_s3687609_a2/Migrate/20210130152601_InitCreate)
  -Update database from nuget package manager console (Update-Database, ensure that the startup project is s3827202_s3687609_a2)
  
  
