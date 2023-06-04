using BookStore.Models.Domain;
using BookStore.Repositorise.Abstract;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookStore.Repositorise.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext _databaseContext;
        public GenreService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Genre model)
        {
            try
            {
                _databaseContext.Genre.Add(model);
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
                var data=_databaseContext.Genre.FirstOrDefault(x => x.Id == id);   
                if (data != null)
                {
                    _databaseContext.Genre.Remove(data);
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
        public Genre FindById(int id)
        {
            return  _databaseContext.Genre.FirstOrDefault(x => x.Id == id);
                
        }

        public IEnumerable<Genre> GetAll()
        {
            return _databaseContext.Genre.ToList();
        }

        public bool Update(Genre model)
        {
            try
            {
                _databaseContext.Genre.Update(model);
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
