using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WLTIDeliveryTool.Repository;
using DAL;
namespace WLTIDeliveryTool.Models
{
    public class ClientModel
    {
        private readonly UnitOfWork _unitOfWork;
        public ClientModel()
        {
            _unitOfWork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }
        public void Update(Client client)
        {
            _unitOfWork.Client.Update(client);
            _unitOfWork.Complete();
        }
        public IEnumerable<Client> GetAll()
        {
            return _unitOfWork.Client.GetAll();
        }
        public Client Get(int id)
        {
            return _unitOfWork.Client.Get(id);
        }
        public void Add(Client client)
        {
            _unitOfWork.Client.Add(client);
            _unitOfWork.Complete();
        }

    }
}