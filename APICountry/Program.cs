
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

/*
  CORS CONFIG
*/
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {   //rutas permitidas para el CORS
                          builder.WithOrigins("http://localhost:4200");
                      });
});

var app = builder.Build();
//Implementacion CORS
app.UseCors(MyAllowSpecificOrigins);
//Url api
const string url = "https://restcountries.com/v2/all?fields=name,capital,population";

/*
Ruta get para data de https://restcountries.com/v2/all
*/
app.MapGet("/api/countries", async() =>{

    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();
    string responseBody = await response.Content.ReadAsStringAsync();

    return responseBody;
});

app.Run();
