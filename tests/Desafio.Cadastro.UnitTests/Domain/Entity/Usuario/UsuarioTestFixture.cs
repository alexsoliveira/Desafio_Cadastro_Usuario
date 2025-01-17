﻿using Bogus;
using Desafio.Cadastro.UnitTests.Common;
using Xunit;
using DomainEntity = Desafio.Cadastro.Domain.Entity;

namespace Desafio.Cadastro.UnitTests.Domain.Entity.Usuario
{
    [CollectionDefinition(nameof(UsuarioTestFixture))]
    public class UsuarioTextFixtureCollection
    : ICollectionFixture<UsuarioTestFixture>
    { }

    public class UsuarioTestFixture : BaseFixture
    {        
        public UsuarioTestFixture() 
            : base() { }

        public string GetValidUsuarioName()
        {
            var usuarioNome = "";

            while (usuarioNome.Length < 3)
                usuarioNome = Faker.Name.FirstName();

            if (usuarioNome.Length > 15)
                usuarioNome = usuarioNome[..15];

            return usuarioNome;
        }

        public DomainEntity.Usuario GetValidUsuario()
            => new(
                GetValidUsuarioName()
            );
    }
}
