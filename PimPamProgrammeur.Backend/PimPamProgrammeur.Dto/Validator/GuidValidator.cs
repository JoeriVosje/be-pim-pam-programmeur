using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto.Validator
{
    public abstract class GuidValidator<Guid> : IValidator<Guid> where Guid : class 
    { public ValidationResult Validate(Guid entity) 
        { 
            throw new NotImplementedException(); 
        } 
    }
}
