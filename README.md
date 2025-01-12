# XmlFormatter

`XmlFormatter` is a C# library designed to convert objects into well-structured XML strings. It handles various data types, including primitive types, nullable types, collections, and nested objects. The library also gracefully handles special cases like null values and zero for numeric types.

## Features

- Convert objects to XML representation.
- Handle properties with:
  - Primitive types (e.g., `int`, `bool`, `double`).
  - Nullable types (e.g., `int?`, `DateTime?`).
  - Strings, decimals, and `DateTime`.
  - Collections (`IEnumerable`) with nested object support.
- Handles null values with empty XML tags.
- Automatically skips indexer properties.

## Installation

You can integrate this library into your project by copying the `XmlFormatter` class into your codebase. Alternatively, you can package it as a library and reference it in your project.

## Usage

### Example Code

Here’s an example of how to use `XmlFormatter`:

```csharp
using System;
using XmlFormattingAssignment;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public string[] Colors { get; set; }
}

class Program
{
    static void Main()
    {
        Animal animal = new Animal
        {
            Id = 1,
            Name = "Lion",
            BirthDate = new DateTime(2022, 12, 1),
            Colors = new[] { "Golden", "Brown" }
        };

        string xml = XmlFormatter.ConvertToXML(animal);
        Console.WriteLine(xml);
    }
}

Output
<Animal>
  <Id>1</Id>
  <Name>Lion</Name>
  <BirthDate>2022-12-01T00:00:00</BirthDate>
  <Colors>
    <String>Golden</String>
    <String>Brown</String>
  </Colors>
</Animal>

How It Works
Core Method: ConvertToXML
This method accepts an object as input and generates its XML representation. It handles:

Null values by generating empty XML tags (e.g., <PropertyName></PropertyName>).
Collections by iterating through elements and serializing each one.
Nested objects by recursively calling the Visit method.
Helper Method: Visit
The Visit method serializes nested objects and their properties. It supports the same types as ConvertToXML, ensuring consistency.

Special Cases
Null Values: Rendered as empty tags.
Zero for Numeric Types: Can be treated as empty or included based on logic.
Collections: Each element in the collection is serialized individually.
Contributing
Contributions are welcome! If you have suggestions, bug reports, or feature requests, feel free to open an issue or submit a pull request.

License
This project is licensed under the MIT License. See the LICENSE file for details.

### Highlights
1. **Feature List**: Details the key functionality.
2. **Installation Instructions**: Provides steps to integrate the library.
3. **Usage Example**: Demonstrates how to use the library with sample output.
4. **Technical Details**: Explains the core methods and their behavior.
5. **Contribution Guidelines**: Encourages community involvement.
6. **License**: Indicates the project's license for open-source use.

