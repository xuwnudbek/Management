namespace Management.Domain.Models;

public class Student(string id, string firstName, string lastName)
{
    public string Id { get; set; } = id;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
}
