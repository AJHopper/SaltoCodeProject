using Microsoft.EntityFrameworkCore;
using SaltoCodeProject.Database;
using SaltoCodeProject.Entities;
using SaltoCodeProject.Helpers;
using System.Linq;

namespace SaltoCodeProject.Services
{
    public interface ILockService
    {
        bool UnlockDoor(int lockId, int userId);
        bool LockDoor(int lockId, int userId);
        Lock CreateLock(Lock lockCreateModel);
        void UpdateLock(Lock lockCreateModel);
        void AddUserToLock(int userId, int lockId);
        void DeleteLock(int lockId);
    }

    public class LockService : ILockService
    {
        private readonly DatabaseContext _context;

        public LockService(DatabaseContext context)
        {
            _context = context;
        }

        public void AddUserToLock(int userId, int lockId)
        {
            var lockToModify = _context.Locks.Include(x=> x.AuthorisedUserIds).SingleOrDefault(x=> x.Id == lockId);
            var userToAdd = _context.Users.Find(userId);

            if(lockToModify == null)
            {
                throw new AppException("Lock not found");
            }
            if (userToAdd == null)
            {
                throw new AppException("User not found");
            }

            if (lockToModify.AuthorisedUserIds.Any(x => x.Id == userToAdd.Id)) return;

            lockToModify.AuthorisedUserIds.Add(userToAdd);
            _context.SaveChanges();
        }

        public Lock CreateLock(Lock lockCreateModel)
        {
            if (_context.Locks.Any(x => x.Location == lockCreateModel.Location))
            {
                throw new AppException("Lock already exists at this location");
            }

            _context.Locks.Add(lockCreateModel);
            _context.SaveChanges();

            return lockCreateModel;
        }

        public void DeleteLock(int lockId)
        {
            var lockToDelete = _context.Locks.Find(lockId);
            if (lockToDelete != null)
            {
                _context.Locks.Remove(lockToDelete);
                _context.SaveChanges();
            }
        }

        public bool LockDoor(int lockId, int userId)
        {
            var lockToModify = _context.Locks.Include(x => x.AuthorisedUserIds).SingleOrDefault(x => x.Id == lockId);

            if (lockToModify == null)
            {
                throw new AppException("Lock not found");
            }

            if (lockToModify.AuthorisedUserIds.Any(x => x.Id == userId))
            {
                lockToModify.IsLocked = true;
                _context.SaveChanges();
                return true;
            }
            else
            {
                throw new AppException("User not authorised to lock");
            }
        }

        public bool UnlockDoor(int lockId, int userId)
        {
            var lockToModify = _context.Locks.Include(x => x.AuthorisedUserIds).SingleOrDefault(x => x.Id == lockId);

            if (lockToModify == null)
            {
                throw new AppException("Lock not found");
            }
            
            if (lockToModify.AuthorisedUserIds.Any(x => x.Id == userId))
            {
                lockToModify.IsLocked = false;
                _context.SaveChanges();
                return true;
            }
            else
            {
                throw new AppException("User not authorised to unlock");
            }
        }

        public void UpdateLock(Lock lockCreateModel)
        {
            var lockToModify = _context.Locks.Find(lockCreateModel.Id);

            if (lockToModify == null)
            {
                throw new AppException("Lock not found");
            }

            if (_context.Locks.Any(x => x.Location == lockCreateModel.Location))
                throw new AppException("Location " + lockCreateModel.Location + " is already taken");

            lockToModify.Location = lockCreateModel.Location;
            lockToModify.Name = lockCreateModel.Name;

            _context.SaveChanges();
        }
    }
}
