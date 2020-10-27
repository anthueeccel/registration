using System;

namespace register.domain.Validator
{
    public abstract class Validators
    {
        public static bool IsDateValid(DateTime date)
        {
            if (date > DateTime.Now.AddYears(-130) && date < DateTime.Now)
                return true;

            return false;
        }
    }
}
