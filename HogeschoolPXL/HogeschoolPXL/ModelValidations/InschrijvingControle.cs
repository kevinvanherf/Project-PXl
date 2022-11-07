using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HogeschoolPXL.ModelValidations
{
    public class InschrijvingControle : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var model = context.Model;
            var lst = new List<ModelValidationResult>();
            if (model == null)
            {
                lst.Add(new ModelValidationResult("", "er bestaat geen vakken en of geen leerlingen  dus kun je geen vak maken "));

            }

            return lst;

        }
    }
}
