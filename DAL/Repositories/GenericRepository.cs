using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
	public class GenericRepository<TEntity> where TEntity : class
	{
		internal MonopolyDbContext context;
		internal DbSet<TEntity> dbSet;

		internal GenericRepository(MonopolyDbContext context)
		{
			this.context = context;
			this.dbSet = context.Set<TEntity>();
		}

		public virtual IEnumerable<TEntity> Get(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "")
		{
			IQueryable<TEntity> query = dbSet;

			//if (typeof(TEntity) == typeof(Branch))
			//{
			//	foreach (var item in query)
			//	{
			//		(item as Branch).BranchType = context.BranchTypes.FirstOrDefault(p => p.Id == (item as Branch).BranchTypeId);
			//	}
			//}

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				return orderBy(query).ToList();
			}
			else
			{
				return query.ToList();
			}
		}

		public virtual TEntity GetFirstOrDef(Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "")
		{
			return Get(filter, orderBy, includeProperties).FirstOrDefault();
		}


		public virtual TEntity GetByID(object id)
		{
			return dbSet.Find(id);
		}

		public virtual void Insert(TEntity entity)
		{
			dbSet.Add(entity);
		}

		public virtual bool InsertCheck(TEntity entity)
		{
			try
			{
				var res =  dbSet.Add(entity) != null;
				if (res)
					context.SaveChanges();
				return res;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public virtual void Delete(object id)
		{
			TEntity entityToDelete = dbSet.Find(id);
			Delete(entityToDelete);
		}

		public virtual void Delete(TEntity entityToDelete)
		{
			if (context.Entry(entityToDelete).State == EntityState.Detached)
			{
				dbSet.Attach(entityToDelete);
			}
			dbSet.Remove(entityToDelete);
		}

		public virtual void Update(TEntity entityToUpdate)
		{
			dbSet.Attach(entityToUpdate);
			context.Entry(entityToUpdate).State = EntityState.Modified;
		}
	}
}
