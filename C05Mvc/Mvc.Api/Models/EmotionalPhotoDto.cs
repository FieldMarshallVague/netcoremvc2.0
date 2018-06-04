using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Api.Models
{
    [ModelBinder(typeof(AwesomeModelBinder))]
    public class EmotionalPhotoDto
    {
        public byte[] Contents { get; set; }
        public EmotionScoresDto Scores { get; set; }
    }
}
