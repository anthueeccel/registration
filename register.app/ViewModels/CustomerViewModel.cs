using register.domain.Enum;
using System;

namespace register.app.ViewModels
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderType Gender { get; set; }
    }
}
