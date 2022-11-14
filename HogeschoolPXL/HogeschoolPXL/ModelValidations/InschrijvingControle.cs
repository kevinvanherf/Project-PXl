using HogeschoolPXL.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HogeschoolPXL.ModelValidations
{
    public class InschrijvingControle : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context , HogeschoolPXLDbContext contextdb)
        {
            
            var lst = new List<ModelValidationResult>();
            if (contextdb.Student == null && contextdb.VakLector == null)
            {
                lst.Add(new ModelValidationResult("", "er bestaat geen leerlingen en geen curssen dus kun je geen inschrijving  maken , er moeten eerst leerlingen komen en cursussen worden aangemaakt."));
            }
            else if (contextdb.Student == null)
            {
                lst.Add(new ModelValidationResult("", "er bestaat geen leerlingen dus kun je geen inschrijving  maken , er moeten eerst leerlingen komen."));

            }
            else if (contextdb.VakLector == null)
            {
                lst.Add(new ModelValidationResult("", "er bestaat geen cursussen dus kun je geen inschrijving  maken ,maak eerst een cursus "));

            }
            return lst;

        }
    }
}
