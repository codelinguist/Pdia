using System;
using System.Collections.Generic;
using System.Linq;
using CoreInfrastructure;
using System.Threading.Tasks;
using System.Threading;

namespace CoreInfrastructure.DataModel
{
	/// <summary>
	/// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata about the superset collection of objects this subset was created from.
	/// </summary>
	/// <remarks>
	/// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata about the superset collection of objects this subset was created from.
	/// </remarks>
	/// <typeparam name="T">The type of object the collection should contain.</typeparam>
	/// <seealso cref="IPagedList{T}"/>
	/// <seealso cref="BasePagedList{T}"/>
	/// <seealso cref="StaticPagedList{T}"/>
	/// <seealso cref="List{T}"/>
	[Serializable]
	public class PagedList<T> : BasePagedList<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PagedList{T}"/> class that divides the supplied superset into subsets the size of the supplied pageSize. The instance then only containes the objects contained in the subset specified by index.
		/// </summary>
		/// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}"/>, it will be treated as such.</param>
		/// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
		/// <param name="pageSize">The maximum size of any individual subset.</param>
		/// <exception cref="ArgumentOutOfRangeException">The specified index cannot be less than zero.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The specified page size cannot be less than one.</exception>
		public PagedList(IQueryable<T> superset, int pageNumber, int pageSize)
		{
			if (pageNumber < 1)
				throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
			if (pageSize < 1)
				throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");

			// set source to blank list if superset is null to prevent exceptions
			TotalItemCount = superset == null ? 0 : superset.Count();
            Load(pageNumber, pageSize);
			// add items to internal list
			if (superset != null && TotalItemCount > 0)
				Subset.AddRange(pageNumber == 1
					? superset.Skip(0).Take(pageSize).ToList()
					: superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
				);
		}

        private PagedList()
        {

        }

        private void Load(int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            PageCount = TotalItemCount > 0
                        ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                        : 0;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;
            FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
            var numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount
                            ? TotalItemCount
                            : numberOfLastItemOnPage;

        }

        internal static async Task<IPagedList<TEntity>> CreateAsync<TEntity>(IQueryable<TEntity> superset, int pageNumber, int pageSize) where TEntity:IEntity
        {
            var pagedList = new PagedList<TEntity>();

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");

            // set source to blank list if superset is null to prevent exceptions
            pagedList.TotalItemCount = superset == null ? 0 : await superset.CountAsync();
            pagedList.Load(pageNumber, pageSize);
            // add items to internal list
            if (superset != null && pagedList.TotalItemCount > 0)
                pagedList.Subset.AddRange(pageNumber == 1
                    ? await superset.Skip(0).Take(pageSize).ToListAsync()
                    : await superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
                );

            return pagedList;
        }


        internal static Task<IPagedList<T>> CreateAsync<T>(IQueryable<IEntity> superset, Func<IEntity, T> select, int pageNumber, int pageSize)
        {
            return CreateAsync(superset, select, pageNumber, pageSize, CancellationToken.None);
        }
        internal static async Task<IPagedList<T>> CreateAsync<T>(IQueryable<IEntity> superset, Func<IEntity, T> select, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var pagedList = new PagedList<T>();

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");

            // set source to blank list if superset is null to prevent exceptions
            pagedList.TotalItemCount = superset == null ? 0 : await superset.CountAsync();
            pagedList.Load(pageNumber, pageSize);
            // add items to internal list
            if (superset != null && pagedList.TotalItemCount > 0)
            {
                if (pageNumber == 1)
                {
                    await superset.Skip(0).Take(pageSize).ForEachAsync(entity => pagedList.Subset.Add(select(entity)), cancellationToken);
                }
                else
                {
                    await superset.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                        .ForEachAsync(entity => pagedList.Subset.Add(select(entity)), cancellationToken);
                }
            }

            return pagedList;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class that divides the supplied superset into subsets the size of the supplied pageSize. The instance then only containes the objects contained in the subset specified by index.
        /// </summary>
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <exception cref="ArgumentOutOfRangeException">The specified index cannot be less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The specified page size cannot be less than one.</exception>
        public PagedList(IEnumerable<T> superset, int pageNumber, int pageSize)
			: this(superset.AsQueryable<T>(), pageNumber, pageSize)
		{
		}
	}
}