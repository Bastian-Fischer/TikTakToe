using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TTTLogic;

namespace TikTakToeTest
{
    [TestClass]
    public class LogicTest
    {
        /* Checkliste f?r tests (unvollst?ndig)
         * Parameter einer Funktion
         *      - Null
         *      - Negative werte
         *      - Leere Container
         *      - Zu grosse werte
         *      - korrekte exceptions wenn fehlerhafte werte benutzt werden
         * 
         * R?ckgabe einer Funktion / out-Parameter
         *      - Null
         *      - Leere Container
         *      - Erwarteter Wert
         */

        /* Nach Spielstart ein leeres Spielfeld
         * Nach einem Zug auf eine g?ltige koordinate muss das gew?hlte feld bef?llt sein, der rest muss weiterhin leer sein
         * Ein Zug auf ein belegtes feld muss "Ung?ltig" liefern
         * Ein Zug auf eine ung?ltige Koordinate (ausserhalb) muss "Ung?ltig" liefern
         * Zwei aufeinanderfolgende Z?ge auf unterschiedliche g?ltige koordinaten m?ssen unterschiedliche steine beinhalten
         * Wenn der letzte stein auf das feld gesetzt wird und keinen Sieg bedeutet muss "Draw" geliefert werden
         * Wenn der letzte stein auf dem feld zu einer siegbedingung f?hrt muss "sieg" zur?ckgegeben werden
         * Wenn 3 gleiche steine in einer reihe sind soll "Sieg" zur?ckgegeben werden 
         * Wenn 3 gleiche steine in einer spalte sind soll "Sieg" zur?ckgegeben werden 
         * Wenn 3 gleiche steine in einer diagonalen sind soll "Sieg" zur?ckgegeben werden 
         * Aufeinanderfolgende fehlerhafte z?ge f?hren nicht zum spielerwechsel
         * 
         * Ein Spieler darf nicht mehr als 10 mal in folge als erster am zug sein / Spieler sind immer abwechslnd dran/ verlierer beginnt
         * Nach einem Sieg sind normalerweise g?ltige z?ge auch "Ung?ltig"
         * Nach einem Sieg bleibt der aktuelle spieler auf dem Siegspieler stehen
         */


        //Forgegeben Tests

        [TestClass]
        public class LogicTests
        {
            [TestMethod]
            public void Createable()
            {
                Logic l = new();
                Assert.IsNotNull(l);
            }

            [TestMethod]
            public void EmptyFieldAfterCreation()
            {
                Logic l = new();
                Board[,] returnedBoard = l.GetGameBoard();

                foreach (Board item in returnedBoard)
                {
                    Assert.IsTrue(item == Board.Empty);
                }
            }

            [TestMethod]
            public void FirstMove_Valid()
            {
                // vorbereitung
                Logic l = new(); // logik starten

                // ausf?hrung
                TurnResult result = l.PlayerTurn(0, 0); // erster zug auf g?ltige koordinaten, muss valid ergeben

                // ?berpr?fen des ergebnisses
                Assert.IsTrue(result == TurnResult.Valid); // testen ob auch valid zur?ck kam
                Assert.IsTrue(l.GetGameBoard()[0, 0] != Board.Empty);// testen ob der Zug auch auf dem Spielfeld vermerkt ist
            }

            [TestMethod]
            public void FirstMove_ValidOnEveryPosition()
            {
                // vorbereitung
                List<Logic> logicList = new(9); // logik starten
                for (int counter = 0; counter < 9; counter++)
                    logicList.Add(new Logic());

                // ausf?hrung
                for (int counter = 0; counter < logicList.Count; counter++)
                {
                    int x = counter % 3;
                    int y = counter / 3;

                    Assert.IsTrue(logicList[counter].PlayerTurn(x, y) == TurnResult.Valid); // erster zug auf g?ltige koordinaten, muss valid ergeben
                    Assert.IsTrue(logicList[counter].GetGameBoard()[y, x] != Board.Empty);// testen ob der Zug auch auf dem Spielfeld vermerkt ist
                }
            }

            [TestMethod]
            public void FirstMove_OutOfRange()
            {
                Logic l = new();

                TurnResult result = l.PlayerTurn(3, 3);
                var returnedBoard = l.GetGameBoard();

                Assert.IsTrue(result == TurnResult.Invalid);
                foreach (Board item in returnedBoard)
                {
                    Assert.IsTrue(item == Board.Empty);
                }
            }

            [TestMethod]
            public void FirstMove_NegativeCoordinates()
            {
                Logic l = new();

                TurnResult result = l.PlayerTurn(-1, -1);
                var returnedBoard = l.GetGameBoard();

                Assert.IsTrue(result == TurnResult.Invalid);
                foreach (Board item in returnedBoard)
                {
                    Assert.IsTrue(item == Board.Empty);
                }
            }

