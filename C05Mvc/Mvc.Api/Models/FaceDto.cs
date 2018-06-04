using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Api.Models
{
    public class FaceDto
    {
        public string FaceId { get; set; }
        public object FaceRectangle { get; set; }
        public FaceAttributesDto FaceAttributes { get; set; }
    }

    public class FaceAttributesDto
    {
        public string Gender { get; set; }
        public float Age { get; set; }
        public EmotionScoresDto Emotion { get; set; }
    }
}
