﻿namespace UserAuthenticationService.Models.UserModels
{
    public record CreateUserRequest
    {
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public required string Login { get; set; }
        
        public required string Password { get; set; }
    }
}
