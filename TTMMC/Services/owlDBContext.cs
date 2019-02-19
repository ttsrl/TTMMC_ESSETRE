using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TTMMC_ESSETRE.Models
{
    public partial class owlDBContext : DbContext
    {
        public owlDBContext()
        {
        }

        public owlDBContext(DbContextOptions<owlDBContext> options): base(options)
        {
        }

        public virtual DbSet<Decofast35Datiesterni> Decofast35Datiesterni { get; set; }
        public virtual DbSet<Decofast35Getvalue> Decofast35Getvalue { get; set; }
        public virtual DbSet<Decofast35Lavorazionemacchina> Decofast35Lavorazionemacchina { get; set; }
        public virtual DbSet<Decofast35Ricetta> Decofast35Ricetta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=10.198.156.253;Initial Catalog=owlDB;Persist Security Info=True;User ID=sa;Password=azsx.2012;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Decofast35Datiesterni>(entity =>
            {
                entity.ToTable("decofast_3_5_datiesterni");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodiceArticolo).HasColumnName("codiceArticolo");

                entity.Property(e => e.Colore)
                    .HasColumnName("colore")
                    .HasMaxLength(100);

                entity.Property(e => e.DataDisposizione)
                    .HasColumnName("dataDisposizione")
                    .HasColumnType("date");

                entity.Property(e => e.DescrizioneArticolo).HasColumnName("descrizioneArticolo");

                entity.Property(e => e.FaseDisposizione).HasColumnName("faseDisposizione");

                entity.Property(e => e.MetriDisposti).HasColumnName("metriDisposti");

                entity.Property(e => e.NomeMacchina)
                    .IsRequired()
                    .HasColumnName("nomeMacchina")
                    .HasMaxLength(400);

                entity.Property(e => e.NumDisposizione).HasColumnName("numDisposizione");

                entity.Property(e => e.NumeroMacchina).HasColumnName("numeroMacchina");

                entity.Property(e => e.PezzeDisposte).HasColumnName("pezzeDisposte");

                entity.Property(e => e.TipoDisposizione)
                    .HasColumnName("tipoDisposizione")
                    .HasMaxLength(400);
            });

            modelBuilder.Entity<Decofast35Getvalue>(entity =>
            {
                entity.ToTable("decofast_3_5_getvalue");

                entity.HasIndex(e => e.NumDisposizioneId)
                    .HasName("decofast_3_5_getvalue_datiEsterni_id_79c3d7f6");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AlimentAriaPressione).HasColumnName("alimentAriaPressione");

                entity.Property(e => e.AspirazioneBocchetteDepress).HasColumnName("aspirazioneBocchetteDepress");

                entity.Property(e => e.CicloRaffreddMin).HasColumnName("cicloRaffredd_min");

                entity.Property(e => e.CicloRaffreddSec).HasColumnName("cicloRaffredd_sec");

                entity.Property(e => e.ContamentriLunghezzAttualePrecedente).HasColumnName("contamentriLunghezzAttualePrecedente");

                entity.Property(e => e.ContametriLunghezAttuale).HasColumnName("contametriLunghezAttuale");

                entity.Property(e => e.CoppiaCilindroDecatit).HasColumnName("coppiaCilindroDecatit");

                entity.Property(e => e.CoppiaCilindroGommato01).HasColumnName("coppiaCilindroGommato01");

                entity.Property(e => e.CoppiaCilindroGommato02).HasColumnName("coppiaCilindroGommato02");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.GuarnizVascaPressione).HasColumnName("guarnizVascaPressione");

                entity.Property(e => e.NominaleTensioneElettrFeltro).HasColumnName("nominaleTensioneElettrFeltro");

                entity.Property(e => e.NumDisposizioneId).HasColumnName("numDisposizione_id");

                entity.Property(e => e.PressGuarnizioneVasca).HasColumnName("pressGuarnizioneVasca");

                entity.Property(e => e.RiscaldamentoCilindroDecatitoreBar).HasColumnName("riscaldamentoCilindroDecatitore_bar");

                entity.Property(e => e.RiscaldamentoCilindroDecatitoreGradi).HasColumnName("riscaldamentoCilindroDecatitore_gradi");

                entity.Property(e => e.RiscaldamentoCilindroDecatitoreMin).HasColumnName("riscaldamentoCilindroDecatitore_min");

                entity.Property(e => e.RiscaldamentoCilindroDecatitorePercent).HasColumnName("riscaldamentoCilindroDecatitore_percent");

                entity.Property(e => e.RiscaldamentoCilindroDecatitoreSec).HasColumnName("riscaldamentoCilindroDecatitore_sec");

                entity.Property(e => e.RiscaldamentoVascaMin).HasColumnName("riscaldamentoVasca_min");

                entity.Property(e => e.RiscaldamentoVascaSec).HasColumnName("riscaldamentoVasca_sec");

                entity.Property(e => e.TempoLavorazioneH).HasColumnName("tempoLavorazione_h");

                entity.Property(e => e.TempoLavorazioneMin).HasColumnName("tempoLavorazione_min");

                entity.Property(e => e.TempoLavorazioneTotH).HasColumnName("tempoLavorazioneTot_h");

                entity.Property(e => e.TempoLavorazioneTotMin).HasColumnName("tempoLavorazioneTot_min");

                entity.Property(e => e.TempoNonOperativoH).HasColumnName("tempoNonOperativo_h");

                entity.Property(e => e.TempoNonOperativoMin).HasColumnName("tempoNonOperativo_min");

                entity.Property(e => e.TempoNonOperativoTotH).HasColumnName("tempoNonOperativoTot_h");

                entity.Property(e => e.TempoNonOperativoTotMin).HasColumnName("tempoNonOperativoTot_min");

                entity.Property(e => e.TempoOperativoH).HasColumnName("tempoOperativo_h");

                entity.Property(e => e.TempoOperativoMin).HasColumnName("tempoOperativo_min");

                entity.Property(e => e.TempoOperativoTotH).HasColumnName("tempoOperativoTot_h");

                entity.Property(e => e.TempoOperativoTotMin).HasColumnName("tempoOperativoTot_min");

                entity.Property(e => e.TiroCentratore).HasColumnName("tiroCentratore");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.VaporeInVascaBar).HasColumnName("vaporeInVasca_bar");

                entity.Property(e => e.VaporeInVascaGradi).HasColumnName("vaporeInVasca_gradi");

                entity.Property(e => e.VaporeInVascaPercent).HasColumnName("vaporeInVasca_percent");

                entity.Property(e => e.VelCilindroGommato01).HasColumnName("velCilindroGommato01");

                entity.Property(e => e.VelCilindroGommato02).HasColumnName("velCilindroGommato02");

                entity.Property(e => e.VelMacchina).HasColumnName("velMacchina");

                entity.Property(e => e.VelocitaCilindroDecatit).HasColumnName("velocitaCilindroDecatit");

                entity.HasOne(d => d.NumDisposizione)
                    .WithMany(p => p.Decofast35Getvalue)
                    .HasForeignKey(d => d.NumDisposizioneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("decofast_3_5_getvalue_numDisposizione_id_701b51e9_fk_decofast_3_5_datiesterni_id");
            });

            modelBuilder.Entity<Decofast35Lavorazionemacchina>(entity =>
            {
                entity.ToTable("decofast_3_5_lavorazionemacchina");

                entity.HasIndex(e => e.LavorazioneMacchinaId)
                    .HasName("decofast_3_5_lavorazionemacchina_lavorazioneMacchina_id_e47b734e");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.LavorazioneMacchinaId).HasColumnName("lavorazioneMacchina_id");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.HasOne(d => d.LavorazioneMacchina)
                    .WithMany(p => p.Decofast35Lavorazionemacchina)
                    .HasForeignKey(d => d.LavorazioneMacchinaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("decofast_3_5_lavorazionemacchina_lavorazioneMacchina_id_e47b734e_fk_decofast_3_5_getvalue_id");
            });

            modelBuilder.Entity<Decofast35Ricetta>(entity =>
            {
                entity.ToTable("decofast_3_5_ricetta");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.NomeRicetta)
                    .IsRequired()
                    .HasColumnName("nomeRicetta")
                    .HasMaxLength(255);

                entity.Property(e => e.SetAspirazioneBocchette).HasColumnName("set_aspirazioneBocchette");

                entity.Property(e => e.SetContametri).HasColumnName("set_contametri");

                entity.Property(e => e.SetCorrezioneNastroRaffred).HasColumnName("set_correzioneNastroRaffred");

                entity.Property(e => e.SetRiscaldCilindrDecatitore).HasColumnName("set_riscaldCilindrDecatitore");

                entity.Property(e => e.SetRulloLucidante).HasColumnName("set_rulloLucidante");

                entity.Property(e => e.SetTensElettronicaFeltro).HasColumnName("set_tensElettronicaFeltro");

                entity.Property(e => e.SetTensPneumaticaFeltro).HasColumnName("set_tensPneumaticaFeltro");

                entity.Property(e => e.SetTiroBallerCilindrAlimen).HasColumnName("set_tiroBallerCilindrAlimen");

                entity.Property(e => e.SetVaporeInVasca).HasColumnName("set_vaporeInVasca");

                entity.Property(e => e.SetVelMacchina).HasColumnName("set_velMacchina");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

        }
    }
}
