// ------------------------------------------------------------------------------------------------------
// <copyright file="AutenticacaoModel.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Entity.DAOs.Usuarios
{
    public class AutenticacaoModelDAO
    {
        public AutenticacaoModelDAO()
        {
            
        }
        public string Email { get; set; }

        public string Senha { get; set; }


    }
}
