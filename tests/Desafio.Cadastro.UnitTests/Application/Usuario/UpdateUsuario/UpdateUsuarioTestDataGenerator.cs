namespace Desafio.Cadastro.UnitTests.Application.Usuario.UpdateUsuario
{
    public class UpdateUsuarioTestDataGenerator
    {
        public static IEnumerable<object[]> GetUsuariosToUpdate(int times = 10)
        {
            var fixture = new UpdateUsuarioTestFixture();

            for (int indice = 0; indice < times; indice++)
            {
                var exampleUsuario = fixture.GetExampleUsuario();
                var exampleInput = fixture.GetValidInput(exampleUsuario.Id);
                yield return new object[] {
                exampleUsuario, exampleInput
            };
            }
        }

        public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
        {
            var fixture = new UpdateUsuarioTestFixture();
            var invalidInputsList = new List<object[]>();
            var totalInvalidCases = 3;

            for (int index = 0; index < times; index++)
            {
                switch (index % totalInvalidCases)
                {
                    case 0:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputShortName(),
                        "Name should be at leats 3 characters long"
                    });
                        break;
                    case 1:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongName(),
                        "Name should be less or equal 15 characters long"
                    });
                        break;                    
                    default:
                        break;
                }
            }

            return invalidInputsList;
        }
    }
}
