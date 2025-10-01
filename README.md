# SouthAfricanIdValidator

This package provides robust validation and age extraction for South African ID numbers. It includes:

- Checksum validation
- Date of birth extraction and validation
- Age calculation (years, months, days)

## Features

- Validate South African ID numbers for correct format and checksum
- Extract and validate date of birth from the ID number
- Calculate age in years, months, and days
- Encapsulate age details in a dedicated class

## Usage Example

```csharp
using SouthAfricanIdValidator;

class Program
{
    static void Main()
    {
        string idNumber = "5504191234567"; // Example ID number
        var extracted = new RsaValidatorExtracted(idNumber);

        if (extracted.Validate())
        {
            var ageInfo = extracted.AgeDetails;
            Console.WriteLine($"Valid ID!");
            Console.WriteLine($"Date of Birth: {ageInfo.DateOfBirth:yyyy-MM-dd}");
            Console.WriteLine($"Age: {ageInfo.AgeString}");
        }
        else
        {
            Console.WriteLine("Invalid ID number.");
        }
    }
}
```

## How It Works

- The `RsaValidator` class validates the ID number using the Luhn algorithm and checks the embedded date of birth.
- The `RsaValidatorExtracted` class uses `RsaValidator` for validation and extracts age details if the ID is valid.
- The `AgeInfo` class provides age in years, months, days, and the date of birth.

## API

### RsaValidator

- `IsValidId(string idNumber)` — Validates the ID number format and checksum
- `IsValidIdDate(string idNumber)` — Validates the date of birth in the ID number
- `Validate(string idNumber)` — Returns true if both checks pass

### RsaValidatorExtracted

- `Validate()` — Validates the ID number
- `AgeDetails` — Returns an `AgeInfo` object with age and date of birth

### AgeInfo

- `Years`, `Months`, `Days` — Age breakdown
- `DateOfBirth` — Extracted date of birth
- `AgeString` — Formatted age string

## License

This project is licensed under the Apache License 2.0. You must give appropriate credit to the original author in any derivative works or distributions.

See the LICENSE file for full details.
