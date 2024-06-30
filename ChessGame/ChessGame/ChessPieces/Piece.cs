namespace ChessGame.ChessPieces
{
	public abstract class Piece
	{
		public const string BLACK = "black";
		public const string WHITE = "white";

		public ChessPosition Position { get; private set; }
		public string Color { get; private set; }

		protected Piece(ChessPosition position, string color)
		{
			Position = position;
			Color = color;
		}

		public virtual void Move(ChessPosition targetPosition)
		{
			Position = targetPosition;
		}

		public abstract List<ChessPosition> GetThreatenedPositions(ChessBoard board);
		public abstract List<ChessPosition> GetMoveablePositions(ChessBoard board);
		protected abstract string SymbolImpl();

		public string Symbol()
		{
			var blackColorPrefix = "\u001b[31;1m";
			var whiteColorPrefix = "\u001b[34;1m";
			var colorSuffix = "\u001b[0m";
			var retval = SymbolImpl();
			return Color == BLACK ? $"{blackColorPrefix}{retval}{colorSuffix}" : $"{whiteColorPrefix}{retval}{colorSuffix}";
		}
	}

}
