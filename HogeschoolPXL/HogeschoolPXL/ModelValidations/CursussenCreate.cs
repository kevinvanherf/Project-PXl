using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HogeschoolPXL.ModelValidations
{
    public class CursussenCreate : Attribute,  IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            int aantal = 0;
            var aantalboeken = 0;
            var lst = new List<ModelValidationResult>();
            if (context.Model ==  null)
            {
                lst.Add(new ModelValidationResult("", "er bestaat geen boek dus kun je geen vak maken "));

            }

            return lst;
        }
        
    }
}
