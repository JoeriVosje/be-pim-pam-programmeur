﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Dto
{
    public class ResultResponseDto
    {
        public bool Success { get; set; }

        public Guid CorrectAnswerId { get; set; }
        public string Hint { get; set; }
    }
}
