namespace RefactoringChallenge.Business.Exceptions
{
    public class EntityNotFoundExeption : BusinessException
    {
        public EntityNotFoundExeption(string entityName) : base($"{entityName} is not found")
        {
        }
    }
}