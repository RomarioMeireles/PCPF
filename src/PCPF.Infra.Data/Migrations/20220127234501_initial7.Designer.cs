﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PCPF.Infra.Data.Repository;

namespace PCPF.Infra.Data.Migrations
{
    [DbContext(typeof(PCPFContext))]
    [Migration("20220127234501_initial7")]
    partial class initial7
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PCPF.Domain.Model.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataRegisto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("DocumentoIdentificacao")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Fornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataRegisto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("DenominacaoFiscal")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Endereco")
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("UtilizadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UtilizadorId");

                    b.ToTable("Fornecedor");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Pagamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataRegisto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Observacao")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("bit");

                    b.Property<decimal>("ValotTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("Pagamento");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataRegisto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("FinalizadoEm")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("IniciadoEm")
                        .HasColumnType("datetime");

                    b.Property<string>("Observacao")
                        .HasColumnType("varchar(200)");

                    b.Property<long>("Referencia")
                        .HasColumnType("bigint");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("StatusPedido")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("PCPF.Domain.Model.PedidoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataRegisto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<decimal>("Desconto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("PedidoItem");
                });

            modelBuilder.Entity("PCPF.Domain.Model.PedidoRascunho", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("SessionId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(150)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("PedidoRascunho");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodigoBarras")
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime>("DataRegisto")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Imagem")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Observacao")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("QuantidadeMinima")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("UtilizadorId")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("UtilizadorId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("PCPF.Domain.Model.ProdutoFornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataRegisto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("FornecedorId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("UtilizadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("UtilizadorId");

                    b.ToTable("ProdutoFornecedor");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataRegisto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DataValidade")
                        .HasColumnType("date");

                    b.Property<string>("NumeroLote")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("UtilizadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("UtilizadorId");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Utilizador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataRegisto")
                        .HasColumnType("datetime");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(150)");

                    b.Property<byte>("Perfil")
                        .HasMaxLength(1)
                        .HasColumnType("tinyint");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Utilizador");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Fornecedor", b =>
                {
                    b.HasOne("PCPF.Domain.Model.Utilizador", "Utilizador")
                        .WithMany("Fornercedores")
                        .HasForeignKey("UtilizadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Utilizador");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Pagamento", b =>
                {
                    b.HasOne("PCPF.Domain.Model.Pedido", "Pedido")
                        .WithMany("Pagamentos")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Pedido", b =>
                {
                    b.HasOne("PCPF.Domain.Model.Cliente", "Cliente")
                        .WithMany("ItensPedido")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("PCPF.Domain.Model.PedidoItem", b =>
                {
                    b.HasOne("PCPF.Domain.Model.Pedido", "Pedido")
                        .WithMany("ItensPedido")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PCPF.Domain.Model.Produto", "Produto")
                        .WithMany("ItensPedido")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Produto", b =>
                {
                    b.HasOne("PCPF.Domain.Model.Utilizador", "Utilizador")
                        .WithMany("Produtos")
                        .HasForeignKey("UtilizadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Utilizador");
                });

            modelBuilder.Entity("PCPF.Domain.Model.ProdutoFornecedor", b =>
                {
                    b.HasOne("PCPF.Domain.Model.Fornecedor", "Fornecedor")
                        .WithMany("produtos")
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PCPF.Domain.Model.Produto", "Produto")
                        .WithMany("produtos")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PCPF.Domain.Model.Utilizador", "Utilizador")
                        .WithMany("produtoFornecedors")
                        .HasForeignKey("UtilizadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Fornecedor");

                    b.Navigation("Produto");

                    b.Navigation("Utilizador");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Stock", b =>
                {
                    b.HasOne("PCPF.Domain.Model.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PCPF.Domain.Model.Utilizador", "Utilizador")
                        .WithMany("Stocks")
                        .HasForeignKey("UtilizadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Produto");

                    b.Navigation("Utilizador");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Cliente", b =>
                {
                    b.Navigation("ItensPedido");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Fornecedor", b =>
                {
                    b.Navigation("produtos");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Pedido", b =>
                {
                    b.Navigation("ItensPedido");

                    b.Navigation("Pagamentos");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Produto", b =>
                {
                    b.Navigation("ItensPedido");

                    b.Navigation("produtos");
                });

            modelBuilder.Entity("PCPF.Domain.Model.Utilizador", b =>
                {
                    b.Navigation("Fornercedores");

                    b.Navigation("produtoFornecedors");

                    b.Navigation("Produtos");

                    b.Navigation("Stocks");
                });
#pragma warning restore 612, 618
        }
    }
}
