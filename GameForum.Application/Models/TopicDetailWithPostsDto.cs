using GameForum.Application.Models.Pagination;

namespace GameForum.Application.Models
{
    public class TopicDetailWithPostsDto
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public AuthorDto Author { get; set; }
        public PaginationResponse<PostDto> Posts { get; set; }
    }
}
