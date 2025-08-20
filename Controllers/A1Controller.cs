using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;
using Data;
using Models;
using Microsoft.AspNetCore.Http.Headers;
using Dtos;

[Route("webapi")]
[ApiController]
public class A1Controller : Controller
{
    private readonly IA1Repo _repository;

    public A1Controller(IA1Repo repository)
    {
        _repository = repository;
    }

    [HttpGet("GetVersion")]
    public ActionResult<String> GetVersion()
    {
        string result = _repository.GetVersion();
        return Ok(result);
    }


    // Response Header not sure
    [HttpGet("Logo")]
    public ActionResult Logo()
    {
        string path = Directory.GetCurrentDirectory();
        string logoDir = Path.Combine(path, "Logos");
        string logoPath = Path.Combine(logoDir, "Logo.svg");
        // Is the response header correct?
        string respHeader = "image/svg+xml";

        return PhysicalFile(logoPath, respHeader);

    }

    [HttpGet("AllArtefacts")]
    public ActionResult<IEnumerable<Artefact>> GetAllArtefacts()
    {
        IEnumerable<Artefact> allArtefacts = _repository.GetAllArtefacts();
        return Ok(allArtefacts);
    }

    [HttpGet("Artefacts/{searchTerm}")]
    public ActionResult<IEnumerable<Artefact>> GetArtefactsByDescription(string searchTerm)
    {
        IEnumerable<Artefact> artefacts = _repository.GetArtefactsByDescription(searchTerm);
        return Ok(artefacts);
    }

    [HttpGet("ArtefactImage/{ArtefactId}")]
    public ActionResult ArtefactImage(string ArtefactId)
    {
        string path = Directory.GetCurrentDirectory();
        string photoDir = Path.Combine(path, "ArtefactsImages");
        string photoPath1 = Path.Combine(photoDir, ArtefactId + ".jpg");
        string photoPath2 = Path.Combine(photoDir, ArtefactId + ".png");
        string photoPath3 = Path.Combine(photoDir, ArtefactId + ".gif");
        string defaultPath = Path.Combine(photoDir, "Default.svg");

        string fileName = defaultPath;
        string respHeader = "image/svg+xml";

        if (System.IO.File.Exists(photoPath1))
        {
            fileName = photoPath1;
            respHeader = "image/jpeg";
        }
        else if (System.IO.File.Exists(photoPath2))
        {
            fileName = photoPath2;
            respHeader = "image/png";
        }
        else if (System.IO.File.Exists(photoPath3))
        {
            fileName = photoPath3;
            respHeader = "image/gif";
        }

        return PhysicalFile(fileName, respHeader);
    }


    [HttpGet("GetComment/{id}")]
    public ActionResult<Comment> GetComment(int id)
    {
        Comment comment = _repository.GetComment(id);

        if (comment != null)
        {
            return Ok(comment);
        }
        else
        {
            return BadRequest($"Comment {id} does not exist");
        }


    }

    [HttpPost("WriteComment")]
    public ActionResult<Comment> WriteComment(CommentInput commentInput)
    {
        string name = commentInput.Name;
        string userComment = commentInput.UserComment;
        string time = DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ");
        string IP = HttpContext.Connection.RemoteIpAddress.ToString();

        Comment comment = new Comment
        {
            Name = name,
            UserComment = userComment,
            Time = time,
            IP = IP
        };

        Comment newComment = _repository.AddComment(comment);

        Comment outComment = new Comment
        {
            Id = newComment.Id,
            Name = newComment.Name,
            UserComment = newComment.UserComment,
            Time = newComment.Time,
            IP = "123.456.789.012"
        };


        return CreatedAtAction(nameof(GetComment), new { id = newComment.Id }, outComment);

    }



    [HttpGet("Comments/{numComment}")]
    public ActionResult<IEnumerable<Comment>> GetComments(int numComment = 5)
    {
        IEnumerable<Comment> comments = _repository.GetComments(numComment);
        return Ok(comments);
    }


    





}