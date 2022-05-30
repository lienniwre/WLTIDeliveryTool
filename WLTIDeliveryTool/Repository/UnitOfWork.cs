using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WLTIDeliveryTool.Interface;


namespace WLTIDeliveryTool.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WLTIDeliveryToolsEntities1 _context;
        public UnitOfWork (WLTIDeliveryToolsEntities1 context)
        {
            _context = context;
            Users = new UserRepository(_context);
            DeliveryStatus = new DeliveryStatusRepository(_context);
            ModeOfDelivery = new ModeOfDeliveryRepository(_context);
            CSE = new CSERepository(_context);
            Client = new ClientRepository(_context);
            DRM = new DrmRepository(_context);
            DeliveryProgress = new DeliveryProgressRepository(_context);
            DrmProgress = new DrmProgressRepository(_context);
            Courier = new CourierRepository(_context);
            EstimatedDelivery = new EstimatedDeliveryRepository(_context);
            Attachments = new AttachmentRepository(_context);
        }
        public IUserRepository Users { get; set; }
        public IDeliveryStatusRepository DeliveryStatus { get; set; }
        public IModeOfDeliveryRepository ModeOfDelivery { get; set; }
        public ICSERepository CSE { get; set; }
        public IClientRepository Client { get; set; }
        public IDrmRepository DRM { get; set; }
        public IDeliveryProgressRepository DeliveryProgress { get; set; }
        public IDrmProgressRepository DrmProgress { get; set; }
        public ICourierRepository Courier { get; set; }
        public IEstimatedDeliveryRepository EstimatedDelivery { get; set; }
        public IAttachmentRepository Attachments { get; set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}