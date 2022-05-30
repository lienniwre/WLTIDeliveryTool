using Microsoft.VisualStudio.TestTools.UnitTesting;
using WLTIDeliveryTool.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using WLTIDeliveryTool.Repository;
using WLTIDeliveryTool.Interface;


namespace WLTIDeliveryTool.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        public readonly UnitOfWork _unitofwork;
        public AccountControllerTests()
        {
            this._unitofwork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }
       
        [TestMethod()]
        public void LoginTest()
        {
            var a = _unitofwork.Users.GetAll();


            //Assert.AreEqual(a,1);
        }
    }
}