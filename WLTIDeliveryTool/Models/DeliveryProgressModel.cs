using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WLTIDeliveryTool.Repository;
using DAL;
namespace WLTIDeliveryTool.Models
{
    public class DeliveryProgressModel
    {
        private readonly UnitOfWork _unitOfWork;

        public DeliveryProgressModel()
        {
            _unitOfWork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }

        public IEnumerable<DeliveryProgress> GetAll()
        {
            return _unitOfWork.DeliveryProgress.GetAll();
        }

        public void Add(DeliveryProgress progress)
        {
            _unitOfWork.DeliveryProgress.Add(progress);
            _unitOfWork.Complete();
        }

        public DeliveryProgress Get(int id)
        {
           return  _unitOfWork.DeliveryProgress.Get(id);
        }

        public void Update(DeliveryProgress progress)
        {
            _unitOfWork.DeliveryProgress.Update(progress);
            _unitOfWork.Complete();
        }
    }
}