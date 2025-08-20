using Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data
{
    public class A1Repo : IA1Repo
    {

        private readonly A1DbContext _dbContext;

        public A1Repo(A1DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string GetVersion()
        {
            return "1.0.0 (Ōpōtiki) by mugn737";
        }

        public IEnumerable<Artefact> GetAllArtefacts()
        {
            IEnumerable<Artefact> allArtefacts = _dbContext.Artefacts.ToList();
            return allArtefacts;
        }

        public IEnumerable<Artefact> GetArtefactsByDescription(string searchTerm)
        {
            IEnumerable<Artefact> artefacts = _dbContext.Artefacts.Where(a => a.Description.ToLower().Contains(searchTerm.ToLower()));
            return artefacts;

        }

        public Comment GetComment(int id)
        {
            Comment comment = _dbContext.Comments.FirstOrDefault(c => c.Id == id);
            return comment;
        }

        public Comment AddComment(Comment comment)
        {
            EntityEntry<Comment> e = _dbContext.Comments.Add(comment);
            Comment c = e.Entity;
            _dbContext.SaveChanges();
            return c;
        }

        public IEnumerable<Comment> GetComments(int numComment)
        {
            IEnumerable<Comment> comments = _dbContext.Comments.ToList();

            return comments.TakeLast(numComment).Reverse();  

        }


    }
}