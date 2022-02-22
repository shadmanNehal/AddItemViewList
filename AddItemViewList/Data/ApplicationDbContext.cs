using AddItemViewList.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AddItemViewList.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
            public DbSet<AddList> AddLists { get; set; }


        protected override void OnModelCreating(ModelBuilder foModelBuilder)
        {
            foModelBuilder.Entity<AddList>().HasNoKey();

        }

        public List<AddList> GetList()
        {
            return this.Set<AddList>().FromSqlInterpolated($"EXEC GetListSP").ToList();
        }

        public void execAddItemSP(string fItemName, string fItemType, string fItemPrice, out int itemId)
        {
            SqlParameter parmOUT = new SqlParameter("@ItemId", SqlDbType.Int);
            parmOUT.Direction = ParameterDirection.Output;

            this.Database.ExecuteSqlInterpolated($"EXEC AddItemSP @ItemName={fItemName},@ItemType={fItemType},@ItemPrice={fItemPrice},@ItemId={parmOUT} output");
            itemId = Convert.ToInt32(parmOUT.Value);

        }

        public void editItemSP(int itemId, string fItemName, string fItemType, string fItemPrice)
        {
            this.Database.ExecuteSqlInterpolated($" EXEC EditItemSP @ItemId={itemId},@ItemName={fItemName},@ItemType={fItemType},@ItemPrice={fItemPrice}");
        }

        public List<AddList> getPagin(int fPageNo, int fPageRecord,out int fCount)
        {
            SqlParameter cnt = new SqlParameter("@inCount", SqlDbType.Int);
            cnt.Direction = ParameterDirection.Output;
            
            List<AddList> loresult =  this.Set<AddList>().FromSqlInterpolated($"EXEC GetListPaged @PageNo={fPageNo}, @RecordsPerPage={fPageRecord}, @inCount={cnt} output").ToList();
            fCount = Convert.ToInt32(cnt.Value);
            return loresult;


        }
    }
}

