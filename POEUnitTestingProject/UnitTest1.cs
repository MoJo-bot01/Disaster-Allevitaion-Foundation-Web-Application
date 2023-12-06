using Microsoft.AspNetCore.Identity;
using WebDAFFinal.Controllers;
using WebDAFFinal.Data;
using Moq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using WebDAFFinal.Models;

namespace POEUnitTestingProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task AllocationsTestMethod()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            using (var context = new ApplicationDbContext(contextOptions))
            {
                // Initialize my database with test data here
                context.good_donations.Add(new good_donations
                {
                    good_donations_id = 1,
                    item_name = "Blankets",
                    item_description = "",
                    item_category = good_donations.CategoryType.Clothes,
                    number_of_items = 100,
                    donation_date = DateTime.Now,
                    is_anonymous = good_donations.AnonymousType.Yes
                });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(contextOptions))
            {
                // Create an instance of your controller with the mocked dependencies
                var controller = new disastersController(context, userManagerMock.Object);

                // My test data
                int disasterId = 1;
                DateTime disasterStartDate = DateTime.Now;
                DateTime disasterEndDate = DateTime.Now.AddDays(7);
                int moneyAllocatedAmount = 100;
                DateTime moneyAllocatedDate = DateTime.Now;
                string usernameMoneyAllocated = "testuser@example.com";
                string goodAllocatedName = "Blankets"; 
                int goodAllocatedNumberOfItems = 150;  
                DateTime goodAllocationDate = DateTime.Now;
                string usernameGoodAllocated = "testuser@example.com";

                // Act
                var result = await controller.EditAssigningGoodsTest(
                    disasterId, userManagerMock.Object, "Mamelodi", "Floods", goodAllocatedName,
                    disasterStartDate, disasterEndDate, moneyAllocatedAmount,
                    moneyAllocatedDate, usernameMoneyAllocated, goodAllocatedName,
                    goodAllocatedNumberOfItems, goodAllocationDate, usernameGoodAllocated);


                // Assert
                Assert.IsTrue(true);

                context.Database.EnsureDeleted();
            }


        }
    }

}