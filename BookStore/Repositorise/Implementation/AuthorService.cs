using BookStore.Models.Domain;
using BookStore.Repositorise.Abstract;

namespace BookStore.Repositorise.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly DatabaseContext _databaseContext;
        public AuthorService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Author model)
        {
            try
            {
                _databaseContext.Author.Add(model);
                _databaseContext.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data=_databaseContext.Author.FirstOrDefault(x => x.Id == id);   
                if (data != null)
                {
                    _databaseContext.Author.Remove(data);
                    _databaseContext.SaveChanges();
                    return true;
                }
                return false;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Author FindById(int id)
        {
            return  _databaseContext.Author.FirstOrDefault(x => x.Id == id);
                
        }

        public IEnumerable<Author> GetAll()
        {
            return _databaseContext.Author.ToList();
        }

        public bool Update(Author model)
        {
            try
            {
                _databaseContext.Author.Update(model);
                _databaseContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
