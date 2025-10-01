namespace SouthAfricanIdValidator;

class RsaValidator
{
    private string IdNumber;

    public RsaValidator()
    {
    }

    public RsaValidator(string idNumber)
    {
        IdNumber = idNumber;
    }

    public bool IsValidId(string idNumber)
    {
        if (string.IsNullOrEmpty(idNumber) || !idNumber.All(char.IsDigit))
            return false;

        int[] digits = idNumber.Select(c => c - '0').ToArray();
        int checksum = 0;
        int parity = digits.Length % 2;

        for (int i = 0; i < digits.Length; i++)
        {
            int digit = digits[i];
            if (i % 2 == parity)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }
            checksum += digit;
        }
        return checksum % 10 == 0;
    }

    public bool IsValidId()
    {
        if (string.IsNullOrEmpty(this.IdNumber) || !this.IdNumber.All(char.IsDigit))
            return false;

        int[] digits = IdNumber.Select(c => c - '0').ToArray();
        int checksum = 0;
        int parity = digits.Length % 2;

        for (int i = 0; i < digits.Length; i++)
        {
            int digit = digits[i];
            if (i % 2 == parity)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }
            checksum += digit;
        }
        return checksum % 10 == 0;
    }

    public bool IsValidIdDate(string idNumber)
    {
        if (string.IsNullOrEmpty(idNumber) || !idNumber.All(char.IsDigit))
            return false;
        if (idNumber.Length < 6)
            return false;
        string datePart = idNumber.Substring(0, 6);
        if (DateTime.TryParseExact(datePart, "yyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            return date <= DateTime.Now;
        }
        return false;
    }

    public bool IsValidIdDate()
    {
        if (string.IsNullOrEmpty(this.IdNumber) || !this.IdNumber.All(char.IsDigit))
            return false;
        if (this.IdNumber.Length < 6)
            return false;
        string datePart = this.IdNumber.Substring(0, 6);
        if (DateTime.TryParseExact(datePart, "yyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            return date <= DateTime.Now;
        }
        return false;
    }

    public bool Validate(string idNumber)
    {
        return IsValidId(idNumber) && IsValidIdDate(idNumber);
    }
    public bool Validate()
    {
        return IsValidId() && IsValidIdDate();
    }
}

class RsaValidatorExtracted
{
    private RsaValidator _rsaVerifier;
    private string _idNumber;

    private AgeInfo _ageInfo;

    public RsaValidatorExtracted(string idNumber)
    {
        _idNumber = idNumber;
        _rsaVerifier = new RsaValidator(idNumber);
        if (Validate())
        {
            var dob = ParseDateOfBirth(idNumber);
            if (dob != null)
                _ageInfo = new AgeInfo(dob.Value);
        }
    }

    public RsaValidatorExtracted()
    {
        _rsaVerifier = new RsaValidator();
    }


    public bool Validate() => _rsaVerifier.Validate(_idNumber);

    public AgeInfo AgeDetails => _ageInfo;

    private DateTime? ParseDateOfBirth(string idNumber)
    {
        if (string.IsNullOrEmpty(idNumber) || idNumber.Length < 6)
            return null;
        try
        {
            int year = int.Parse(idNumber.Substring(0, 2));
            int month = int.Parse(idNumber.Substring(2, 2));
            int day = int.Parse(idNumber.Substring(4, 2));
            int currentYear2 = DateTime.Now.Year % 100;
            int century = (year > currentYear2) ? 1900 : 2000;
            year += century;
            var dob = new DateTime(year, month, day);
            return dob <= DateTime.Now ? dob : (DateTime?)null;
        }
        catch
        {
            return null;
        }
    }

    public class AgeInfo
    {
        public int Years { get; }
        public int Months { get; }
        public int Days { get; }
        public DateTime DateOfBirth { get; }
        public string AgeString => $"{Years} years, {Months} months, {Days} days";

        public AgeInfo(DateTime dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
            var now = DateTime.Now;
            Years = now.Year - dateOfBirth.Year;
            Months = now.Month - dateOfBirth.Month;
            Days = now.Day - dateOfBirth.Day;

            if (Days < 0)
            {
                Months--;
                Days += DateTime.DaysInMonth(now.Year, (now.Month == 1 ? 12 : now.Month - 1));
            }
            if (Months < 0)
            {
                Years--;
                Months += 12;
            }
        }
    }
}
