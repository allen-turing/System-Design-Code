namespace ChessGame
{
	public class Bishop : Piece
	{
		private static readonly (int, int)[] BeamIncrements =
			{ (1, 1), (-1, 1), (-1, -1), (1, -1) };

		public Bishop(ChessPosition position, string color) : base(position, color) { }

		public override List<ChessPosition> GetThreatenedPositions(ChessBoard board)
		{
			return BeamIncrements.SelectMany(increment => board.BeamSearchThreat(Position, Color, increment.Item1, increment.Item2)).ToList();
		}

		public override List<ChessPosition> GetMoveablePositions(ChessBoard board)
		{
			return BeamIncrements.SelectMany(increment => board.BeamSearchThreat(Position, Color, increment.Item1, increment.Item2)).ToList();
		}

		protected override string SymbolImpl() => "Bi";
	}

}
