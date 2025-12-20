1. Install Nuget Packages 
   Microsoft.AspNetCore.Authentication.JwtBearer
   System.IdentityModel.Tokens.Jwt
   Microsoft.IdentityModel.Tokens

2. Add keys in appsettings 

Issuer → “Who created this token?” → your API

Audience → “Who is allowed to use this token?” → your API

3. Setup Program.cs file

4. Add Model Folder

5. Create Login Controller - login and generates the token based on user details 

6. Inside the login controller, inject the Iconfiguration to have access to details from the appsettings 

7. Create the login endpoint - authenticates the user (compares if it is present in db) and then 
	Generates the token using the user details

8. Now that we have the token, we will create the UserController 

