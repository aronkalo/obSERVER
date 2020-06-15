// <copyright file="ClientMock.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.GameLogic.Test
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using obServer.GameLogic;
    using obServer.GameModel.Interfaces;
    using obServer.GameModel.Item;
    using obServer.Repository.GameModel;

    /// <summary>
    /// Mock class.
    /// </summary>
    [TestFixture]
    public class ClientMock
    {
        private Mock<IRepoOBServerModel> clientModel;
        private Guid[] identifiers = new Guid[20];
        private ClientLogic iClientLogic;

        /// <summary>
        /// Initializing test database.
        /// </summary>
        [SetUp]
        public void Init()
        {
            for (int i = 0; i < this.identifiers.Length; i++)
            {
                this.identifiers[i] = Guid.NewGuid();
            }

            this.clientModel = new Mock<IRepoOBServerModel>();
            this.clientModel.Setup(x => x.AllItems).Returns(new List<IBaseItem>()
            {
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 10, 10, }, 0, true, 100, "TesztUser1"),
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 20, 20, }, 0, true, 100, "TesztUser2"),
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 30, 30, }, 0, true, 100, "TesztUser3"),
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 40, 40, }, 0, true, 100, "TesztUser4"),
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 50, 50, }, 0, true, 100, "TesztUser5"),
            });

            this.clientModel.Setup(x => x.Players).Returns(new List<IBaseItem>()
            {
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 10, 10, }, 0, true, 100, "TesztUser1"),
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 20, 20, }, 0, true, 100, "TesztUser2"),
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 30, 30, }, 0, true, 100, "TesztUser3"),
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 40, 40, }, 0, true, 100, "TesztUser4"),
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 50, 50, }, 0, true, 100, "TesztUser5"),
            });

            this.clientModel.Setup(x => x.Statics).Returns(new List<IBaseItem>());

            this.clientModel.Setup(x => x.Weapons).Returns(new List<IBaseItem>());

            this.clientModel.Setup(x => x.Crates).Returns(new List<IBaseItem>());

            this.clientModel.Setup(x => x.MyPlayer).Returns(new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 10, 10, }, 0, true, 100, "TesztUser1"));

            this.clientModel.Setup(x => x.Colliders).Returns(new List<IBaseItem>()
            {
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 10, 10, }, 0, true, 100, "TesztUser1"),
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 20, 20, }, 0, true, 100, "TesztUser2"),
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 30, 30, }, 0, true, 100, "TesztUser3"),
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 40, 40, }, 0, true, 100, "TesztUser4"),
                new Player(Player.PlayerGeometry, this.identifiers[0], new double[] { 50, 50, }, 0, true, 100, "TesztUser5"),
            });

            this.iClientLogic = new ClientLogic(this.clientModel.Object);
        }

        /// <summary>
        /// Adding player test.
        /// </summary>
        [Test]
        public void Test_AddMyPlayerNotThrowsException()
        {
            Assert.DoesNotThrow(() => this.iClientLogic.AppendPlayer());
        }

        /// <summary>
        /// Bullet fly test.
        /// </summary>
        [Test]
        public void Test_FlyBulletsNotThrowsException()
        {
            Assert.DoesNotThrow(() => this.iClientLogic.FlyBullets(1));
        }

        /// <summary>
        /// Client is ready test.
        /// </summary>
        [Test]
        public void Test_ReadyClientNotThrowsException()
        {
            Assert.DoesNotThrow(() => this.iClientLogic.Ready("TEST_USER"));
        }

        /// <summary>
        /// Visual remover test.
        /// </summary>
        [Test]
        public void Test_RemoveVisualNotThrowsException()
        {
            Assert.DoesNotThrow(() => this.iClientLogic.RemoveVisuals());
        }

        /// <summary>
        /// Checkstate method test.
        /// </summary>
        [Test]
        public void Test_CheckStateNotThrowsException()
        {
            Assert.DoesNotThrow(() => this.iClientLogic.CheckState());
        }

        /// <summary>
        /// Client check test.
        /// </summary>
        [Test]
        public void Test_CheckClientsCount()
        {
            Assert.That(this.iClientLogic.Clients.Count == 0);
        }
    }
}
