namespace Desafio.Cadastro.UnitTests.Application.Usuario.CreateUsuario
{
    public class CreateUsuarioTestDataGenerator
    {
        public static IEnumerable<object[]> GetInvalidInputs(int times = 6)
        {
            var fixture = new CreateUsuarioTestFixture();
            var invalidInputsList = new List<object[]>();
            var totalInvalidCases = 4;

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
