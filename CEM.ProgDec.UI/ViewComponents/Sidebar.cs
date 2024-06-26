using CEM.ProgDec.BL;
using Microsoft.AspNetCore.Mvc;

namespace CEM.Bands.UI.ViewComponents
{
    public class Sidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(ProgramManager.Load().OrderBy(p => p.Name));
        }

    }
}