            [TestMethod]
            public void PlayerSwitch_ValidMove()
            {
                Logic l = new();

                // playerturn nimmt erst X dann Y//
                Assert.IsTrue(l.PlayerTurn(1, 1) == TurnResult.Valid);
                Assert.IsTrue(l.PlayerTurn(0, 0) == TurnResult.Valid);
                Assert.IsTrue(l.PlayerTurn(0, 1) == TurnResult.Valid);

                var returnedBoard = l.GetGameBoard();

                // Arrayzugriff ist erst Y dann X 
                Assert.IsTrue(returnedBoard[1, 1] != Board.Empty);
                Assert.IsTrue(returnedBoard[0, 0] != Board.Empty);
                Assert.IsTrue(returnedBoard[1, 0] != Board.Empty);

                Assert.IsTrue(returnedBoard[1, 1] == returnedBoard[1, 0]);
            }

            [TestMethod]
            public void PlayerSwitch_InvalidMove()
            {
                Logic l = new();

                Assert.IsTrue(l.PlayerTurn(1, 1) == TurnResult.Valid);//PlayerA belegt mitte
                Assert.IsTrue(l.PlayerTurn(1, 1) == TurnResult.Invalid);// PlayerB belegt fehlerhaft mitte und bleibt am zug
                Assert.IsTrue(l.PlayerTurn(3, -1) == TurnResult.Invalid);// PlayerB gibt falsche koordinaten an
                Assert.IsTrue(l.PlayerTurn(0, 0) == TurnResult.Valid);//PlayerB belegt oben links

                var returnedBoard = l.GetGameBoard();
                Assert.IsTrue(returnedBoard[1, 1] != Board.Empty);
                Assert.IsTrue(returnedBoard[0, 0] != Board.Empty);

                Assert.IsTrue(returnedBoard[0, 0] != returnedBoard[1, 1]);
            }

            [TestMethod]
            public void EndGame_Win()
            {
                Logic l = new();

                Assert.IsTrue(l.PlayerTurn(0, 1) == TurnResult.Valid);//PlayerA
                Assert.IsTrue(l.PlayerTurn(1, 1) == TurnResult.Valid);//PlayerB

                Assert.IsTrue(l.PlayerTurn(0, 2) == TurnResult.Valid);//PlayerA
                Assert.IsTrue(l.PlayerTurn(1, 2) == TurnResult.Valid);//PlayerB

                var turnResult = l.PlayerTurn(0, 0);
                Assert.IsTrue(turnResult == TurnResult.WinO || turnResult == TurnResult.WinX);//PlayerA gewinnt
            }

            [TestMethod]
            public void EndGame_Draw()
            {
                Logic l = new();
                //X O X
                //O X O 
                //O X O

                Assert.IsTrue(l.PlayerTurn(0, 1) == TurnResult.Valid); // O
                Assert.IsTrue(l.PlayerTurn(0, 0) == TurnResult.Valid); // X
                Assert.IsTrue(l.PlayerTurn(1, 0) == TurnResult.Valid); // O
                Assert.IsTrue(l.PlayerTurn(0, 2) == TurnResult.Valid); // X
                Assert.IsTrue(l.PlayerTurn(2, 0) == TurnResult.Valid); // O 
                Assert.IsTrue(l.PlayerTurn(1, 1) == TurnResult.Valid); // X
                Assert.IsTrue(l.PlayerTurn(1, 2) == TurnResult.Valid); // O
                Assert.IsTrue(l.PlayerTurn(2, 1) == TurnResult.Valid); // X
                Assert.IsTrue(l.PlayerTurn(2, 2) == TurnResult.Draw);  // O
            }

            [TestMethod]
            public void EndGame_WinOnLastStone()
            {
                Logic l = new();
                //X O X
                //O X O 
                //O X X

                Assert.IsTrue(l.PlayerTurn(0, 0) == TurnResult.Valid); // X
                Assert.IsTrue(l.PlayerTurn(0, 1) == TurnResult.Valid); // O
                Assert.IsTrue(l.PlayerTurn(0, 2) == TurnResult.Valid); // X
                Assert.IsTrue(l.PlayerTurn(1, 0) == TurnResult.Valid); // O
                Assert.IsTrue(l.PlayerTurn(1, 1) == TurnResult.Valid); // X
                Assert.IsTrue(l.PlayerTurn(2, 0) == TurnResult.Valid); // O 
                Assert.IsTrue(l.PlayerTurn(2, 1) == TurnResult.Valid); // X
                Assert.IsTrue(l.PlayerTurn(1, 2) == TurnResult.Valid); // O
                Assert.IsTrue(l.PlayerTurn(2, 2) != TurnResult.Draw);  // X
            }

            [TestMethod]
            public void EndGame_PlayablaAfterDrawAndReset()
            {
                Logic l = new();
                //X O X
                //O X O 
                //O X O

                Assert.IsTrue(l.PlayerTurn(0, 1) == TurnResult.Valid); // O
                Assert.IsTrue(l.PlayerTurn(0, 0) == TurnResult.Valid); // X
                Assert.IsTrue(l.PlayerTurn(1, 0) == TurnResult.Valid); // O
                Assert.IsTrue(l.PlayerTurn(0, 2) == TurnResult.Valid); // X
                Assert.IsTrue(l.PlayerTurn(2, 0) == TurnResult.Valid); // O 
                Assert.IsTrue(l.PlayerTurn(1, 1) == TurnResult.Valid); // X
                Assert.IsTrue(l.PlayerTurn(1, 2) == TurnResult.Valid); // O
                Assert.IsTrue(l.PlayerTurn(2, 1) == TurnResult.Valid); // X
                Assert.IsTrue(l.PlayerTurn(2, 2) == TurnResult.Draw); // O

                l.Reset();
                Assert.IsTrue(l.PlayerTurn(0, 1) == TurnResult.Valid); // O

            }

