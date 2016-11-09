using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.NET.Annotations
{
    public class YearValidationAttribute : ValidationAttribute
    {
        private readonly int _minValue = 1900;
        private readonly int _maxValue = DateTime.Today.Year;

        public override bool IsValid(object value)
        {
            int val = (int)value;
            return val >= _minValue && val <= _maxValue;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, _minValue, _maxValue);
        }
    }
}
