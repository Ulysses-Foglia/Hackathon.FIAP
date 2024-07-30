﻿using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Gateway.Interfaces;

namespace Fiap.CleanArchitecture.Controller
{
    public class EmailControlador : IEmailControlador
    {
        private readonly IEmailGateway _emailGateway;

        public EmailControlador(IEmailGateway emailGateway)
        {
            _emailGateway = emailGateway;
        }

        public async Task SendMail(string message) => await _emailGateway.SendMailMessage(message);
    }
}
