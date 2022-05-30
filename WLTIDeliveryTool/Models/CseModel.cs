using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WLTIDeliveryTool.Repository;
using DAL;
namespace WLTIDeliveryTool.Models
{
    public class CseModel
    {
        private readonly UnitOfWork _unitOfWork;
        public CseModel()
        {
            _unitOfWork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }

        public IEnumerable<ChannelSalesExecutive> GetAll()
        {
            return _unitOfWork.CSE.GetAll();
        }

        public void Add(ChannelSalesExecutive cse)
        {
            _unitOfWork.CSE.Add(cse);
            _unitOfWork.Complete();
        }

        public ChannelSalesExecutive Get(int id)
        {
           return _unitOfWork.CSE.Get(id);
        }

        public void Update(ChannelSalesExecutive cse)
        {
            _unitOfWork.CSE.Update(cse);
            _unitOfWork.Complete();
        }
    }
}