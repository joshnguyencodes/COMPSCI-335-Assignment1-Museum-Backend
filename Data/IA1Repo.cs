using Models;

namespace Data
{
    public interface IA1Repo
    {
        String GetVersion();

        IEnumerable<Artefact> GetAllArtefacts();

        IEnumerable<Artefact> GetArtefactsByDescription(string searchTerm);

        Comment GetComment(int id);

        Comment AddComment(Comment comment);

        IEnumerable<Comment> GetComments(int numComment);
    }
}