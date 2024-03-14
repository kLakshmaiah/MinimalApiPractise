namespace MinimalApiPractise.DTO
{
    public class Employee
    {
        //create properties like employee id, name, department, designation, salary, etc.
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }
        public double? Salary { get; set; }
    }
}
