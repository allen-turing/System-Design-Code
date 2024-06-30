namespace ChessGame
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class ChessGame
	{
		private readonly ChessBoard _board;
		private string _currentTurnColor;
		private bool _gameOver;

		public ChessGame()
		{
			_board = new ChessBoard();
			_currentTurnColor = Piece.WHITE;
			_gameOver = false;
		}

		private bool IsKingThreatened(ChessBoard board, string kingColor)
		{
			var kingPosition = kingColor == Piece.WHITE ? board.WhiteKingPosition : board.BlackKingPosition;
			return board.Pieces.Where(piece => piece.Color != kingColor)
							   .SelectMany(piece => piece.GetThreatenedPositions(board))
							   .Contains(kingPosition);
		}

		private List<Piece> GetValidPieces(string color)
		{
			return _board.Pieces.Where(piece => piece.Color == color &&
												piece.GetMoveablePositions(_board)
													 .Any(pos => !DoesMoveExposeKing(piece, pos)))
								.ToList();
		}

		private bool DoesMoveExposeKing(Piece piece, ChessPosition targetPosition)
		{
			var hypotheticalBoard = CloneBoardWithMove(piece, targetPosition);
			return IsKingThreatened(hypotheticalBoard, piece.Color);
		}

		private ChessBoard CloneBoardWithMove(Piece piece, ChessPosition targetPosition)
		{
			var hypotheticalBoard = new ChessBoard();
			foreach (var originalPiece in _board.Pieces)
			{
				var clonedPiece = PieceFactory.Create(originalPiece.GetType().Name.ToLower(), originalPiece.Position, originalPiece.Color);
				if (originalPiece is King king)
				{
					((King)clonedPiece).SetBoardHandle(hypotheticalBoard);
				}
				hypotheticalBoard.Pieces.Add(clonedPiece);
			}

			hypotheticalBoard.ExecuteMove(new MoveCommand(piece.Position, targetPosition));
			return hypotheticalBoard;
		}

		public void PlayTurn(string moveCommandStr)
		{
			if (_gameOver)
			{
				Console.WriteLine("Game over! Cannot play any more moves.");
				return;
			}

			var moveCommand = MoveCommand.FromString(moveCommandStr);
			var pieceToMove = _board.GetPiece(moveCommand.Src);

			if (pieceToMove == null || pieceToMove.Color != _currentTurnColor)
			{
				Console.WriteLine("Invalid move: It's not the player's turn or no piece at the source position.");
				return;
			}

			var validPositions = pieceToMove.GetMoveablePositions(_board);
			if (!validPositions.Contains(moveCommand.Dst) || DoesMoveExposeKing(pieceToMove, moveCommand.Dst))
			{
				Console.WriteLine("Invalid move: Target position is not valid or exposes the king.");
				return;
			}

			_board.ExecuteMove(moveCommand);

			if (IsKingThreatened(_board, _currentTurnColor))
			{
				Console.WriteLine("Checkmate! " + _currentTurnColor + " loses.");
				_gameOver = true;
			}
			else
			{
				_currentTurnColor = _currentTurnColor == Piece.WHITE ? Piece.BLACK : Piece.WHITE;
			}
		}

		public void PrintBoard()
		{
			for (int y = _board.Size - 1; y >= 0; y--)
			{
				for (int x = 0; x < _board.Size; x++)
				{
					var piece = _board.GetPiece(new ChessPosition(x, y));
					Console.Write(piece?.Symbol() ?? "..");
					Console.Write(" ");
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		public void StartGame()
		{
			while (!_gameOver)
			{
				Console.WriteLine(_currentTurnColor + "'s turn. Enter move (e.g., e2 e4): ");
				var moveCommandStr = Console.ReadLine();
				PlayTurn(moveCommandStr);
				PrintBoard();
			}
		}
	}

}
