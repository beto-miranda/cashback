# cashback

dotnet ef migrations add -c CashBack.Web.Data.IdentityDbContext -o Data/Migrations/IdentityDatabase Initial

dotnet ef migrations add -c CashBack.Data.EntityFramework.ApplicationDbContext -o Data/Migrations/ApplicationDatabase Initial


dotnet ef database update -c CashBack.Web.Data.IdentityDbContext 

dotnet ef database update -c CashBack.Data.EntityFramework.ApplicationDbContext