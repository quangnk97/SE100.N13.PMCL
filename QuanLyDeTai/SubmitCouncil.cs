using QuanLyDeTai.Models;

namespace QuanLyDeTai
{
    public class SubmitCouncil
    {
        public Assessment Assessment { get; set; }
        public Topic Topic { get; set; }
        public List<Lecturer> Lecturers { get; set; }
    }
}
