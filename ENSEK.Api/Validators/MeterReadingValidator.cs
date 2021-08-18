using ENSEK.Api.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

namespace ENSEK.Api.Validators
{
    public class MeterReadingValidator : AbstractValidator<MeterReadingDto>
    {
        public MeterReadingValidator()
        {
            RuleFor(record => record.AccountId)
                .NotNull()
                .NotEmpty()
                .Matches(@"[0-9]+");

            RuleFor(record => record.MeterReadingDateTime)
                .NotNull()
                .NotEmpty()
                .Must(dateTime => DateTimeIsValid(dateTime));

            RuleFor(record => record.MeterReadValue)
                .NotNull()
                .NotEmpty()
                .Matches(@"[0-9]{5}")
                .Length(5);
        }

        private bool DateTimeIsValid(string value)
        {
            return DateTime.TryParse(value, out _);
        }
    }
}
