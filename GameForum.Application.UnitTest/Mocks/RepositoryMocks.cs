using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Models;
using GameForum.Application.Models.Pagination;
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

            mockTopicRepository.Setup(repo => repo.GetByIdWithAuthor(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var topic = topics.FirstOrDefault(t => t.TopicId == id);

                    topic.Author = GetUsers().FirstOrDefault(u => u.Id == topic.AuthorId);

                    return topic;
                });

            mockTopicRepository.Setup(repo => repo.AddAsync(It.IsAny<Topic>())).ReturnsAsync(
                (Topic topic) =>
                {
                    topics.Add(topic);
                    return topic;
                });

            mockTopicRepository.Setup(repo => repo.TopicExists(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return topics.Any(topic => topic.TopicId == id);
            });

            mockTopicRepository.Setup(repo => repo.GetPageAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(
                (int pageNumber, int pageSize) =>
            {

                var mapper = new AutoMapper.Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TopicDto, Topic>().ReverseMap()));

                var topicsFromDb = topics
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .ToList();

                var totalCount = topics.Count();

                var items = mapper.Map<List<TopicDto>>(topicsFromDb);

                return new PaginationResponse<TopicDto>(items, totalCount, pageSize, pageNumber);
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
                AuthorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e"
            };

            Topic t2 = new Topic()
            {
                TopicId = 2,
                Title = "Pomoc z expowiskiem",
                Content = "Gdzie moge wbijać poziom na 40 lvl ?",
                AuthorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e"
            };

            List<Topic> topics = new List<Topic>();

            topics.Add(t1);
            topics.Add(t2);

            return topics;
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

            mockPostRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Post>())).Callback(
                (Post post) =>
                {
                    var postToUpdate = posts.First(p => p.PostId == post.PostId);

                    postToUpdate.Content = post.Content;
                });

            mockPostRepository.Setup(repo => repo.PostExists(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return posts.Any(p => p.PostId == id);
                });

            mockPostRepository.Setup(repo =>
                repo.GetPageByTopicIdAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(
                (int pageNumber, int pageSize, int id) =>
                {
                    var topicPosts = posts.Where(p => p.TopicId == id).ToList();

                    var mapper = new AutoMapper.Mapper(new MapperConfiguration(cfg => cfg.CreateMap<PostDto, Post>().ReverseMap()));

                    var postsFromDb = topicPosts
                        .Skip(pageSize * (pageNumber - 1))
                        .Take(pageSize)
                        .ToList();

                    var totalCount = topicPosts.Count();

                    var items = mapper.Map<List<PostDto>>(postsFromDb);

                    return new PaginationResponse<PostDto>(items, totalCount, pageSize, pageNumber);
                });


            return mockPostRepository;
        }

        private static List<ApplicationUser> GetUsers()
        {
            ApplicationUser user1 = new ApplicationUser()
            {
                Id = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e",
                UserName = "Typowy gracz"
            };

            List<ApplicationUser> users = new List<ApplicationUser>();

            users.Add(user1);

            return users;
        }


        private static List<Post> GetPosts()
        {
            Post p1 = new Post()
            {
                PostId = 1,
                Content = "Dwuręczny miecz",
                TopicId = 1,
                AuthorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e"
            };
            Post p2 = new Post()
            {
                PostId = 2,
                Content = "Gobliny polecam bardzo",
                TopicId = 2,
                AuthorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e"
            };
            Post p3 = new Post()
            {
                PostId = 3,
                Content = "Jednoręczny lepszy",
                TopicId = 1,
                AuthorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e"
            };

            Post p4 = new Post()
            {
                PostId = 4,
                Content = "Tylko sztylety",
                TopicId = 1,
                AuthorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e"
            };

            List<Post> posts = new List<Post>();

            posts.Add(p1);
            posts.Add(p2);
            posts.Add(p3);
            posts.Add(p4);

            return posts;
        }
    }
}
