using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.dao
{
    public class FeedbackDao
    {
        public int id { get; set; }
        public string text { get; set; }
        public bool isPublished { get; set; }
        public int patientId { get; set; }

        public static Feedback feedbackDaoTofeedback(FeedbackDao feedbackdao) {
            Feedback feedback = new Feedback
            {
                id = feedbackdao.id,
                text = feedbackdao.text,
                isPublished = feedbackdao.isPublished,
                patientId = feedbackdao.patientId,
            };
            return feedback;
        }

        public static FeedbackDao feedbackTofeedbackDao(Feedback feedback)
        {
            FeedbackDao feedbackDao = new FeedbackDao
            {
                id = feedback.id,
                text = feedback.text,
                isPublished = feedback.isPublished,
                patientId = feedback.patientId,
            };
            return feedbackDao;
        }
    }
}
