using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiGame.Common.BusinessModels;
using ReversiGame.Common.Definitions;
using ReversiGame.Common.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.UnitTests.RulesTests
{
    [TestClass]
    public class ReversiRulesTests
    {
        [TestMethod]
        public void InitialPositionTest()
        {
            var model = new GameModel();
            ReversiRules.Initialize(model);

            Assert.AreEqual(2, ReversiRules.CountCells(model, Constants.BlackValue));
            Assert.AreEqual(2, ReversiRules.CountCells(model, Constants.WhiteValue));
            Assert.AreEqual(Constants.BlackValue, model.CurrentPlayer);
            Assert.IsFalse(ReversiRules.IsGameOver(model));
        }

        [TestMethod]
        public void InitialValidMovesTest()
        {
            var model = new GameModel();
            ReversiRules.Initialize(model);

            var moves = ReversiRules.GetValidMoves(model, Constants.BlackValue)
                .Select(m => m.Row * 10 + m.Column)
                .OrderBy(m => m)
                .ToArray();

            CollectionAssert.AreEqual(new[] { 23, 32, 45, 54 }, moves);
        }

        [TestMethod]
        public void PlacePieceFlipsAndPassesTurnTest()
        {
            var model = new GameModel();
            ReversiRules.Initialize(model);

            ReversiRules.PlacePiece(model, 2, 3);

            Assert.AreEqual(Constants.BlackValue, model[2, 3]);
            Assert.AreEqual(Constants.BlackValue, model[3, 3]);
            Assert.AreEqual(4, ReversiRules.CountCells(model, Constants.BlackValue));
            Assert.AreEqual(1, ReversiRules.CountCells(model, Constants.WhiteValue));
            Assert.AreEqual(Constants.WhiteValue, model.CurrentPlayer);
            Assert.AreEqual(1, model.MoveCount);
        }

        [TestMethod]
        public void OccupiedCellIsNotValidMoveTest()
        {
            var model = new GameModel();
            ReversiRules.Initialize(model);

            Assert.IsFalse(ReversiRules.IsValidMove(model, 3, 3, Constants.BlackValue));
            Assert.IsFalse(ReversiRules.IsValidMove(model, 0, 0, Constants.BlackValue));
        }

        [TestMethod]
        public void PassTurnWhenOpponentHasNoMovesTest()
        {
            // Белым после хода черных ходить некуда: ход остается у черных
            var model = new GameModel();
            model[0, 0] = Constants.BlackValue;
            model[0, 1] = Constants.WhiteValue;
            model[1, 1] = Constants.WhiteValue;
            model.CurrentPlayer = Constants.BlackValue;

            ReversiRules.PlacePiece(model, 0, 2);

            Assert.AreEqual(Constants.BlackValue, model[0, 1]);
            Assert.AreEqual(Constants.BlackValue, model.CurrentPlayer);
            Assert.IsNotNull(model.Message);
        }

        [TestMethod]
        public void GameOverAndResultTest()
        {
            var model = new GameModel();
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = row < 5 ? Constants.BlackValue : Constants.WhiteValue;
                }
            }

            Assert.IsTrue(ReversiRules.IsGameOver(model));
            Assert.AreEqual(GameResult.BlackWins, ReversiRules.GetResult(model));
        }
    }
}
