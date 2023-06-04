using BookStore.Models.Domain;
using BookStore.Repositorise.Abstract;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookStore.Repositorise.Implementation
{
    public class PublisherService : IPublisherService
    {
        private readonly DatabaseContext _databaseContext;
        public PublisherService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Publisher model)
        {
            try
            {
                _databaseContext.Publisher.Add(model);
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
                var data=_databaseContext.Publisher.FirstOrDefault(x => x.Id == id);   
                if (data != null)
                {
                    _databaseContext.Publisher.Remove(data);
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
        public Publisher FindById(int id)
        {
            return  _databaseContext.Publisher.FirstOrDefault(x => x.Id == id);
                
        }

        public IEnumerable<Publisher> GetAll()
        {
            return _databaseContext.Publisher.ToList();
        }

        public bool Update(Publisher model)
        {
            try
            {
                _databaseContext.Publisher.Update(model);
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
