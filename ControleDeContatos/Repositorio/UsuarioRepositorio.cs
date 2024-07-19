﻿
using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext) 
        {
            _bancoContext = bancoContext;
        }
        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
          
           usuario.DataCadastro = DateTime.Now;
            // INSERT no banco de dados 
           _bancoContext.Usuarios.Add(usuario);
           _bancoContext.SaveChanges(); // commit na base
           return usuario;
        }
        public List<UsuarioModel> BuscarTodos()
        {
            // usa o context para dar o select na tabela contato
            return _bancoContext.Usuarios.ToList();
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualização dos dados. Usuário não encontrado");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);

            if (usuarioDB == null) throw new Exception("Houve um erro na remoção do contato. Usuário não encontrado");

            _bancoContext.Usuarios.Remove(usuarioDB);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}
