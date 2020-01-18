using Microsoft.EntityFrameworkCore;
using System.Configuration;


namespace LandRegistry.Models
{
    public partial class landregistrydbContext : DbContext
    {
        public landregistrydbContext()
        {

        }

        public landregistrydbContext(DbContextOptions<landregistrydbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<CadEng> CadEng { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }
        public virtual DbSet<Registry> Registry { get; set; }
        public virtual DbSet<ServiceUnit> ServiceUnit { get; set; }
        public virtual DbSet<Settlement> Settlement { get; set; }
        public virtual DbSet<UsePurpose> UsePurpose { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ConfigurationManager.ConnectionStrings["landregistrydb"].ConnectionString, x => x.ServerVersion("8.0.18-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CadEng>(entity =>
            {
                entity.HasKey(e => e.CeId)
                    .HasName("PRIMARY");

                entity.ToTable("cad_eng");

                entity.Property(e => e.CeId)
                    .HasColumnName("ce_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.DisId)
                    .HasName("PRIMARY");

                entity.ToTable("district");

                entity.Property(e => e.DisId)
                    .HasColumnName("dis_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                .HasName("PRIMARY");

                entity.ToTable("users");

                entity.HasComment("aka database users");

                entity.Property(e => e.UserId)
                .HasColumnName("user_id")
                .HasColumnType("int(11)");

                entity.Property(e => e.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasColumnType("varchar(32)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.OwnId)
                    .HasName("PRIMARY");

                entity.ToTable("owner");

                entity.Property(e => e.OwnId)
                    .HasColumnName("own_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ConNum)
                    .IsRequired()
                    .HasColumnName("con_num")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Email)                    
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Inn)
                    .HasColumnName("inn")
                    .HasColumnType("varchar(12)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Patronymic)                    
                    .HasColumnName("patronymic")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Registry>(entity =>
            {
                entity.HasKey(e => e.CadNum)
                    .HasName("PRIMARY");

                entity.ToTable("registry");

                entity.HasComment("main table ya know");

                entity.HasIndex(e => e.CeId)
                    .HasName("fk_ce_id_idx");

                entity.HasIndex(e => e.DisId)
                    .HasName("fk_dis_id_idx");

                entity.HasIndex(e => e.OwnId)
                    .HasName("fk_own_id_idx");

                entity.HasIndex(e => e.SettlId)
                    .HasName("fk_settl_id_idx");

                entity.HasIndex(e => e.SuId)
                    .HasName("fk_su_id_idx");

                entity.HasIndex(e => e.UpId)
                    .HasName("fk_up_id_idx");

                entity.Property(e => e.CadNum)
                    .HasColumnName("cad_num")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Area)
                    .HasColumnName("area")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CeId)
                    .HasColumnName("ce_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DisId)
                    .HasColumnName("dis_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OwnId)
                    .HasColumnName("own_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SettlId)
                    .HasColumnName("settl_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SuId)
                    .HasColumnName("su_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpId)
                    .HasColumnName("up_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdTime)
                    .HasColumnName("upd_time")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Ce)
                    .WithMany(p => p.Registry)
                    .HasForeignKey(d => d.CeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ce_id");

                entity.HasOne(d => d.Dis)
                    .WithMany(p => p.Registry)
                    .HasForeignKey(d => d.DisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_dis_id");

                entity.HasOne(d => d.Own)
                    .WithMany(p => p.Registry)
                    .HasForeignKey(d => d.OwnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_own_id");

                entity.HasOne(d => d.Settl)
                    .WithMany(p => p.Registry)
                    .HasForeignKey(d => d.SettlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_settl_id");

                entity.HasOne(d => d.Su)
                    .WithMany(p => p.Registry)
                    .HasForeignKey(d => d.SuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_su_id");

                entity.HasOne(d => d.Up)
                    .WithMany(p => p.Registry)
                    .HasForeignKey(d => d.UpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_up_id");
            });

            modelBuilder.Entity<ServiceUnit>(entity =>
            {
                entity.HasKey(e => e.SuId)
                    .HasName("PRIMARY");

                entity.ToTable("service_unit");

                entity.Property(e => e.SuId)
                    .HasColumnName("su_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ChiefFullName)
                    .HasColumnName("chief_full_name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ConNum)
                    .IsRequired()
                    .HasColumnName("con_num")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("time");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("time");
            });

            modelBuilder.Entity<Settlement>(entity =>
            {
                entity.HasKey(e => e.SettlId)
                    .HasName("PRIMARY");

                entity.ToTable("settlement");

                entity.Property(e => e.SettlId)
                    .HasColumnName("settl_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<UsePurpose>(entity =>
            {
                entity.HasKey(e => e.UpId)
                    .HasName("PRIMARY");

                entity.ToTable("use_purpose");

                entity.Property(e => e.UpId)
                    .HasColumnName("up_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasColumnName("purpose")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
