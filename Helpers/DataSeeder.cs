using TaskRecordingSystem.Models;

public static class DataSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Tasks.Any())
            return; // už jsou data, končíme

        var random = new Random();

        // 1. Vytvoření firem
        var companies = new List<Company>();
        for (int i = 1; i <= 5; i++)
        {
            companies.Add(new Company
            {
                Name = $"Firma {i}",
                Description = $"Popis firmy {i}"
            });
        }
        db.Companies.AddRange(companies);

        // 2. Vytvoření uživatelů
        var users = new List<AppUser>();
        for (int i = 1; i <= 20; i++)
        {
            var company = companies[random.Next(companies.Count)];
            users.Add(new AppUser
            {
                FirstName = $"Uživatel{i}",
                Surname = $"Příjmení{i}",
                Email = $"user{i}@example.com",
                Company = company
            });
        }
        db.Users.AddRange(users);

        // 3. Vytvoření úkolů
        var statuses = Enum.GetValues(typeof(TaskItemStatus)).Cast<TaskItemStatus>().ToList();

        for (int i = 1; i <= 100; i++)
        {
            var reporter = users[random.Next(users.Count)];
            var assignee = users[random.Next(users.Count)];
            var company = reporter.Company;

            var status = statuses[random.Next(statuses.Count)];
            var reportedDate = DateTime.Today.AddDays(-random.Next(30));
            var dueDate = DateTime.Today.AddDays(random.Next(1, 15));
            DateTime? resolvedDate = null;

            if (status == TaskItemStatus.Resolved)
            {
                resolvedDate = reportedDate.AddDays(random.Next((DateTime.Today - reportedDate).Days + 1));
            }

            db.Tasks.Add(new TaskItem
            {
                Title = $"Úkol {i}",
                Description = $"Popis úkolu {i}",
                Priority = random.Next(1, 4),
                Status = status,
                ReportedDate = reportedDate,
                DueDate = dueDate,
                ResolvedDate = resolvedDate,
                Reporter = reporter,
                Assignee = assignee,
                Company = company
            });
        }

        db.SaveChanges();
    }
}