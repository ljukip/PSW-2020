using PSW_bolnica.dao;
using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.services
{
    public class FeedbackService: IFeedbackService
    {
        private DBContext dbcontext;

        public FeedbackService(DBContext context)
        {
            dbcontext = context;
        }

        

        public FeedbackDao Add(FeedbackDao feedbackDao) { 
            Feedback feedback = FeedbackDao.feedbackDaoTofeedback(feedbackDao);

            dbcontext.feedbacks.Add(feedback);
            dbcontext.SaveChanges();

            return feedbackDao;
        }

        public Feedback Publish(int id)
        {
            Feedback feedback = dbcontext.feedbacks.FirstOrDefault(f => f.id == id);
            feedback.isPublished = true;
            dbcontext.SaveChanges();
            return feedback;
        }
        public Feedback Unpublish(int id)
        {
            Feedback feedback = dbcontext.feedbacks.FirstOrDefault(f => f.id == id);
            feedback.isPublished = false;
            dbcontext.SaveChanges();
            return feedback;
        }
    }
}
