namespace AspContactApi.Controllers
{
    public class AddPostRequestDto
    {
        public string FullName { get; internal set; }
        public string Email { get; internal set; }
        public string Address { get; internal set; }
        public string Phone { get; internal set; }
    }
}