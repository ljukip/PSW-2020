using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSW_bolnica.dao;
using PSW_bolnica.model;
using PSW_bolnica.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSW_bolnica.services.Tests
{
    [TestClass()]
    public class ReferralServiceTests
    {
        private DbContextOptions<DBContext> _options;

        public ReferralServiceTests()
        {
            _options = new DbContextOptionsBuilder<DBContext>().UseInMemoryDatabase(databaseName: "DBC_Referrals").Options;
        }

        private void SetupDatabase(DBContext context)
        {
            context.Database.EnsureDeleted();
            CreateReferrals(context);
        }
        private void CreateReferrals(DBContext context)
        {
            context.referral.Add(new Referral
            {
                Id = 1,
                IsDeleted = false,
                idOfPatient = 1,
                Speciality = "oncology"
            });

            context.referral.Add(new Referral
            {
                Id = 2,
                IsDeleted = false,
                idOfPatient = 2,
                SpecialistId = 2,
                Speciality = "ophtalmology"
            });

            context.SaveChanges();
        }

        [TestMethod()]
        public void AddTest()
        {
            using (var context = new DBContext(_options))
            {
                SetupDatabase(context);
                ReferralService service = new ReferralService(context);
                Referral referral = new Referral();
                service.Add(referral);

                Assert.AreEqual(context.referral.ToList().Count(), 3);
            }
        }
    }
}