using static MinimalApiPractise.DTO.CollectionEmployee;
using MinimalApiPractise.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using MinimalApiPractise.Mapper;
using AutoMapper;
using FluentValidation;
using MinimalApiPractise.Validator;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(CustomMappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

app.MapGet("/api/Employee", () =>
{
    return Results.Ok(Employees);
}).WithName("Employees").Produces<IEnumerable<Employee>>(200, "application/json");
app.MapGet("/api/Employee/{id:int?}", (int? id) =>
{
    Employee? employee = Employees.FirstOrDefault(emp => emp.EmployeeId == id);
    if (employee != null)
    {
        return Results.Ok(employee);
    }
    else
    {
        return Results.NotFound();
    }
}).WithName("GetEmployee").Produces<Employee>(201).Produces<Employee>(404);

app.MapPost("/api/PostEmployee", async (IValidator<EmployeeDto> validator, IMapper _mapper, EmployeeDto employeeDto) =>
{
    FluentValidation.Results.ValidationResult isValid = await validator.ValidateAsync(employeeDto);
    if (isValid.IsValid)
    {
        int? id = Employees.LastOrDefault()?.EmployeeId;
        Employee employee = _mapper.Map<Employee>(employeeDto);
        //{
        //    EmployeeId = id + 1,
        //    EmployeeName = employeeDto.EmployeeName,     autoMapper is Taking the Responsibility
        //    Department = employeeDto.Department,
        //    Designation = employeeDto.Designation,
        //    Salary = employeeDto.Salary
        //};
        Employees.Add(employee);
        return Results.Created($"/api/Employee/{id}", employee);
    }
    else
        return Results.BadRequest(isValid.Errors.FirstOrDefault()?.ToString());
}).WithName("AddEmployee").Produces<Employee>(200).Produces<Employee>(400);

app.MapPut("/api/Employee", (IMapper _mapper, EmployeeDto employeeDto) =>
{
    if (Employees.Any(emp => emp.EmployeeId == employeeDto.EmployeeId))
    {
        Employee Oldemploye = _mapper.Map<Employee>(employeeDto);
        //Oldemploye.EmployeeId = employee.EmployeeId;
        //Oldemploye.EmployeeName= employee.EmployeeName;
        //Oldemploye.Salary= employee.Salary;
        //Oldemploye.Designation = employee.Designation;   autoMapper is Taking the Responsibility
        //Oldemploye.Department = employee.Department;
        //var index = Employees.IndexOf(Oldemploye);
        Employees[Employees.FindIndex(emp => emp.EmployeeId == Oldemploye.EmployeeId)] = Oldemploye;//finding the Specific id and inserted at specific possition.
        return Results.CreatedAtRoute("GetEmployee", new { id = employeeDto.EmployeeId }, Oldemploye);
    }
    else
    {
        return Results.NotFound();
    }
}).WithName("UpdateEmployee").Produces<Employee>(201).Produces(404);
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
