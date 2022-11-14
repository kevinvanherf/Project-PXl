using HogeschoolPXL.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HogeschoolPXL.ModelValidations
{
    public class InschrijvingControle : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context )
        {
            
            var lst = new List<ModelValidationResult>();
            if (context.Model == null )
            {
                lst.Add(new ModelValidationResult("", "er bestaat geen leerlingen of geen curssen dus kun je geen inschrijving  maken , er moeten eerst leerlingen komen of cursussen worden aangemaakt."));
            }
          
            return lst;

        }
    }
}
