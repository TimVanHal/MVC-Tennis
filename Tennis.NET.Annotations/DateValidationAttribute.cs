using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.NET.Annotations
{
    public class DateValidationAttribute : ValidationAttribute
    {
        private readonly DateTime _minValue = new DateTime(1900, 1, 1);
        private readonly DateTime _maxValue = DateTime.Today;

        public override bool IsValid(object value)
        {
            DateTime val = (DateTime)value;
            return val >= _minValue && val <= _maxValue;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, _minValue, _maxValue);
        }

    }
}
