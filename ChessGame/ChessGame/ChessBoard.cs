namespace ChessGame
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class ChessBoard
	{
		private readonly int _size;
		private readonly List<Piece> _pieces;
		private ChessPosition _whiteKingPosition;
		private ChessPosition _blackKingPosition;

		public ChessBoard(int size = 8)
		{
			_size = size;
			_pieces = new List<Piece>();
			InitializePieces();
		}

		private void InitializePieces()
		{
			var initialPieceSetSingle = new List<(string, int, int)>
			{
				(PieceType.ROOK, 0, 0), (PieceType.KNIGHT, 1, 0), (PieceType.BISHOP, 2, 0), (PieceType.QUEEN, 3, 0),
				(PieceType.KING, 4, 0), (PieceType.BISHOP, 5, 0), (PieceType.KNIGHT, 6, 0), (PieceType.ROOK, 7, 0),
				(PieceType.PAWN, 0, 1), (PieceType.PAWN, 1, 1), (PieceType.PAWN, 2, 1), (PieceType.PAWN, 3, 1),
				(PieceType.PAWN, 4, 1), (PieceType.PAWN, 5, 1), (PieceType.PAWN, 6, 1), (PieceType.PAWN, 7, 1)
			};

			foreach (var (type, x, y) in initialPieceSetSingle)
			{
				var pieceWhite = PieceFactory.Create(type, new ChessPosition(x, y), Piece.WHITE);
				if (type == PieceType.KING)
				{
					RegisterKingPosition(pieceWhite.Position, pieceWhite.Color);
				}
				_pieces.Add(pieceWhite);

				var pieceBlack = PieceFactory.Create(type, new ChessPosition(_size - x - 1, _size - y - 1), Piece.BLACK);
				if (type == PieceType.KING)
				{
					RegisterKingPosition(pieceBlack.Position, pieceBlack.Color);
				}
				_pieces.Add(pieceBlack);
			}
		}

		public Piece GetPiece(ChessPosition position)
		{
			return _pieces.FirstOrDefault(piece => piece.Position.Equals(position));
		}

		public List<ChessPosition> BeamSearchThreat(ChessPosition startPosition, string ownColor, int incrementX, int incrementY)
		{
			var threatenedPositions = new List<ChessPosition>();
			var currX = startPosition.XCoord + incrementX;
			var currY = startPosition.YCoord + incrementY;

			while (currX >= 0 && currY >= 0 && currX < _size && currY < _size)
			{
				var currPosition = new ChessPosition(currX, currY);
				var currPiece = GetPiece(currPosition);

				if (currPiece != null)
				{
					if (currPiece.Color != ownColor)
					{
						threatenedPositions.Add(currPosition);
					}
					break;
				}

				threatenedPositions.Add(currPosition);
				currX += incrementX;
				currY += incrementY;
			}

			return threatenedPositions;
		}

		public ChessPosition SpotSearchThreat(ChessPosition startPosition, string ownColor, int incrementX, int incrementY, bool threatOnly = false, bool freeOnly = false)
		{
			var currX = startPosition.XCoord + incrementX;
			var currY = startPosition.YCoord + incrementY;

			if (currX >= _size || currY >= _size || currX < 0 || currY < 0)
			{
				return null;
			}

			var currPosition = new ChessPosition(currX, currY);
			var currPiece = GetPiece(currPosition);

			if (currPiece != null)
			{
				if (freeOnly) return null;
				return currPiece.Color != ownColor ? currPosition : null;
			}

			return threatOnly ? null : currPosition;
		}

		public List<Piece> Pieces => new List<Piece>(_pieces);

		public int Size => _size;

		public ChessPosition WhiteKingPosition => _whiteKingPosition;

		public ChessPosition BlackKingPosition => _blackKingPosition;

		public void ExecuteMove(MoveCommand command)
		{
			var sourcePiece = GetPiece(command.Src);

			for (int i = 0; i < _pieces.Count; i++)
			{
				if (_pieces[i].Position.Equals(command.Dst))
				{
					_pieces.RemoveAt(i);
					break;
				}
			}

			sourcePiece.Move(command.Dst);
		}

		public void RegisterKingPosition(ChessPosition position, string color)
		{
			if (color == Piece.WHITE)
			{
				_whiteKingPosition = position;
			}
			else if (color == Piece.BLACK)
			{
				_blackKingPosition = position;
			}
			else
			{
				throw new InvalidOperationException("Unknown color of the king piece");
			}
		}
	}

}
