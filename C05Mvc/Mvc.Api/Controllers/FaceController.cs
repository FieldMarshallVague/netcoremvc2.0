using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mvc.Api.Models;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    public class FaceController : Controller
    {
        public ActionResult Post(EmotionalPhotoDto photo)
        {
            return Ok(photo.Scores);
        }
    }
}