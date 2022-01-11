// Copyright (c) SquadroCorp
// This file is part of Squadrosu!.
// Squadrosu! is licensed under the GPL v3. See LICENSE.md for details.

using System;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Squadrosu.Framework;

public class Piece
{
    /// <summary>
    /// To which player is the current piece
    /// </summary>
    public readonly Player Player;
    /// <summary>
    /// Direction of the piece
    /// </summary>
    public Direction Direction { get; private set; }
    /// <summary>
    /// Position in the row
    /// </summary>
    public int Position { get; private set; }
    /// <summary>
    /// Line number
    /// </summary>
    public readonly int LineNumber;
    /// <summary>
    /// The board in which the piece plays
    /// </summary>
    public readonly Board Board;

    public Piece(Player player, int lineNumber, Board board)
    {
        Direction = Direction.Forward;
        Position = 0;
        LineNumber = lineNumber;
        Player = player;
        Board = board;
    }

    /// <summary>
    /// The piece returns to the initial position of the direction
    /// </summary>
    public void PlaceOnEdge()
    {
        this.Position = (this.Direction == Direction.Forward) ? 0 : 6;
    }
    /// <summary>
    /// Place the piece on edge
    /// </summary>
    public void Reset()
    {
        Direction = Direction.Forward;
        PlaceOnEdge();
    }

    /// <summary>
    /// Returns the step of a piece in a given state
    /// </summary>
    /// <returns>How many squares the piece can go through</returns>
    public int Step()
    {
        if (LineNumber == 3)
            return 2;

        bool isBlack = Player == Player.Black;
        bool isForward = Direction == Direction.Forward;
        bool isOnEdge = LineNumber == 1 || LineNumber == 5;
        return isBlack ^ isForward ^ isOnEdge ? 3 : 1;
    }

    /// <summary>
    /// Move the position of the piece
    /// </summary>
    public void Move()
    {
        if (this.Direction == Direction.Forward)
            this.moveForward();
        else if (this.Direction == Direction.Backward)
            this.moveBackward();
    }

    /// <summary>
    /// Move the piece if its direction is forward
    /// </summary>
    /// <param name="board">The current board</param>
    private void moveForward()
    {
        int step = this.Step();
        int newPosition = Math.Min(this.Position + step, 6);
        bool isBlack = (this.Player == Player.Black);
        int i = this.Position;

        while (i + 1 <= Math.Min(this.Position + step, 5))
        {
            if (isBlack && this.Board.Positions[this.LineNumber, i + 1] != null)
            {
                while (this.Board.Positions[this.LineNumber, i + 1] != null)
                {
                    this.Board.Positions[this.LineNumber, i + 1].PlaceOnEdge();
                    newPosition = i + 2;
                    i++;
                }
                i = 7;
            }
            else if (this.Board.Positions[i + 1, this.LineNumber] != null)
            {
                while (this.Board.Positions[i + 1, this.LineNumber] != null)
                {
                    this.Board.Positions[i + 1, this.LineNumber].PlaceOnEdge();
                    newPosition = i + 2;
                    i++;
                }
                i = 7;
            }
            i++;
        }

        this.Position = newPosition;
        if (newPosition == 6)
            this.Direction = Direction.Backward;
        this.Board.Update();
    }

    /// <summary>
    /// Move the piece if its dierction is Bakcward
    /// </summary>
    /// <param name="board">The current Board</param>
    private void moveBackward()
    {
        int step = this.Step();
        int newPosition = Math.Max(this.Position - step, 0);
        bool isBlack = (this.Player == Player.Black);
        int i = this.Position;

        while (i - 1 >= Math.Max(this.Position - step, 1))
        {
            if (isBlack && this.Board.Positions[this.LineNumber, i - 1] != null)
            {
                while (this.Board.Positions[this.LineNumber, i - 1] != null)
                {
                    this.Board.Positions[this.LineNumber, i - 1].Reset();
                    newPosition = i - 2;
                    i--;
                }
                i = -1;
            }
            else if (this.Board.Positions[i - 1, this.LineNumber] != null)
            {
                while (this.Board.Positions[i - 1, this.LineNumber] != null)
                {
                    this.Board.Positions[i - 1, this.LineNumber].Reset();
                    newPosition = i - 2;
                    i--;
                }
                i = -1;
            }
            i--;
        }

        this.Position = newPosition;
        if (newPosition == 0)
            this.Direction = Direction.Finished;
        this.Board.Update();
    }


    public override string ToString() => Player == Player.Black ? "B" : "W";
}
