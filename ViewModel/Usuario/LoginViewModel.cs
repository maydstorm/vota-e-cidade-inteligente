﻿using System.ComponentModel.DataAnnotations;

namespace VotaE_API.ViewModel.Usuario
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Senha { get; set; }
        public string Role { get; set; }
    }
}