            [TestMethod]
            public void EndGame_NoMovesAfterWin()
            {
                Logic l = new();

                Assert.IsTrue(l.PlayerTurn(0, 1) == TurnResult.Valid);//PlayerA
                Assert.IsTrue(l.PlayerTurn(1, 1) == TurnResult.Valid);//PlayerB

                Assert.IsTrue(l.PlayerTurn(0, 2) == TurnResult.Valid);//PlayerA
                Assert.IsTrue(l.PlayerTurn(1, 2) == TurnResult.Valid);//PlayerB

                var turnResult = l.PlayerTurn(0, 0);
                Assert.IsTrue(turnResult == TurnResult.WinO || turnResult == TurnResult.WinX);//PlayerA gewinnt

                Assert.IsTrue(l.PlayerTurn(2, 2) == TurnResult.Invalid);//Ung?ltig da das Spiel schon vorbei ist
            }

            [TestMethod]
            public void Player_GetCurrentPlayer()
            {
                Logic l = new();

                bool result = l.GetCurrentPlayer();
                l.PlayerTurn(0, 0);
                Assert.IsTrue(result != l.GetCurrentPlayer());
            }
            [TestMethod]
            public void Player_GetCurrentPlayer_InvalidMove()
            {
                Logic l = new();

                bool result = l.GetCurrentPlayer();
                l.PlayerTurn(0, 0); // X
                l.PlayerTurn(0, 1); // O
                l.PlayerTurn(0, 1); // X ung?ltig, x bleibt am zug
                Assert.IsTrue(result == l.GetCurrentPlayer());
            }

            [TestMethod]
            public void Reset_SwitchesPlayer()
            {
                Logic l = new();
                bool firstPlayer = l.GetCurrentPlayer();

                l.Reset();
                Assert.IsTrue(firstPlayer != l.GetCurrentPlayer());
                l.Reset();
                Assert.IsTrue(firstPlayer == l.GetCurrentPlayer());
            }

            [TestMethod]
            public void Reset_ClearsTheBoard()
            {
                Logic l = new();
                l.PlayerTurn(1, 1);
                Assert.IsTrue(l.GetGameBoard()[1, 1] != Board.Empty);
                l.Reset();
                Assert.IsTrue(l.GetGameBoard()[1, 1] == Board.Empty);

            }

            [TestMethod]
            public void Player_DifferentBeginners()
            {
                List<Logic> logicList = new();

                logicList.Add(new Logic());
                logicList.Add(new Logic());
                logicList.Add(new Logic());
                logicList.Add(new Logic());
                logicList.Add(new Logic());
                logicList.Add(new Logic());
                logicList.Add(new Logic());
                logicList.Add(new Logic());

                bool firstStarter = logicList[0].GetCurrentPlayer();
                bool different = false;

                for (int counter = 1; counter < logicList.Count; counter++)
                {
                    if (logicList[counter].GetCurrentPlayer() != firstStarter)
                    {
                        different = true;
                        break;
                    }
                }
                Assert.IsTrue(different);
            }
            //--------------------------------------------------------------------------------------------------
            // more than 3 x y

            [TestMethod]
            public void CreateRandomSizedBoard() {
                Random rand = new Random();
                int SizeX =rand.Next(3, 11);
                int SizeY =rand.Next(3, 11);
                Logic l = new(SizeX, SizeY);
                Assert.IsTrue(l.GetGameBoard().GetLength(0) == SizeX);
                Assert.IsTrue(l.GetGameBoard().GetLength(1) == SizeY);
            }
            [TestMethod]
            public void BoardXYNotUnder3(){
                Logic l = new(2, 2);
                Assert.IsTrue(l.GetGameBoard().GetLength(0) > 2);
                Assert.IsTrue(l.GetGameBoard().GetLength(1) > 2);
            }
            [TestMethod]
            public void NeedetToWinNotUnder3() {
                Logic l = new(5, 5, 1);
                Assert.IsTrue(l.NeedToWin > 2);
            }
            [TestMethod]
            public void NeedToWinNotBiggerThanSmallestAxis() {
                Logic l = new(5, 10, 6);
                Assert.IsTrue(l.NeedToWin <= 5);
            }
            [TestMethod]
            public void BigerSizedBoardIsWinable()
            {
                Logic l = new(10, 10, 4);

                for (int i = 0; i < 3; i++)
                {
                    l.PlayerTurn(i, 1);
                    l.PlayerTurn(i, 0);
                }
                var turnResult = l.PlayerTurn(3, 1);
                Assert.IsTrue(turnResult == TurnResult.WinO || turnResult == TurnResult.WinX);
            }
        }
    }
}
