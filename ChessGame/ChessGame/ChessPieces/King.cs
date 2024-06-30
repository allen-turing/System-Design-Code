namespace ChessGame.ChessPieces
{
	public class King : Piece
	{
		private static readonly (int, int)[] SpotIncrements =
			{ (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1), (0, -1) };

		private ChessBoard _boardHandle;

		public King(ChessPosition position, string color) : base(position, color) { }

		public void SetBoardHandle(ChessBoard board)
		{
			_boardHandle = board;
			_boardHandle.RegisterKingPosition(Position, Color);
		}

		public override void Move(ChessPosition targetPosition)
		{
			base.Move(targetPosition);
			_boardHandle.RegisterKingPosition(targetPosition, Color);
		}

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

		protected override string SymbolImpl() => "Ki";
	}

}
