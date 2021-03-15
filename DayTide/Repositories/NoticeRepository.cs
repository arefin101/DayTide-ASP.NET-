using DayTide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTide.Repositories
{
    public class NoticeRepository:Repository<Notice>
    {
        public List<Notice> GetNoticeByIdSend_For(string id)
        {
            return this.context.Notices.Where(x => x.Send_For == id).ToList();
        }
        public List<Notice> GetNoticeByIdSend_by(string id)
        {
            return this.context.Notices.Where(x => x.Send_by == id).ToList();
        }
        public Notice GetNoticeById(int id)
        {
            return this.context.Notices.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}