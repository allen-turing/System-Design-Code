namespace ChessGame.ChessPieces
{
	public class Knight : Piece
	{
		private static readonly (int, int)[] SpotIncrements =
			{ (1, 2), (2, 1), (2, -1), (1, -2), (-1, -2), (-2, -1), (-2, 1), (-1, 2) };

		public Knight(ChessPosition position, string color) : base(position, color) { }

		public override List<ChessPosition> GetThreatenedPositions(ChessBoard board)
		{
			return SpotIncrements
				.Select(increment => board.SpotSearchThreat(Position, Color, increment.Item1, increment.Item2, true))
				.Where(pos => pos != null)
				.ToList();
		}

		public override List<ChessPosition> GetMoveablePositions(ChessBoard board)
		{
			return SpotIncrements
				.Select(increment => board.SpotSearchThreat(Position, Color, increment.Item1, increment.Item2))
				.Where(pos => pos != null)
				.ToList();
		}

		protected override string SymbolImpl() => "Kn";
	}

}
