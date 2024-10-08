﻿using DomainLayer.Models;

namespace ServiceLayer.IServices
{
    public interface IConfirmEmailService
    {
        public Task SendConfirmEmailAsync(User user);
        public bool IsEmailConfirm(string key, string code);
        public Task SendConfirmChangePasswordEmailAsync(User user);
    }
}
