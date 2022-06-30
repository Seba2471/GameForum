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
            };

            Topic t2 = new Topic()
            {
                TopicId = 2,
                Title = "Pomoc z expowiskiem",
                Content = "Gdzie moge wbijać poziom na 40 lvl ?",
            };

            List<Topic> topics = new List<Topic>();

            topics.Add(t1);
            topics.Add(t2);

            return topics;
        }

        private static Topic GetTopicByIdWithPostsList(int id)
        {
            var posts = GetPosts();


            var topics = GetTopics();

            var topic = topics.FirstOrDefault(t => t.TopicId == id);

            topic.Posts = posts.Where(p => p.TopicId == id).ToList();

            return topic;
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

            mockPostRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var post = posts.Find(p => p.PostId == id);
                    return post;
                });

            mockPostRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Post>())).ReturnsAsync(
                (Post post) =>
                {
                    var postToUpdate = posts.First(p => p.PostId == post.PostId);

                    postToUpdate.Content = post.Content;

                    return postToUpdate;
                });

            mockPostRepository.Setup(repo => repo.IsPostExists(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return posts.Any(p => p.PostId == id);
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
