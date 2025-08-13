using System;

namespace TestApp.Domain.Customers.ValueObjects;

public record CustomerNameValueObject
{
    public string Value { get; init; }
    public CustomerNameValueObject(string name)
    {
        if (name == "")
        {
            throw new Exception("Name is required");
        }

        Value = name;
    }
}
