using ChessGame.ChessPieces;

namespace ChessGame
{
	public static class PieceFactory
	{
		public static Piece Create(string pieceType, ChessPosition position, string color)
		{
			return pieceType switch
			{
				PieceType.KING => new King(position, color),
				PieceType.QUEEN => new Queen(position, color),
				PieceType.KNIGHT => new Knight(position, color),
				PieceType.ROOK => new Rook(position, color),
				PieceType.BISHOP => new Bishop(position, color),
				PieceType.PAWN => new Pawn(position, color),
				_ => throw new ArgumentException("Invalid piece type")
			};
		}
	}

}
