using System;
using UnicomTIC_LMS_BE.DataBase;
using UnicomTIC_LMS_BE.Entities.Email;

namespace UnicomTIC_LMS_BE.Repositories
{
    public class SendMailRepository(LMSDbContext appDbContext)
    {
        public async Task<EmailTemplate> GetTemplate(EmailType emailTypes)
        {
            var template = appDbContext.EmailTemplates.Where(x => x.EmailType == emailTypes).FirstOrDefault();
            return template;
        }
    }
}
