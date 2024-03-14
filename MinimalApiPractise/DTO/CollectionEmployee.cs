namespace MinimalApiPractise.DTO
{
    public static class CollectionEmployee
    {
        public static List<Employee> Employees { get; set; } = new();

        static CollectionEmployee()
        {
            Employees = new List<Employee>
                {
                    new Employee { EmployeeId = 1, EmployeeName = "John", Department = "HR", Designation = "Manager", Salary = 50000 },
                    new Employee { EmployeeId = 2, EmployeeName = "Smith", Department = "IT", Designation = "Developer", Salary = 40000 },
                    new Employee { EmployeeId = 3, EmployeeName = "Peter", Department = "Finance", Designation = "Accountant", Salary = 30000 },
                    new Employee { EmployeeId = 4, EmployeeName = "Sam", Department = "HR", Designation = "Executive", Salary = 20000 },
                    new Employee { EmployeeId = 5, EmployeeName = "Tom", Department = "IT", Designation = "Tester", Salary = 25000 }
                };
        }
    }
}
/*

{
    "employeeId": 5,
  "employeeName": "TomJJ",
  "department": "IT",
  "designation": "Tester",
  "salary": 205000
}

*/