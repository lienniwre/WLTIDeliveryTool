using Microsoft.VisualStudio.TestTools.UnitTesting;
using WLTIDeliveryTool.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLTIDeliveryTool.Repository;
using DAL;

namespace WLTIDeliveryTool.Helper.Tests
{
    [TestClass()]
    public class SelectListHelperTests
    {
        public readonly UnitOfWork _unitofwork;
        public SelectListHelperTests()
        {
            this._unitofwork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }
        [TestMethod()]
        public void CategoryTest()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Please select Status", Value = "0", Selected = true });
            var status = _unitofwork.DeliveryStatus.GetAll();

            foreach (var item in status)
            {


               // item.Definition = _textInfo.ToTitleCase(item.Definition);
                var category = new SelectListItem { Text = item.Definition, Value = item.Id.ToString() };
                list.Add(category);

            }
            Assert.Fail();
        }
    }
}