using System;

namespace ProductTask.Repository.Account.Dto
{
    public class GetAllUsersResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
