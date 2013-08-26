using System.Web;
using System.Web.Http;
using feedbacksys.Domain;
using feedbacksys.Domain.Repository;

namespace feedbacksys.Controllers
{
    public class FeebackController : ApiController
    {
        [System.Web.Mvc.HttpPost]
        public void Post(Image img)
        {
            var repository = new ImageRepository();
            repository.Create(img);

        }
        
    }
}