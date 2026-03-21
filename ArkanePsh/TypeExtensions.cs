using System;
using System.Management;

namespace ArkaneSystems.PowerShell
{
  public static class Int32Extensions
  {
    public static string ToRoman (this int value)
    {
      if (value <= 0)
        return string.Empty;
      var numerals = new[]
            {
                new { Value = 1000, Numeral = "M" },
                new { Value = 900, Numeral = "CM" },
                new { Value = 500, Numeral = "D" },
                new { Value = 400, Numeral = "CD" },
                new { Value = 100, Numeral = "C" },
                new { Value = 90, Numeral = "XC" },
                new { Value = 50, Numeral = "L" },
                new { Value = 40, Numeral = "XL" },
                new { Value = 10, Numeral = "X" },
                new { Value = 9, Numeral = "IX" },
                new { Value = 5, Numeral = "V" },
                new { Value = 4, Numeral = "IV" },
                new { Value = 1, Numeral = "I" }
            };
      int remaining = value;
      string result = string.Empty;
      foreach (var item in numerals)
      {
        while (remaining >= item.Value)
        {
          result += item.Numeral;
          remaining -= item.Value;
        }
      }

      return result;
    }
  }

  public static class ManagementObjectExtensions
  {
    public static string GetDomainRoleStr (this ManagementObject obj)
    {
      if (obj == null || obj.Properties["DomainRole"] == null)
        return "Unknown";
      int role = Convert.ToInt32(obj["DomainRole"]);
      return role switch
      {
        0 => "Standalone Workstation",
        1 => "Member Workstation",
        2 => "Standalone Server",
        3 => "Member Server",
        4 => "Backup Domain Controller",
        5 => "Primary Domain Controller",
        _ => "Unknown"
      };
    }
  }
}
