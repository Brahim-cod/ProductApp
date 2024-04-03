using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shered.Helpers;

internal class MinWordsAttribute : ValidationAttribute
{
    private readonly int _minWords;

    public MinWordsAttribute(int minWords)
    {
        _minWords = minWords;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var text = value.ToString();
            var wordCount = Regex.Matches(text, @"[\S]+").Count;

            if (wordCount < _minWords)
            {
                return new ValidationResult($"The {validationContext.DisplayName} field must contain at least {_minWords} words.");
            }
        }

        return ValidationResult.Success;
    }
}
