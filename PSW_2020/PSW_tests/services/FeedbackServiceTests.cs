using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSW_bolnica.dao;
using PSW_bolnica.model;
using PSW_bolnica.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSW_bolnica.services.Tests
{
    [TestClass()]
    public class FeedbackServiceTests
    {
        private DbContextOptions<DBContext> _options;

        public FeedbackServiceTests()
        {
            _options = new DbContextOptionsBuilder<DBContext>().UseInMemoryDatabase(databaseName: "DBC_Feedbacks").Options;
        }

        private void SetupDatabase(DBContext context)
        {
            context.Database.EnsureDeleted();
            CreateFeedbacks(context);
        }
        private void CreateFeedbacks(DBContext context)
        {
            context.feedbacks.Add(new Feedback
            {
                id=1,
                isPublished=false,

            });

            context.feedbacks.Add(new Feedback
            {
                id = 2,
                isPublished = true,

            });

            context.SaveChanges();
        }
        [TestMethod()]
        public void AddTest()
        {
            using (var context = new DBContext(_options))
            {
                SetupDatabase(context);
                FeedbackService service = new FeedbackService(context);
                FeedbackDao feedbackDao = new FeedbackDao();
                service.Add(feedbackDao);

                Assert.AreEqual(context.feedbacks.ToList().Count(), 3);
            }
        }

        [TestMethod()]
        public void PublishTest()
        {
            using (var context = new DBContext(_options))
            {
                SetupDatabase(context);
                FeedbackService service = new FeedbackService(context);
                service.Publish(1);

                Assert.AreEqual(context.feedbacks.Find(1).isPublished, true);
            }
        }

        [TestMethod()]
        public void UnpublishTest()
        {
            using (var context = new DBContext(_options))
            {
                SetupDatabase(context);
                FeedbackService service = new FeedbackService(context);
                service.Unpublish(1);

                Assert.AreEqual(context.feedbacks.Find(1).isPublished, false);
            }
        }
    }
}