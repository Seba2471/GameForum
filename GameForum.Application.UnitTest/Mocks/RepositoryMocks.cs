using GameForum.Application.Contracts.Persistence;
using GameForum.Domain.Entities;
using Moq;

namespace GameForum.Application.UnitTest.Mocks
{
    public class RepositoryMocks
    {

        public static Mock<ITopicRepository> GetTopicRepository()
        {
            var topics = GetTopics();

            var mockTopicRepository = new Mock<ITopicRepository>();

            mockTopicRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(topics);

            mockTopicRepository.Setup(repo => repo.GetTopicByIdWithPostsList(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var topic = GetTopicByIdWithPostsList(id);
                    return topic;
                });


            mockTopicRepository.Setup(repo => repo.AddAsync(It.IsAny<Topic>())).ReturnsAsync(
                (Topic topic) =>
                {
                    topics.Add(topic);
                    return topic;
                });


            return mockTopicRepository;
        }

        private static List<Topic> GetTopics()
        {
            Topic t1 = new Topic()
            {
                TopicId = 1,
                Title = "Pomoc z ekwipunkiem",
                Content = "Proszę o pomoc z ekwipunkiem na 35 lvl",
                DepartmentId = 1
            };

            Topic t2 = new Topic()
            {
                TopicId = 2,
                Title = "Pomoc z expowiskiem",
                Content = "Gdzie moge wbijać poziom na 40 lvl ?",
                DepartmentId = 2,
            };

            List<Topic> topics = new List<Topic>();

            topics.Add(t1);
            topics.Add(t2);

            return topics;
        }

        private static Topic GetTopicByIdWithPostsList(int i)
        {
            var posts = GetPosts();

            Topic t1 = new Topic()
            {
                TopicId = i,
                Title = "Pomoc z ekwipunkiem",
                Content = "Proszę o pomoc z ekwipunkiem na 35 lvl",
                DepartmentId = 1,
                Posts = posts.Where(p => p.TopicId == 1).ToList()
            };

            return t1;
        }


        public static Mock<IPostRepository> GetPostRepository()
        {
            var posts = GetPosts();

            var mockPostRepository = new Mock<IPostRepository>();

            mockPostRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(posts);

            mockPostRepository.Setup(repo => repo.AddAsync(It.IsAny<Post>())).ReturnsAsync(
                (Post post) =>
                {
                    posts.Add(post);
                    return post;
                });

            return mockPostRepository;
        }

        private static List<Post> GetPosts()
        {
            Post p1 = new Post()
            {
                PostId = 1,
                Content = "Dwuręczny miecz",
                TopicId = 1
            };
            Post p2 = new Post()
            {
                PostId = 2,
                Content = "Gobliny polecam bardzo",
                TopicId = 2
            };
            Post p3 = new Post()
            {
                PostId = 3,
                Content = "Jednoręczny lepszy",
                TopicId = 1
            };

            List<Post> posts = new List<Post>();

            posts.Add(p1);
            posts.Add(p2);
            posts.Add(p3);

            return posts;
        }
    }
}
