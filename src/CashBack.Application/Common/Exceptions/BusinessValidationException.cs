using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace CashBack.Application.Common.Exceptions
{
    public class BusinessValidationException : Exception
    {
        public BusinessValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public BusinessValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        public BusinessValidationException(IDictionary<string, string[]> failures)
            : this()
        {
            Failures = failures;
        }

        public BusinessValidationException(string propertyName, string failure)
            : this()
        {
            Failures = new Dictionary<string, string[]>();
            Failures.Add(propertyName, new[]{failure});
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}

