public static class DataCleaner
{
    public static void Clear(AppDbContext db)
    {
        // Správné pořadí mazání kvůli FK závislostem:
        db.ChatMessages.RemoveRange(db.ChatMessages);
        db.ChecklistItems.RemoveRange(db.ChecklistItems);
        db.Attachments.RemoveRange(db.Attachments);
        db.Tasks.RemoveRange(db.Tasks);
        db.Users.RemoveRange(db.Users);
        db.Companies.RemoveRange(db.Companies);

        db.SaveChanges();
    }
}