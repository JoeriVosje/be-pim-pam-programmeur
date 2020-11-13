using System;
using System.Collections.Generic;

namespace PimPamProgrammeur.Dto
{
    public class ComponentOrderRequestDto
    {
        public IEnumerable<Guid> ComponentIds { get; set; }
    }
}
