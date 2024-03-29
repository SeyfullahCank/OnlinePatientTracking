using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnswerQuestion.API.Models;

namespace AnswerQuestion.API.Data
{
    public class AnswerQuestionAPIContext : DbContext
    {
        public AnswerQuestionAPIContext (DbContextOptions<AnswerQuestionAPIContext> options)
            : base(options)
        {
        }

        public DbSet<AnswerQuestion.API.Models.Question> Question { get; set; } = default!;
        public DbSet<AnswerQuestion.API.Models.Answer> Answer { get; set; } = default!;
    }
}
