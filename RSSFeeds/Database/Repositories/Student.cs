using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RSSFeeds.Database.Repositories
{
	public class UserRepository
	{
		private readonly testcontext _context;
		public UserRepository(testcontext context)
		{
			_context = context;
		}
		public async Task<PagedList<Student>> GetUsers(string searchterm , int pagesize,int pagenum, string order ,string sortby)
		{
			IQueryable<Student> productQuery = _context.Student;
			if (!string.IsNullOrEmpty(searchterm))
			{
				productQuery = productQuery.Where(n => n.Name.Contains(searchterm));
			}
			Expression<Func<Student, object>> orderby = order switch
			{
				"name" => x => x.Name,
				_ => x => x.Id,

			};
			if (sortby== "asc")
			{
				productQuery = productQuery.OrderBy(orderby);
			}
			else
			{
				productQuery = productQuery.OrderByDescending(orderby);
			}
			var students = await PagedList<Student>.CreateAsync(productQuery,pagenum,pagesize);
			return students;
		}
	}
}
