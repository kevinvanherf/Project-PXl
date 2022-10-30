using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HogeschoolPXL.ModelValidations
{
    public class CursussenCreate : Attribute,  IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var lst = new List<ModelValidationResult>();
            lst.Add(new ModelValidationResult("", "Datum is van uitgifte kan niet voor het huidige jaar zijn! "));
            return lst;
        }
        
    }
}
