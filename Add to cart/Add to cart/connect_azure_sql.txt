///////////
// FIRST - moidfy the:
// private bool useInMemory = true;
// to:
// private bool useInMemory = false;
//
// Then:

// Installs the migrations tool
dotnet tool install --global dotnet-ef

// Create the migrations files
dotnet ef migrations add -c BookContext InitialCreate

// Actually updates the database
dotnet ef database update