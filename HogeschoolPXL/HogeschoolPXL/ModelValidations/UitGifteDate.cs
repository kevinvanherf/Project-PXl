using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HogeschoolPXL.ModelValidations
{
    public class UitGifteDate : Attribute , IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            int jaar = DateTime.Now.Year;
            var dtm = DateTime.Now;
            var lst = new List<ModelValidationResult>();
            if (DateTime.TryParse(context.Model.ToString(),out dtm))
            {
                if (dtm > new DateTime(jaar, 1, 1))
                    lst.Add(new ModelValidationResult("", "Datum is van uitgifte kan niet voor het huidige jaar zijn! "));
                else if (dtm < new DateTime(1980, 1, 1))
                    lst.Add(new ModelValidationResult("", "datum van uitgifte is te oud moet voor 1980 zijn uitgegeven!"));
            }
            else
            {
                lst.Add(new ModelValidationResult("", "Datum is niet geldig!"));
            }
            return lst;
        }
    }
}
