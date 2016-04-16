using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMMaschine;

namespace DeleteCopyAndLoad
{
    public class ViewModel
    {
        public Model Model { get; set; }
        public ActionCommando DeleteActionCommando { get; set; }
        public ActionCommando CopyActionCommando { get; set; }
        public ActionCommando ChooseFolderCommando { get; set; }
        public ActionCommando CopyRangeActionCommando { get; set; }


        public ViewModel()
        {
            this.Model = new Model();
            this.DeleteActionCommando = new ActionCommando(DeleteButtonClick);
            this.CopyActionCommando = new ActionCommando(CopyButtonClick);
            this.ChooseFolderCommando = new ActionCommando(ChooseFolderClick);
            this.CopyRangeActionCommando = new ActionCommando(CopyRangeClick);
        }

        private void CopyRangeClick()
        {
            this.Model.CopyRangeClick();
        }

        private void ChooseFolderClick()
        {
            this.Model.ChooseFolderClick();
        }

        private void CopyButtonClick()
        {
            this.Model.CopyClick();
        }

        private void DeleteButtonClick()
        {
            this.Model.DeleteClick();
        }
    }

}
