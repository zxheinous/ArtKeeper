using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ArtKeeper.Models;

public partial class ArtKeeperContext : DbContext
{
    public ArtKeeperContext()
    {
    }

    public ArtKeeperContext(DbContextOptions<ArtKeeperContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClimateLog> ClimateLogs { get; set; }

    public virtual DbSet<ConditionPassport> ConditionPassports { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Exhibit> Exhibits { get; set; }

    public virtual DbSet<ExhibitExhibition> ExhibitExhibitions { get; set; }

    public virtual DbSet<Exhibition> Exhibitions { get; set; }

    public virtual DbSet<MaterialsInventory> MaterialsInventories { get; set; }

    public virtual DbSet<MuseumHall> MuseumHalls { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<RestorationDiary> RestorationDiaries { get; set; }

    public virtual DbSet<RestorationTask> RestorationTasks { get; set; }

    public virtual DbSet<RestorationWorkshop> RestorationWorkshops { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplyMaterial> SupplyMaterials { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=QWE\\QWE;Database=ArtKeeper;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClimateLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__ClimateL__9E2397E06FFB27A6");

            entity.ToTable("ClimateLog");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.CheckDatetime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("check_datetime");
            entity.Property(e => e.CurrentHumidity)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("current_humidity");
            entity.Property(e => e.CurrentTemperature)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("current_temperature");
            entity.Property(e => e.DeviationFlag).HasColumnName("deviation_flag");
            entity.Property(e => e.HallId).HasColumnName("hall_id");
            entity.Property(e => e.IlluminationLux).HasColumnName("illumination_lux");

            entity.HasOne(d => d.Hall).WithMany(p => p.ClimateLogs)
                .HasForeignKey(d => d.HallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClimateLog_MuseumHalls");
        });

        modelBuilder.Entity<ConditionPassport>(entity =>
        {
            entity.HasKey(e => e.PassportId).HasName("PK__Conditio__EC9E90ED8F15B605");

            entity.Property(e => e.PassportId).HasColumnName("passport_id");
            entity.Property(e => e.BaseDamage).HasColumnName("base_damage");
            entity.Property(e => e.DiagnosisDate).HasColumnName("diagnosis_date");
            entity.Property(e => e.ExhibitId).HasColumnName("exhibit_id");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.SurfaceDamage).HasColumnName("surface_damage");
            entity.Property(e => e.БиологическийФактор)
                .HasMaxLength(100)
                .HasColumnName("биологический_фактор");
            entity.Property(e => e.Заключение).HasColumnName("заключение");

            entity.HasOne(d => d.Exhibit).WithMany(p => p.ConditionPassports)
                .HasForeignKey(d => d.ExhibitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConditionPassports_Exhibits");

            entity.HasOne(d => d.Expert).WithMany(p => p.ConditionPassports)
                .HasForeignKey(d => d.ExpertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConditionPassports_Users");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB852F3CC3F0");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CategoryType)
                .HasMaxLength(30)
                .HasColumnName("category_type");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .HasColumnName("customer_name");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Customers_Users");
        });

        modelBuilder.Entity<Exhibit>(entity =>
        {
            entity.HasKey(e => e.ExhibitId).HasName("PK__Exhibits__26AFB897A990E2A3");

            entity.HasIndex(e => e.InventoryNumber, "UQ__Exhibits__070F296CECDE0636").IsUnique();

            entity.Property(e => e.ExhibitId).HasColumnName("exhibit_id");
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .HasDefaultValue("Неизвестный автор")
                .HasColumnName("author");
            entity.Property(e => e.CreationDate)
                .HasMaxLength(50)
                .HasColumnName("creation_date");
            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(50)
                .HasColumnName("current_status");
            entity.Property(e => e.Dimensions)
                .HasMaxLength(50)
                .HasColumnName("dimensions");
            entity.Property(e => e.HallId).HasColumnName("hall_id");
            entity.Property(e => e.InventoryNumber)
                .HasMaxLength(50)
                .HasColumnName("inventory_number");
            entity.Property(e => e.MaterialTechnique)
                .HasMaxLength(150)
                .HasColumnName("material_technique");
            entity.Property(e => e.SafetyCategory)
                .HasMaxLength(30)
                .HasColumnName("safety_category");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");

            entity.HasOne(d => d.Hall).WithMany(p => p.Exhibits)
                .HasForeignKey(d => d.HallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exhibits_MuseumHalls");
        });

        modelBuilder.Entity<ExhibitExhibition>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__ExhibitE__BFCFB4DDCC263141");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.ExhibitId).HasColumnName("exhibit_id");
            entity.Property(e => e.ExhibitionId).HasColumnName("exhibition_id");
            entity.Property(e => e.PlacementLocation)
                .HasMaxLength(100)
                .HasColumnName("placement_location");

            entity.HasOne(d => d.Exhibit).WithMany(p => p.ExhibitExhibitions)
                .HasForeignKey(d => d.ExhibitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExhibitExhibitions_Exhibits");

            entity.HasOne(d => d.Exhibition).WithMany(p => p.ExhibitExhibitions)
                .HasForeignKey(d => d.ExhibitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExhibitExhibitions_Exhibitions");
        });

        modelBuilder.Entity<Exhibition>(entity =>
        {
            entity.HasKey(e => e.ExhibitionId).HasName("PK__Exhibiti__3EB7E11D234719C6");

            entity.Property(e => e.ExhibitionId).HasColumnName("exhibition_id");
            entity.Property(e => e.CuratorId).HasColumnName("curator_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");
            entity.Property(e => e.TotalBudget)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("total_budget");

            entity.HasOne(d => d.Curator).WithMany(p => p.Exhibitions)
                .HasForeignKey(d => d.CuratorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exhibitions_Users");
        });

        modelBuilder.Entity<MaterialsInventory>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__6BFE1D28966C561F");

            entity.ToTable("MaterialsInventory");

            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CurrentQuantity)
                .HasColumnType("decimal(10, 3)")
                .HasColumnName("current_quantity");
            entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");
            entity.Property(e => e.MaterialName)
                .HasMaxLength(100)
                .HasColumnName("material_name");
            entity.Property(e => e.UnitMeasure)
                .HasMaxLength(20)
                .HasColumnName("unit_measure");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.MaterialsInventories)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaterialsInventory_Warehouses");
        });

        modelBuilder.Entity<MuseumHall>(entity =>
        {
            entity.HasKey(e => e.HallId).HasName("PK__MuseumHa__A63DE8CFC6186D79");

            entity.Property(e => e.HallId).HasColumnName("hall_id");
            entity.Property(e => e.CurrentCount).HasColumnName("current_count");
            entity.Property(e => e.Floor).HasColumnName("floor");
            entity.Property(e => e.HallName)
                .HasMaxLength(100)
                .HasColumnName("hall_name");
            entity.Property(e => e.MaxCapacity).HasColumnName("max_capacity");
            entity.Property(e => e.ResponsibleId).HasColumnName("responsible_id");
            entity.Property(e => e.TargetHumidity)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("target_humidity");
            entity.Property(e => e.TargetTemperature)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("target_temperature");

            entity.HasOne(d => d.Responsible).WithMany(p => p.MuseumHalls)
                .HasForeignKey(d => d.ResponsibleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MuseumHalls_Users");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__4659622917B7748A");

            entity.HasIndex(e => e.OrderNumber, "UQ__Orders__730E34DF98C6CB59").IsUnique();

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.ExhibitionId).HasColumnName("exhibition_id");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(50)
                .HasColumnName("order_number");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .HasColumnName("order_status");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.TourDatetime)
                .HasColumnType("datetime")
                .HasColumnName("tour_datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.Exhibition).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ExhibitionId)
                .HasConstraintName("FK_Orders_Exhibitions");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__779B7C58101B8184");

            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.GeneratedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("generated_at");
            entity.Property(e => e.GeneratedBy).HasColumnName("generated_by");
            entity.Property(e => e.ParametersJson).HasColumnName("parameters_json");
            entity.Property(e => e.ReportType)
                .HasMaxLength(50)
                .HasColumnName("report_type");

            entity.HasOne(d => d.GeneratedByNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.GeneratedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reports_Users");
        });

        modelBuilder.Entity<RestorationDiary>(entity =>
        {
            entity.HasKey(e => e.DiaryId).HasName("PK__Restorat__339232C8445AF321");

            entity.Property(e => e.DiaryId).HasColumnName("diary_id");
            entity.Property(e => e.ActionsTaken).HasColumnName("actions_taken");
            entity.Property(e => e.EntryDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("entry_date");
            entity.Property(e => e.OperationType)
                .HasMaxLength(100)
                .HasColumnName("operation_type");
            entity.Property(e => e.PhotoPath)
                .HasMaxLength(255)
                .HasColumnName("photo_path");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.Task).WithMany(p => p.RestorationDiaries)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_RestorationDiaries_Tasks");
        });

        modelBuilder.Entity<RestorationTask>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Restorat__0492148D44A61A1A");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.CommissionActNumber)
                .HasMaxLength(50)
                .HasColumnName("commission_act_number");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ExhibitId).HasColumnName("exhibit_id");
            entity.Property(e => e.RestorerId).HasColumnName("restorer_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TaskDescription).HasColumnName("task_description");
            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");

            entity.HasOne(d => d.Exhibit).WithMany(p => p.RestorationTasks)
                .HasForeignKey(d => d.ExhibitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RestorationTasks_Exhibits");

            entity.HasOne(d => d.Restorer).WithMany(p => p.RestorationTasks)
                .HasForeignKey(d => d.RestorerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RestorationTasks_Users");

            entity.HasOne(d => d.Workshop).WithMany(p => p.RestorationTasks)
                .HasForeignKey(d => d.WorkshopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RestorationTasks_Workshops");
        });

        modelBuilder.Entity<RestorationWorkshop>(entity =>
        {
            entity.HasKey(e => e.WorkshopId).HasName("PK__Restorat__EA6B0559572A99F4");

            entity.Property(e => e.WorkshopId).HasColumnName("workshop_id");
            entity.Property(e => e.HeadId).HasColumnName("head_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Specialization)
                .HasMaxLength(100)
                .HasColumnName("specialization");
            entity.Property(e => e.WorkshopName)
                .HasMaxLength(100)
                .HasColumnName("workshop_name");

            entity.HasOne(d => d.Head).WithMany(p => p.RestorationWorkshops)
                .HasForeignKey(d => d.HeadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RestorationWorkshops_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CC6DDD0335");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__6EE594E89E6C3D2A");

            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .HasColumnName("company_name");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(100)
                .HasColumnName("contact_person");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Inn)
                .HasMaxLength(12)
                .HasColumnName("inn");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<SupplyMaterial>(entity =>
        {
            entity.HasKey(e => e.SupplyId).HasName("PK__SupplyMa__4870CD830F3B1618");

            entity.Property(e => e.SupplyId).HasColumnName("supply_id");
            entity.Property(e => e.MaterialName)
                .HasMaxLength(100)
                .HasColumnName("material_name");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(10, 3)")
                .HasColumnName("quantity");
            entity.Property(e => e.ReceivedBy).HasColumnName("received_by");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.SupplyDate).HasColumnName("supply_date");
            entity.Property(e => e.TotalCost)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("total_cost");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.ReceivedByNavigation).WithMany(p => p.SupplyMaterials)
                .HasForeignKey(d => d.ReceivedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplyMaterials_Users");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplyMaterials)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplyMaterials_Suppliers");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F6AD1907E");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164E0EE3DBD").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5727CFF8D4A").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastLogin)
                .HasColumnType("datetime")
                .HasColumnName("last_login");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.WarehouseId).HasName("PK__Warehous__734FE6BF84C14449");

            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.ResponsibleId).HasColumnName("responsible_id");
            entity.Property(e => e.WarehouseName)
                .HasMaxLength(100)
                .HasColumnName("warehouse_name");

            entity.HasOne(d => d.Responsible).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.ResponsibleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouses_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
