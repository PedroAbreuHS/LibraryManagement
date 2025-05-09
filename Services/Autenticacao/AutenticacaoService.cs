﻿using System.Security.Cryptography;

namespace LibraryManagement.Services.Autenticacao
{
    public class AutenticacaoService : IAutenticacaoRepository
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }
    }
}
