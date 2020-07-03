using AskApp.Ask.DAL.Entities;
using AskApp.Ask.DAL.Extensions;
using AskApp.Common.Interfaces.IRepositories;
using AskApp.Common.TOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskApp.Ask.DAL.Repositories
{
    public class AskUserRepository : IAskUserRepository
    {
        private AskContext askContext;
        public AskUserRepository(AskContext askContext)
        {
            this.askContext = askContext;
        }

        public void Dispose()
        {
            askContext.Dispose();
        }

        public AskUserTO Create(AskUserTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }
            if (entity.Id != 0)
            {
                return entity;
            }
            var entityEF = askContext.AskUsers.Add(entity.ToEF());
            askContext.SaveChanges();

            return entityEF.Entity.ToTransferObject();
        }

        public bool Delete(AskUserTO entity)
        {
            if (entity is null)
            {
                throw new KeyNotFoundException();
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("AskUser To Delete Invalid Id");
            }

            var askUser = askContext.AskUsers.FirstOrDefault(x => x.Id == entity.Id);
            askContext.AskUsers.Remove(askUser);
            askContext.SaveChanges();
            return true;
        }

        public List<AskUserTO> GetAll()
        {
            var list = askContext.AskUsers.AsEnumerable()
                 ?.Select(x => x.ToTransferObject())
                 .ToList();
            if (!list.Any())
            {
                throw new ArgumentNullException("There is no AskUser in DB");
            }
            return list;
        }

        public AskUserTO GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("AskUser not found, invalid Id");
            }
            return askContext.AskUsers.FirstOrDefault(x => x.Id == id).ToTransferObject();
        }

        public AskUserTO Modify(AskUserTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("AskUser To Update Invalid Id");
            }
            if (!askContext.AskUsers.Any(x => x.Id == entity.Id))
            {
                throw new KeyNotFoundException($"Update(AskUserTO) Can't find AskUser to update.");
            }

            var editedEntity = askContext.AskUsers.FirstOrDefault(e => e.Id == entity.Id);
            if (editedEntity != default)
            {
                entity.ToTrackedEF(editedEntity);
            }
            askContext.SaveChanges();

            return editedEntity.ToTransferObject();
        }
    }
}
