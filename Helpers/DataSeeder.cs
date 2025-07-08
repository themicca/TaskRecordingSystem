using TaskRecordingSystem.Models;

public static class DataSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Tasks.Any())
            return; // už jsou data, končíme

        // 1. Vytvoření firmy
        var company1 = new Company
        {
            Name = "Malá firma",
            Description = "Firma pro demo účely"
        };

        var company2 = new Company
        {
            Name = "Velká firma",
            Description = "Firma pro demo účely"
        };

        db.Companies.AddRange(company1, company2);

        // 2. Vytvoření uživatelů
        var user1 = new AppUser
        {
            FirstName = "Jan",
            Surname = "Novák",
            Email = "jan.novak@example.com",
            Company = company1
        };

        var user2 = new AppUser
        {
            FirstName = "Petra",
            Surname = "Malá",
            Email = "petra.mala@example.com",
            Company = company1
        };

        db.Users.AddRange(user1, user2);

        // 3. Úkol
        var task = new TaskItem
        {
            Title = "Nastavit testovací prostředí",
            Description = "Připravit základní instalace a databázi",
            Priority = 2,
            Status = "Open",
            ReportedDate = DateTime.Today.AddDays(-1),
            DueDate = DateTime.Today.AddDays(3),
            Reporter = user1,
            Assignee = user2,
            Company = company1
        };

        db.Tasks.Add(task);

        db.SaveChanges();
    }
}