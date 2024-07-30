﻿using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Models;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.UseCase.Interfaces;

namespace Fiap.CleanArchitecture.UseCase
{
    public class UsuarioUseCase : IUsuarioUseCase
    {
        private readonly IUsuarioGateway _usuarioGateway;
        private readonly ITarefaGateway _tarefaGateway;

        public UsuarioUseCase(IUsuarioGateway usuarioGatway, ITarefaGateway tarefaGateway)
        {
            _usuarioGateway = usuarioGatway;
            _tarefaGateway = tarefaGateway;
        }

        public Usuario AltereUsuaio(UsuarioAlterarDAO usuario)
        {
            var novoUsuario = new Usuario(usuario);

            return _usuarioGateway.Alterar(novoUsuario);
        }

        public string AutentiqueUsuario(UsuarioDAO usuario)
        {
            var usuarioAut = new Usuario(usuario.Email, usuario.Senha);

            var token = _usuarioGateway.GerarToken(usuarioAut);

            return token;
        }

        public void CadastreNovoUsuario(UsuarioDAO usuario)
        {
            var novoUsuario = new Usuario(usuario);

            _usuarioGateway.Criar(novoUsuario);
        }

        public void ExcluaUsuario(int IdUsuario)
        {
            var tarefas = _tarefaGateway.BuscarTodos()
                .Where(x => x.Responsavel.Id == IdUsuario || x.Criador.Id == IdUsuario);

            if (tarefas.Any())
                _usuarioGateway.Excluir(IdUsuario);
            else
                throw new Exception(MensagensValidacoes.Usuario_RelacaoTarefas);
        }
    }
}
