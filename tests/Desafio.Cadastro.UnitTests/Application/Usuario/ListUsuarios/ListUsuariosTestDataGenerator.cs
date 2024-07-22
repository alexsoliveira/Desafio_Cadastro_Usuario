using Desafio.Cadastro.Application.UseCases.Usuario.ListUsuarios;

namespace Desafio.Cadastro.UnitTests.Application.Usuario.ListUsuarios
{
    public class ListUsuariosTestDataGenerator
    {
        public static IEnumerable<object[]> GetInputWithoutAllParameters(int times = 12)
        {
            var fixture = new ListUsuariosTestFixture();
            var inputExample = fixture.GetExampleInput();
            for (int i = 0; i < times; i++)
            {
                switch (i % 6)
                {
                    case 0:
                        yield return new object[] { new ListUsuariosInput() };
                        break;
                    case 1:
                        yield return new object[] { new ListUsuariosInput(inputExample.Page) };
                        break;
                    case 2:
                        yield return new object[] {
                            new ListUsuariosInput(
                                inputExample.Page,
                                inputExample.PerPage)
                        };
                        break;
                    case 3:
                        yield return new object[] {
                            new ListUsuariosInput(
                                inputExample.Page,
                                inputExample.PerPage,
                                inputExample.Search)
                        };
                        break;
                    case 4:
                        yield return new object[] {
                            new ListUsuariosInput(
                                inputExample.Page,
                                inputExample.PerPage,
                                inputExample.Search,
                                inputExample.Sort)
                        };
                        break;
                    case 5:
                        yield return new object[] {
                            inputExample
                        };
                        break;
                    default:
                        yield return new object[] { new ListUsuariosInput() };
                        break;
                }
            }
        }
    }
}
