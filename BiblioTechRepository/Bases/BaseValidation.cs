namespace BiblioTechDomain.Bases
{
    public class BaseValidation
    {
        public BaseValidation()
        {
            _errors = new List<BaseError>();
        }

        public BaseValidation(BaseError error)
        {
            _errors = new List<BaseError> { error };           
        }

        private IList<BaseError> _errors;

        public bool HasErros { get { return _errors.Any() ? true : false; } }

        public IEnumerable<BaseError> Errors 
        {
            get 
            { 
                return _errors; 
            } 
        }

        public void AddError(BaseError error)
        {
            _errors.Add(error);
        }              
    }
}
