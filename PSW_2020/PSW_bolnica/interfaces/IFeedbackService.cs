using PSW_bolnica.dao;
using PSW_bolnica.model;

namespace PSW_bolnica.services
{
    public interface IFeedbackService
    {
        public FeedbackDao Add(FeedbackDao feedbackDao);
        public Feedback Publish(int id);

        public Feedback Unpublish(int id);
    }
}